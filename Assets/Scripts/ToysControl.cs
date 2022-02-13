using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToysControl : MonoBehaviour
{
    public GameObject[] SpawnPlace; 
    public double cycle=3.0;

    private List<int> TargetList = new List<int>();
    private int TempNum;
    private int TargetNum;
    private float Timer = 0;
    
    public static ToysControl instance
    {
        get
        {
            if (m_inst == null)
            {
                m_inst = FindObjectOfType<ToysControl>();
            }
            return m_inst;
        }
       
    }

    private static ToysControl m_inst;          


    private void Awake()
    {
        for(int i=0; i < SpawnPlace.Length; i++)
        {
            TargetList.Add(i);
        }
    }

    private void Update()
    {
        if (GameManager.instance.CountChk() <= 0 && GameManager.instance.StartChk() && GameManager.instance.isBallMode())
        {
            Timer += Time.deltaTime;
            if (Timer >= cycle)
            {
                var obj = ObjPool.instance.GetNewObj();
                TempNum = Random.Range(0, TargetList.Count);
                TargetNum = TargetList[TempNum];
                TargetList.RemoveAt(TempNum);
                obj.transform.position = SpawnPlace[TargetNum].transform.position;
                obj.setTarget(TargetNum);
               

                Timer = 0;
                cycle = Random.Range(3.0f, 7.0f);
            }
        }
        else
        {
            Timer = 0;
        }
    }

    public void ReturnTargetList(int num)
    {
        TargetList.Add(num);
    }
}
