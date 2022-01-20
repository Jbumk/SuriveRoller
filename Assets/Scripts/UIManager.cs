﻿using System.Collections;
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
    public Slider BGM;
    public Slider Effect;
    public Toggle tglJoyStick;
    public Toggle tglHorizontal;
    private bool MethodPad = true;
    public Toggle tglXReverse;
    public Toggle tglYReverse;
    private int XReverse = 1;
    private int YReverse = 1;
    private Vector2 Accel;
    
   

    [Header("MenuRank")]
    public GameObject MenuRank;   

    [Header("MenuInGame")]
    public GameObject MenuInGame;
    public TextMeshProUGUI PlayTime;
    public TextMeshProUGUI Text_StartCount;
    public GameObject JoyPad;

    [Header("MenuPause")]
    public GameObject MenuPuase;

    [Header("MenuResult")]
    public GameObject MenuResult;
    public TextMeshProUGUI Res_PlayTime;
    public TextMeshProUGUI Res_Rank;

    [Header("Games")]
    public GameObject MainStage;


   

    private void Start()
    {       
        //컨트롤 방법 설정
        if (SaveManager.instance.SaveData.JoyStickControl)
        {          
            tglJoyStick.isOn = true;
            tglHorizontal.isOn = false;
            MethodPad = true;
            JoyPad.SetActive(true);
            
        }
        else
        {           
            tglJoyStick.isOn = false;
            tglHorizontal.isOn = true;
            MethodPad = false;
            JoyPad.SetActive(false);
        }

        //X반전 여부 설정
        if (SaveManager.instance.SaveData.XReverse)
        {
            tglXReverse.isOn = true;
            XReverse = -1;
        }
        else
        {
            tglXReverse.isOn = false;
            XReverse = 1;
        }

        //Y반전 여부 설정
        if (SaveManager.instance.SaveData.YReverse)
        {
            tglYReverse.isOn = true;
            YReverse = -1;
        }
        else
        {
            tglYReverse.isOn = false;
            YReverse = 1;
        }

        //X,Y반전 설정 감지
        tglXReverse.onValueChanged.AddListener(delegate
        {
            Debug.Log("X값 변동");
            XReverse *= -1;
            
        });
        tglYReverse.onValueChanged.AddListener(delegate
        {
            Debug.Log("Y값 변동");
            YReverse *= -1;
        });

        //기울기감지 초기값 불러오기
        Accel.x = SaveManager.instance.SaveData.AccelSet.x;
        Accel.y = SaveManager.instance.SaveData.AccelSet.y;
        
    }


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
        //컨트롤 방법 저장
        if (tglJoyStick.isOn)
        {
            Debug.Log("조이스틱 이용");
            MethodPad = true;
            JoyPad.SetActive(true);
            SaveManager.instance.SaveData.JoyStickControl = true;
        }

        if(tglHorizontal.isOn)
        {
            Debug.Log("수평값 이용");
            MethodPad = false;
            JoyPad.SetActive(false);
            SaveManager.instance.SaveData.JoyStickControl = false;
        }

        //X반전 여부저장
        if (tglXReverse.isOn)
        {
            SaveManager.instance.SaveData.XReverse = true;
        }
        else
        {
            SaveManager.instance.SaveData.XReverse = false;
        }
        //Y반전 여부 저장
        if (tglYReverse.isOn)
        {
            SaveManager.instance.SaveData.YReverse = true;
        }
        else
        {
            SaveManager.instance.SaveData.YReverse = false;
        }
       
      
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


    //기울기 초기값조절
    public void AccelerationSet()
    {
        Accel.x = Input.acceleration.x;
        Accel.y = Input.acceleration.y;
        SaveManager.instance.SaveData.AccelSet.x = Accel.x;
        SaveManager.instance.SaveData.AccelSet.y = Accel.y;
    }

    //기능관련===========================




    //리턴값
    public bool MethodPadChk()
    {
        return MethodPad;
    }
    
    public int XReverseChk()
    {
        return XReverse;
    }
    
    public int YReverseChk()
    {
        return YReverse;
    }

    public Vector2 AccelChk()
    {
        return Accel;
    }

    //리턴값


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
