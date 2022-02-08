using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FloorControl : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("Floor")]
    private GameObject Floor;
    public GameObject SingleFloor;
    public GameObject MultiFloor;  
    public float SpinSpeed=9f;  
    public static Vector3 FloorVec=Vector3.zero;
    private Vector2 AccelSet;

    [Header("Stick")]
    public RectTransform Stick;   
    private float Range = 20f;
    Vector2 inputDir;

    //UI의 vscom 이나 Host로 참가 할때 호출해준다;
    public void SetSingle()
    {
        Floor = SingleFloor;
    }

    public void SetMulti()
    {
        Floor = MultiFloor;
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
            //컨트롤 방식이 JoyStick(D-pad)방식인지 체크
            if (UIManager.instance.MethodPadChk())
            {                
                FloorVec.x += inputDir.normalized.y * UIManager.instance.XReverseChk();
                FloorVec.z -= inputDir.normalized.x * UIManager.instance.YReverseChk();
            }
            else
            {              
                FloorVec.x -= (UIManager.instance.AccelChk().y-Input.acceleration.y) * UIManager.instance.XReverseChk();
                FloorVec.z += (UIManager.instance.AccelChk().x-Input.acceleration.x) * UIManager.instance.YReverseChk();
            }
            FloorVec.x = Mathf.Clamp(FloorVec.x, -40f, 40f);
            FloorVec.z = Mathf.Clamp(FloorVec.z, -40f, 40f);
            Floor.transform.rotation = Quaternion.Euler(FloorVec);
        }


    }  

}
