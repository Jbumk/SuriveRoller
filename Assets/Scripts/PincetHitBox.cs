using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PincetHitBox : MonoBehaviour
{
    private float Timer;
    private double ReSpawnTIme = 1.1;
    public GameObject Stage;
   


    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (GameManager.instance.isBallMode())
            {               
                GameManager.instance.GameEnd();
            }
            else
            {               
            
                col.transform.SetParent(transform);
                GameManager.instance.PlayerGravityOff();
                Timer += Time.deltaTime;
                if (Timer >= ReSpawnTIme)
                {                    
                    col.transform.SetParent(null);
                    GameManager.instance.GetBall();
                    GameManager.instance.PlayerGravityOn();
                    col.transform.SetParent(Stage.transform);
                    GameManager.instance.PlayerSpawn();
                    Timer = 0;
                }
            }
        }
    }
}
