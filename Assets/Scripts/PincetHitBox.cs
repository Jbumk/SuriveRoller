using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PincetHitBox : MonoBehaviour
{
    private float Timer;
    private double ReSpawnTIme = 1.0;
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
                //점수 추가 후 공 재생성?
                col.transform.SetParent(transform);
                GameManager.instance.PlayerGravityOff();
                Timer += Time.deltaTime;
                if (Timer >= ReSpawnTIme)
                {                    
                    col.transform.SetParent(null);
                    UIManager.instance.GetBall();
                    GameManager.instance.PlayerGravityOn();
                    col.transform.SetParent(Stage.transform);
                    GameManager.instance.PlayerSpawn();
                    Timer = 0;
                }
            }
        }
    }
}
