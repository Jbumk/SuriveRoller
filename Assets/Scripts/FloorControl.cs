using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorControl : MonoBehaviour
{
    public GameObject Floor;
    public float SpinSpeed=9f;
    public static Vector3 FloorVec = Vector3.zero;
   






    private void FixedUpdate()
    {
        if (GameManager.instance.CountChk() <= 0)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                FloorVec += (Vector3.right * SpinSpeed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                FloorVec += (Vector3.forward * SpinSpeed * Time.deltaTime);

            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                FloorVec -= (Vector3.forward * SpinSpeed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                FloorVec -= (Vector3.right * SpinSpeed * Time.deltaTime);
            }
            Floor.transform.rotation = Quaternion.Euler(FloorVec);
        }
              
    }

    public static void ResetStage()
    {
        FloorVec = Vector3.zero;
    }
}
