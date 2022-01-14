using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PincetHitBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            GameManager.instance.GameEnd();
        }
    }
}
