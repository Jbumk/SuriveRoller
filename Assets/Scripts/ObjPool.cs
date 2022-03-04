using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjPool : MonoBehaviour
{
    //싱글톤 구현

    public static ObjPool instance;
    public GameObject ToysPrefab;
    public GameObject Floor;
    private int ToysCount=25;

    Queue<Toys> ToysObjPool = new Queue<Toys>();

    //awke 부분
    private void Awake()
    {
        instance = this;
        Initialize(ToysCount);
    }
    //생성자
    private void Initialize(int count)
    {
        for(int i = 0; i < count; i++)
        {
            ToysObjPool.Enqueue(CreateNewObj());
        }

    }

    private Toys CreateNewObj()
    {
        var newObj = Instantiate(ToysPrefab).GetComponent<Toys>();
        newObj.transform.SetParent(transform);
        newObj.gameObject.SetActive(false);
        return newObj;
    }

    public Toys GetNewObj()
    {
        if (ToysObjPool.Count > 0)
        {
            var obj = ToysObjPool.Dequeue();
            obj.transform.rotation = Floor.transform.rotation;
            obj.transform.SetParent(Floor.transform);            
            obj.gameObject.SetActive(true);

            return obj;
        }
        else
        {
            var newObj = CreateNewObj();
            newObj.transform.rotation = Floor.transform.rotation;
            newObj.transform.SetParent(Floor.transform);
            newObj.gameObject.SetActive(transform);

            return newObj;
        }
    }
    

    public void ReturnToys(Toys obj)
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(instance.transform);
        instance.ToysObjPool.Enqueue(obj);
    }

      
}
