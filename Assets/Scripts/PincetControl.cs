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
    private Vector3 OriginVec;

    //바닥 경고용
    private RaycastHit hit;
    private int laymask =1 << 8;
    private Renderer rend;

    private void Awake()
    {
        OriginVec = Pincet.transform.position;
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
                    Debug.DrawRay(transform.position, transform.up * -30,Color.red);
                    Pincet.transform.position = Vector3.MoveTowards(Pincet.transform.position, GrabPoint[MoveCode].transform.position, DownSpeed * Time.deltaTime);                   
                    if (Physics.Raycast(transform.position, transform.up * -1,out hit, Mathf.Infinity, laymask))
                    {                     
                        rend = hit.transform.GetComponent<Renderer>();
                        rend.material.color = Color.red;
                    }

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
                    if (rend != null)
                    {
                        rend.material.color = Color.white;
                    }

                }
            }

            
        }
    }

    public void ResetPincet()
    {
        DoMove = true;
        DownChk = false;
        MoveCode = Random.Range(0, MovePoint.Length);
        Pincet.transform.position = OriginVec;
        if (rend != null)
        {
            rend.material.color = Color.white;
            rend = null;
        }
    }

}
