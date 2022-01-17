using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FloorControl : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("Floor")]
    public GameObject Floor;
    public float SpinSpeed=9f; 
    public static Quaternion FloorQuat;


    [Header("Stick")]
    public RectTransform Stick;
    public RectTransform Rect;
    private float Range = 20f;
    Vector2 inputDir;

    private void Awake()
    {
        FloorQuat = Floor.transform.rotation;
    }



    public void OnBeginDrag(PointerEventData eventData)
    {

        inputDir = (eventData.position - eventData.pressPosition)/10;
        if(inputDir.magnitude < Range)
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
       
        inputDir = (eventData.position - eventData.pressPosition)/10;
        Debug.Log(inputDir);
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
            Floor.transform.Rotate(inputDir*Time.deltaTime,Space.Self);
        }


    }

}
