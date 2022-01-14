using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance
    {
        get
        {
            if (m_inst == null)
            {
                m_inst = FindObjectOfType<UIManager>();
            }
            return m_inst;
        }
    }

    private static UIManager m_inst;

    [Header("MenuMain")]
    public GameObject MenuMain;

    [Header("MenuStart")]
    public GameObject MenuStart;

    [Header("MenuSet")]
    public GameObject MenuSet;
    public ToggleGroup ControlMethod;
    public Slider BGM;
    public Slider Effect;

    [Header("MenuRank")]
    public GameObject MenuRank;   

    [Header("MenuInGame")]
    public GameObject MenuInGame;
    public TextMeshProUGUI PlayTime;
    public TextMeshProUGUI Text_StartCount;    

    [Header("MenuPause")]
    public GameObject MenuPuase;

    [Header("MenuResult")]
    public GameObject MenuResult;
    public TextMeshProUGUI Res_PlayTime;
    public TextMeshProUGUI Res_Rank;

    [Header("Games")]
    public GameObject MainStage;


          
    //메뉴이동===========================
    public void ToSelectMode()
    {
        
        MenuStart.SetActive(true);
        MenuMain.SetActive(false);
    }

    public void ToRank()
    {
        Time.timeScale = 1f;
        MenuRank.SetActive(true);
        MenuMain.SetActive(false);
        MainStage.SetActive(false);
        MenuStart.SetActive(false);
        MenuInGame.SetActive(false);
        MenuResult.SetActive(false);
    }
    

    public void ToVsCom()
    {
        Time.timeScale = 1f;
        MenuStart.SetActive(false);
        MenuInGame.SetActive(true);
        MainStage.SetActive(true);
   
      
    }
    
    public void OpenPause()
    {
        Time.timeScale = 0;
        MenuPuase.SetActive(true);
        Text_StartCount.gameObject.SetActive(false);
        SoundManager.instance.BGMStop();
     
    }    


    public void OpenSet()
    {
        Time.timeScale = 0;
        MenuSet.SetActive(true);
    }
    public void CloseSet()
    {        
        MenuSet.SetActive(false);
        SaveManager.instance.SaveData.BGMVolum = BGM.value;
        SaveManager.instance.SaveData.EffectVolum = Effect.value;
        SaveManager.instance.DataSave();
    }

    public void OpenResult()
    {
        Time.timeScale = 0;
        MenuResult.SetActive(true);
    }

 

    public void ToHome()
    {
        Time.timeScale = 1f;        
        MenuMain.SetActive(true);
        MainStage.SetActive(false);
        MenuResult.SetActive(false);
        MenuPuase.SetActive(false);
        MenuInGame.SetActive(false);
        MenuRank.SetActive(false);
        MenuStart.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }

    //메뉴이동===========================

    //기능관련===========================


    //이어하기
    public void Resume()
    {
        //카운트 시작    
        Time.timeScale = 1f;
        MenuPuase.SetActive(false);
        Text_StartCount.gameObject.SetActive(true);      
    
    }
    
    //재시작
    public void ReStart()
    {       
        Time.timeScale = 1f;
        MenuPuase.SetActive(false);
        MenuResult.SetActive(false);
        PlayTime.text = " ";
        Text_StartCount.gameObject.SetActive(true);
    }   

    //기능관련===========================




    //UI표시 ============================

    public void SetStartCount(float Time)
    {
        Text_StartCount.text = string.Format("{0}", Mathf.Ceil(Time));
        if (Time <= 0)
        {
            Text_StartCount.gameObject.SetActive(false);
        }
    }

    public void SetPlayTime(float Time)
    {
        PlayTime.text = string.Format("{0:N2} 초", Time);
    }

    public void SetResult(float Time)
    {
        Res_PlayTime.text = string.Format("{0:N2} 초", Time);
    }

    public void SetRank(int Rank)
    {
        if (Rank > 0)
        {
            Res_Rank.text = string.Format("{0} 위", Rank);
        }
        else
        {
            Res_Rank.text = string.Format("랭크아웃");
        }
    }
    //UI표시 ============================

 
  
}
