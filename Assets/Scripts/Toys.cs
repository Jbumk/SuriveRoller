using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toys : MonoBehaviour
{
    private Rigidbody rigid;
    private bool isfall = true;
    private int targetNum;
    private RaycastHit hit;
    private Renderer rend;
    private int laymask = 1 << 8;
    private Color WarnColor = Color.red;
    private Color NormalColor = Color.white;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        WarnColor.a = 0.4f;
        NormalColor.a = 0.2f;
    
    }

    private void FixedUpdate()
    {       
        if (isfall)
        {          
            transform.Translate(Vector3.down * 5f * Time.deltaTime);            
        }

        if (!GameManager.instance.isBallMode())
        {
            ObjPool.instance.ReturnToys(this);
        }

        if (GameManager.instance.PlayTimechk()<=0)
        {
            isfall = true;
            ToysControl.instance.ReturnTargetList(targetNum);
            targetNum = 0;
            ObjPool.instance.ReturnToys(this);
            if (rend != null)
            {
                rend.material.color = NormalColor;
                rend = null;
            }
        }


    }
    private void OnTriggerEnter(Collider col)
    {
        //바닥에 닿았을때
        if (col.gameObject.CompareTag("Floor") && isfall)
        {
           
            isfall = false;
            if (rend != null)
            {
                rend.material.color = NormalColor;
                rend = null;
            }
        }

        //집게에 잡혔을때
        if (col.gameObject.CompareTag("Pincet")&&!isfall)
        {
            
            transform.SetParent(col.transform);
        }

        //잡혀서 올라갔을때
        if (col.gameObject.CompareTag("GrabPoint") && !isfall)
        {
           
            ToysControl.instance.ReturnTargetList(targetNum);
            targetNum = 0;
            isfall = true;
            ObjPool.instance.ReturnToys(this);            
        }      
    }

    private void OnCollisionEnter(Collision col)
    {
       
        //플레이어 깔렸을때
        if (col.gameObject.CompareTag("Player") && isfall)
        {
            
            GameManager.instance.GameEnd();
            isfall = true;
            ToysControl.instance.ReturnTargetList(targetNum);
            targetNum = 0;
            ObjPool.instance.ReturnToys(this);
            if (rend != null)
            {
                rend.material.color = NormalColor;
                rend = null;
            }
        }
    }

    public void setTarget(int targetNum)
    {      
        if (Physics.Raycast(transform.position, transform.up * -1, out hit, Mathf.Infinity, laymask))
        {
            rend = hit.transform.GetComponent<Renderer>();
            rend.material.color = WarnColor;
        }
        this.targetNum = targetNum;
    }
}
