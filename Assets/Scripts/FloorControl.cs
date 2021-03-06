using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FloorControl : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("Floor")]
    public GameObject Floor;
    public float SpinSpeed=9f;  
    public static Vector3 FloorVec=Vector3.zero;
    private Vector2 AccelSet;

    [Header("Stick")]
    public RectTransform Stick;   
    private float Range = 20f;
    Vector2 inputDir;


    private bool isball = true;
    private double TurnCoolTime=0.5;
    private float TurnTimer = 0;
    private Vector2 RandomVec;
    //최소 0.5~ 3초

 
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
            if (isball)
            {
                //컨트롤 방식이 JoyStick(D-pad)방식인지 체크
                if (UIManager.instance.MethodPadChk())
                {
                    FloorVec.x += inputDir.normalized.y * UIManager.instance.XReverseChk();
                    FloorVec.z -= inputDir.normalized.x * UIManager.instance.YReverseChk();
                }
                else
                {
                    FloorVec.x -= (UIManager.instance.AccelChk().y - Input.acceleration.y) * UIManager.instance.XReverseChk();
                    FloorVec.z += (UIManager.instance.AccelChk().x - Input.acceleration.x) * UIManager.instance.YReverseChk();
                }
                FloorVec.x = Mathf.Clamp(FloorVec.x, -40f, 40f);
                FloorVec.z = Mathf.Clamp(FloorVec.z, -40f, 40f);
                Floor.transform.rotation = Quaternion.Euler(FloorVec);
            }
            else
            {
                TurnTimer += Time.deltaTime;
                if (TurnTimer >= TurnCoolTime)
                {
                    RandomVec.x = Random.Range(-1f, 1f);
                    RandomVec.y = Random.Range(-1f, 1f);
                    TurnCoolTime = Random.Range(0.3f, 2.0f);
                    TurnTimer = 0;
                }
                FloorVec.x -= RandomVec.x;
                FloorVec.y -= RandomVec.y;

                FloorVec.x = Mathf.Clamp(FloorVec.x, -40f, 40f);
                FloorVec.y = Mathf.Clamp(FloorVec.y, -40f, 40f);
                Floor.transform.rotation = Quaternion.Euler(FloorVec);
                
            }
        }


    }  

    public void SetBallMode()
    {
        isball = true;
    }

    public void SetPincetMode()
    {
        isball = false;
    }
}
