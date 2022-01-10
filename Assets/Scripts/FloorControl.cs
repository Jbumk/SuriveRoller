using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorControl : MonoBehaviour
{
    public GameObject Floor;
    public float SpinSpeed=3f;
    private Vector3 FloorVec = Vector3.zero;
    private Quaternion FloorQuat;

    private void Awake()
    {
        FloorQuat = Floor.transform.rotation;
        
    }

    private void FixedUpdate()
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
