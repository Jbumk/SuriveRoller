using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PincetControl : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject Pincet;

    [Header("Contorl")]
    public RectTransform Stick;
    public Image BtnDown;
    private Vector2 inputDir;
    private float Range = 20f;
    private Vector3 PincetVec= Vector3.zero;
    private Vector3 UpPoint;
    private Vector3 DownPoint;
   


    [Header("Auto")]
    public GameObject[] MovePoint;
    public GameObject[] GrabPoint;
   
    public float MoveSpeed = 10f;
    public float DownSpeed = 10f;
    private int MoveCode =0;
    private bool DownChk = false;
    private bool DoMove = false;
    private Vector3 OriginVec;

    private bool isBall = true;

    //바닥 경고용
    private RaycastHit hit;
    private int laymask =1 << 8;
    private Renderer rend;


    private void Awake()
    {
        OriginVec = Pincet.transform.position;
        PincetVec = OriginVec;
        MoveCode = Random.Range(0, MovePoint.Length);
    }



    public void OnBeginDrag(PointerEventData eventData)
    {

        inputDir = (eventData.position - eventData.pressPosition) / 10;
        if (inputDir.magnitude < Range)
        {
            Stick.anchoredPosition = inputDir;
        }
        else
        {
            inputDir = inputDir.normalized * Range;
            Stick.anchoredPosition = inputDir;
        }


    }

    public void OnDrag(PointerEventData eventData)
    {

        inputDir = (eventData.position - eventData.pressPosition) / 10;
        if (inputDir.magnitude < Range)
        {
            Stick.anchoredPosition = inputDir;
        }
        else
        {
            inputDir = inputDir.normalized * Range;
            Stick.anchoredPosition = inputDir;
        }

    }
     public void OnEndDrag(PointerEventData eventData)
    {
        inputDir = Vector2.zero;
        Stick.anchoredPosition = Vector2.zero;
        
    }


    private void FixedUpdate()
    {

        if (GameManager.instance.CountChk() <= 0)
        {
            //BallMode일때 움직임
            if (isBall)
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
                        if (Physics.Raycast(Pincet.transform.position, Pincet.transform.up * -1, out hit, Mathf.Infinity, laymask))
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
            //PincetMode일때 움직임
            else
            {
                if (DownChk)
                {                                      
                    Pincet.transform.position = Vector3.MoveTowards(Pincet.transform.position, DownPoint, DownSpeed * Time.deltaTime);
                    if (Vector3.Distance(Pincet.transform.position, DownPoint) <= 0.1)
                    {
                        DownChk = false;
                    }
                }
                else
                {
                    if (DoMove)
                    {
                        PincetVec.x += inputDir.normalized.x * Time.deltaTime * MoveSpeed;
                        PincetVec.z += inputDir.normalized.y * Time.deltaTime * MoveSpeed;

                        PincetVec.x = Mathf.Clamp(PincetVec.x, -6f, 6f);
                        PincetVec.z = Mathf.Clamp(PincetVec.z, -1f, 11f);
                        Pincet.transform.position = PincetVec;
                    }
                    else
                    {
                        Pincet.transform.position = Vector3.MoveTowards(Pincet.transform.position, UpPoint, DownSpeed * Time.deltaTime);
                        if (Vector3.Distance(Pincet.transform.position, UpPoint) <= 0.1)
                        {
                            DoMove = true;
                            GameManager.instance.PincetPointOn();
                            BtnDown.color = Color.green;
                        }
                    }

                    
                }
            }

        }
            
        
    }

    public void ResetPincet()
    {
        PincetVec = OriginVec;
        Pincet.transform.position = OriginVec;
        BtnDown.color = Color.green;
        DoMove = true;
        DownChk = false;
        MoveCode = Random.Range(0, MovePoint.Length);
        if (!isBall)
        {
            GameManager.instance.PincetPointOn();
        }
        if (rend != null)
        {
            rend.material.color = Color.white;
            rend = null;
        }
    }

    public void PutDown()
    {
        if (BtnDown.color != Color.red && GameManager.instance.CountChk() <= 0)
        {
            GameManager.instance.PincetPointOff();
            BtnDown.color = Color.red;
            DownChk = true;
            DoMove = false;
            UpPoint = Pincet.transform.position;
            DownPoint = UpPoint;
            DownPoint.y = UpPoint.y - 12.5f;
        }
    }

    public void SetBallMode()
    {
        isBall = true;
    }

    public void SetPincetMode()
    {
        isBall = false;
    }

}
