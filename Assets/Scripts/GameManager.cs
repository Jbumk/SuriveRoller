using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public GameObject Pincet;
    private Vector3 PincetOriginVec;

    public GameObject Stage;
    private Vector3 StageOringVec = Vector3.zero;

    //Control
    private bool isStart = false;
    private float StartCount = 3.1f;
    private float PlayTime = 0;


    private void Awake()
    {
        PlayerOriginVec = Player.transform.position;
        rigid = Player.GetComponent<Rigidbody>();
        // PincetOriginVec = Pincet.transform.position;
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
        Stage.transform.rotation = Quaternion.Euler(Vector3.zero);
        FloorControl.ResetStage();
        PlayTime = 0;
        // Pincet.transform.position = PincetOriginVec;
    
    }

    public void GameStart()
    {
        isStart = true;
        StartCount = 3.1f;
    }

    //리턴값
    public float CountChk()
    {
        return StartCount;
    }
  
}
