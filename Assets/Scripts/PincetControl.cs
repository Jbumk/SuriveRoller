using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PincetControl : MonoBehaviour
{
    public GameObject[] MovePoint;
    public GameObject[] GrabPoint;
    public GameObject Pincet;
    public float MoveSpeed = 10f;
    public float DownSpeed = 10f;
    private int MoveCode =0;
    private bool DownChk = false;
    private bool DoMove = false;

    private void Awake()
    {
        MoveCode = Random.Range(0, MovePoint.Length);
    }
    private void FixedUpdate()
    {       
        if (GameManager.instance.CountChk() <= 0)
        {
            if (DoMove)
            {
                if (Vector3.Distance(Pincet.transform.position, MovePoint[MoveCode].transform.position) <= 0.1)
                {                    
                    DoMove = false;
                    DownChk = true;
                }
                else
                {
                    Pincet.transform.position = Vector3.MoveTowards(Pincet.transform.position, MovePoint[MoveCode].transform.position, MoveSpeed * Time.deltaTime);
                }
            }
            else
            {
                if (DownChk)
                {
                    Pincet.transform.position = Vector3.MoveTowards(Pincet.transform.position, GrabPoint[MoveCode].transform.position, DownSpeed * Time.deltaTime);
                    if (Vector3.Distance(Pincet.transform.position, GrabPoint[MoveCode].transform.position) <= 0.1)
                    {
                        DownChk = false;
                    }
                }
                else
                {
                    Pincet.transform.position = Vector3.MoveTowards(Pincet.transform.position, MovePoint[MoveCode].transform.position, DownSpeed * Time.deltaTime);
                    if (Vector3.Distance(Pincet.transform.position, MovePoint[MoveCode].transform.position) <= 0.1)
                    {
                        MoveCode = Random.Range(0, MovePoint.Length);
                        DoMove = true;
                    }
                }
            }
        }
    }

}
