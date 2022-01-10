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
        MenuStart.SetActive(false);
        MenuInGame.SetActive(true);
        MainStage.SetActive(true);
        //여기서 초기화 설정
    }
    
    public void OpenPause()
    {
        Time.timeScale = 0;
        MenuPuase.SetActive(true);
    }    


    public void OpenSet()
    {
        Time.timeScale = 0;
        MenuSet.SetActive(true);
    }
    public void CloseSet()
    {        
        MenuSet.SetActive(false);
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
    }

    public void Quit()
    {
        Application.Quit();
    }

    //메뉴이동===========================

    //기능관련===========================
    public void Resume()
    {
        //카운트 시작
        Time.timeScale = 1f;
        MenuPuase.SetActive(false);
    }
    
    public void ReStart()
    {
        //설정 초기화
        Time.timeScale = 1f;
        MenuPuase.SetActive(false);
    }
    //기능관련===========================

}
