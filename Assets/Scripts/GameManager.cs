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
    private bool BallMode = false;
    private Vector3 SpawnVec;
    

    //Control
    private bool isStart = false;
    private float StartCount = 3.1f;
    private float PlayTime = 0;


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
            PlayTime += Time.deltaTime;
            UIManager.instance.SetPlayTime(PlayTime);
        }
       
    }


    public void GameReset()
    {
        Player.transform.position = PlayerOriginVec;
        Player.transform.SetParent(Stage.transform);
        FloorControl.FloorVec = Vector3.zero;
        Stage.transform.rotation = Quaternion.Euler(Vector3.zero);      
        PlayTime = 0;        
    
    }

    public void GameStart()
    {
        isStart = true;
        StartCount = 3.1f;
     
        SoundManager.instance.BGMOn();
    }

    public void GameEnd()
    {
        SaveManager.instance.DataLoad();
        SaveManager.instance.SaveData.Rank.Add(PlayTime);
        SaveManager.instance.SaveData.Rank.Sort();
        SaveManager.instance.SaveData.Rank.Reverse();
        for(int i=0; i< SaveManager.instance.SaveData.Rank.Count; i++)
        {
            if (PlayTime == SaveManager.instance.SaveData.Rank[i])
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



        UIManager.instance.OpenResult();
        UIManager.instance.SetResult(PlayTime);
        SoundManager.instance.BGMStop();
        isStart = false;
        SaveManager.instance.DataSave();
    }

    public void SetBallMode()
    {
        BallMode = true;
    }

    public void SetPincetMode()
    {
        BallMode = false;
    }
    public void PlayerSpawn()
    {
        SpawnVec.x = UnityEngine.Random.Range(-6.5f, 6.5f);
        SpawnVec.y = 1f;
        SpawnVec.z = UnityEngine.Random.Range(-2f, 11.5f);
        Player.transform.position = SpawnVec;
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

    //리턴값
    public float CountChk()
    {
        return StartCount;
    }

    public float PlayTimechk()
    {
        return PlayTime;
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
