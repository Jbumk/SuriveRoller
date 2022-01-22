using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toys : MonoBehaviour
{
    private Rigidbody rigid;
    private bool isfall = true;
    private Vector3 target;
    private int targetNum;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        
        if (isfall)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, 3f * Time.deltaTime);                
        }

        if (GameManager.instance.PlayTimechk()<=0)
        {
            ToysControl.instance.ReturnTargetList(targetNum);
            targetNum = 0;
            ObjPool.instance.ReturnToys(this);
        }

    }
    private void OnTriggerEnter(Collider col)
    {
        //바닥에 닿았을때
        if (col.gameObject.CompareTag("Floor") && isfall)
        {
            Debug.Log("바닥 닿음");
            isfall = false;
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
        }
    }

    public void setTarget(Vector3 target,int targetNum)
    {
        this.target = target;
        this.targetNum = targetNum;
    }
}
