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

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    
    }

    private void FixedUpdate()
    {       
        if (isfall)
        {          
            transform.Translate(Vector3.down * 5f * Time.deltaTime);            
        }
       

        if (GameManager.instance.PlayTimechk()<=0)
        {
            isfall = true;
            ToysControl.instance.ReturnTargetList(targetNum);
            targetNum = 0;
            ObjPool.instance.ReturnToys(this);
            if (rend != null)
            {
                rend.material.color = Color.white;
                rend = null;
            }
        }

    }
    private void OnTriggerEnter(Collider col)
    {
        //바닥에 닿았을때
        if (col.gameObject.CompareTag("Floor") && isfall)
        {
            Debug.Log("바닥 닿음");
            isfall = false;
            if (rend != null)
            {
                rend.material.color = Color.white;
                rend = null;
            }
        }

        //집게에 잡혔을때
        if (col.gameObject.CompareTag("Pincet")&&!isfall)
        {
            Debug.Log("잡힘");
            transform.SetParent(col.transform);
        }

        //잡혀서 올라갔을때
        if (col.gameObject.CompareTag("GrabPoint") && !isfall)
        {
            Debug.Log("삭제");
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
            Debug.Log("깔림");
            GameManager.instance.GameEnd();
            isfall = true;
            ToysControl.instance.ReturnTargetList(targetNum);
            targetNum = 0;
            ObjPool.instance.ReturnToys(this);
            if (rend != null)
            {
                rend.material.color = Color.white;
                rend = null;
            }
        }
    }

    public void setTarget(int targetNum)
    {      
        if (Physics.Raycast(transform.position, transform.up * -1, out hit, Mathf.Infinity, laymask))
        {
            rend = hit.transform.GetComponent<Renderer>();
            rend.material.color = Color.red;
        }
        this.targetNum = targetNum;
    }
}
