using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class GameManager : MonoBehaviour
{
    public static GameManager instance
    {
        get
        {
            if (m_inst == null)
            {
                m_inst = FindObjectOfType<GameManager>();
            }
            return m_inst;
        }
    }

    private static GameManager m_inst;


    [Header("Games")]
    public GameObject Player;
    private Vector3 PlayerOriginVec;
    private Rigidbody rigid;  
    public GameObject Stage;
    public GameObject WarnPoints;
    public GameObject PincetPoint;
    public bool BallMode = false;
    private Vector3 SpawnVec;
    

    //Control
    private bool isStart = false;
    private float StartCount = 3.01f;
    private float PlayTime = 0;
    private int CatchBalls = 0;


    private void Awake()
    {
        SaveManager.instance.DataLoad();
        PlayerOriginVec = Player.transform.position;
        rigid = Player.GetComponent<Rigidbody>();
      
    }

    private void Update()
    {
        if (isStart && StartCount > 0)
        {
            StartCount -= Time.deltaTime;
            UIManager.instance.SetStartCount(StartCount);
            rigid.velocity = Vector3.zero;
        }
        else if (isStart && StartCount <= 0)
        {
            //정상적인 게임진행
            if (BallMode) { 
                PlayTime += Time.deltaTime;
                UIManager.instance.SetPlayTime(PlayTime);
            }
            else
            {
                PlayTime -= Time.deltaTime;
                UIManager.instance.SetPlayTime(PlayTime);
                if (PlayTime <= 0)
                {
                    GameEnd();
                }
            }
        }
       
    }

    public void Resume()
    {
        isStart = true;
        StartCount = 3.01f;
        SoundManager.instance.BGMOn();
        SoundManager.instance.CountDownOn();
    }


    public void GameReset()
    {
        isStart = true;
        StartCount = 3.01f;
        SoundManager.instance.BGMOn();
        SoundManager.instance.CountDownOn();
        Player.transform.position = PlayerOriginVec;
        Player.transform.SetParent(Stage.transform);
        FloorControl.FloorVec = Vector3.zero;
        Stage.transform.rotation = Quaternion.Euler(Vector3.zero);
        if (BallMode)
        {
            PlayTime = 0;
        }
        else
        {
            PlayTime = 60;
        }
        CatchBalls = 0;
        UIManager.instance.ReStart();
    }


    public void GameEnd()
    {
        SaveManager.instance.DataLoad();        
        SoundManager.instance.BGMStop();
        SoundManager.instance.EndSoundOn();
        if (BallMode)
        {
            SaveManager.instance.SaveData.BallRank.Add(PlayTime);
            SaveManager.instance.SaveData.BallRank.Sort();
            SaveManager.instance.SaveData.BallRank.Reverse();
            for (int i = 0; i < SaveManager.instance.SaveData.BallRank.Count; i++)
            {
                if (PlayTime == SaveManager.instance.SaveData.BallRank[i])
                {
                    if (i <= 49)
                    {
                        UIManager.instance.SetRank(i + 1);
                    }
                    else
                    {
                        UIManager.instance.SetRank(0);
                    }
                }
            }
            UIManager.instance.SetResult(PlayTime);
        }
        else
        {           
            SaveManager.instance.SaveData.PincetRank.Add(CatchBalls);        
            SaveManager.instance.SaveData.PincetRank.Sort();                
            for (int i=0; i < SaveManager.instance.SaveData.PincetRank.Count; i++)
            {
                if (CatchBalls == SaveManager.instance.SaveData.PincetRank[i])
                {
                    i = SaveManager.instance.SaveData.PincetRank.Count -i;
                    if(i <= 49)
                    {
                        UIManager.instance.SetRank(i);
                    }
                    else
                    {
                        UIManager.instance.SetRank(0);
                    }
                }
            }         
            UIManager.instance.SetResult(CatchBalls);
        }





        UIManager.instance.OpenResult();
        isStart = false;
        SaveManager.instance.DataSave();
    }

    public void SetBallMode()
    {
      
        BallMode = true;
        GameReset();
        PincetPointOff();
        WarnPointsOn();
        
    }

    public void SetPincetMode()
    {        
        BallMode = false;
        GameReset();
        PincetPointOn();
        WarnPointsOff();
       
    }
    public void PlayerSpawn()
    {
        SpawnVec.x = UnityEngine.Random.Range(-14.5f, -0.6f);
        SpawnVec.y = 9f;
        SpawnVec.z = UnityEngine.Random.Range(-14.5f, -0.6f);
        Player.transform.localPosition = SpawnVec;
    }
    public void PlayerGravityOn()
    {
        rigid.useGravity = true;
        Player.transform.localScale=new Vector3(1f, 1f, 1f);
    }
    public void PlayerGravityOff()
    {
        rigid.velocity = Vector3.zero;
        rigid.useGravity = false;
    }

    public void WarnPointsOn()
    {
        WarnPoints.SetActive(true);
    }
    public void WarnPointsOff()
    {
        WarnPoints.SetActive(false);
    }

    public void PincetPointOn()
    {
        PincetPoint.SetActive(true);
    }

    public void PincetPointOff()
    {
        PincetPoint.SetActive(false);
    }
    
    public void GetBall()
    {
        CatchBalls += 1;
        UIManager.instance.SetCatchBalls(CatchBalls);
    }


    //리턴값
    public float CountChk()
    {
        return StartCount;
    }

    public float PlayTimechk()
    {
        return PlayTime;
    }

    public int CatchBallsChk()
    {
        return CatchBalls;
    }

    public bool StartChk()
    {
        return isStart;
    }

    public bool isBallMode()
    {
        return BallMode;
    }
  
}
