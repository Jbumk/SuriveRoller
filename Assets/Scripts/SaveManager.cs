using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class SaveManager : MonoBehaviour
{

    static GameObject _container;
    static GameObject Container
    {
        get
        {
            return _container;
        }
    }

    static SaveManager _instance;
    public static SaveManager instance
    {
        get
        {
            if (!_instance)
            {
                _container = new GameObject();
                _container.name = "SaveManager";
                _instance = _container.AddComponent(typeof(SaveManager)) as SaveManager;
                DontDestroyOnLoad(_container);
            }
            return _instance;
        }
    }

    public string SaveDataFileName = ".json";

    public SaveData _saveData;
    public SaveData SaveData
    {
        get
        {
            if (_saveData == null)
            {
                DataLoad();
                DataSave();
            }
            return _saveData;
        }
    }

    public void DataLoad()
    {
        string filePath = Application.persistentDataPath + SaveDataFileName;
        if (File.Exists(filePath))
        {
           
            string FromJsonData = File.ReadAllText(filePath);
            _saveData = JsonUtility.FromJson<SaveData>(FromJsonData);
        }
        else
        {
         
            _saveData = new SaveData();
        }
    }

    public void DataSave()
    {
        string ToJsonData = JsonUtility.ToJson(SaveData);
        string filePath = Application.persistentDataPath + SaveDataFileName;
        File.WriteAllText(filePath, ToJsonData);

    }

    private void OnApplicationQuit()
    {
        DataSave();
    }

}
