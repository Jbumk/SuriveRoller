using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance
    {
        get
        {
            if (m_inst == null)
            {
                m_inst = FindObjectOfType<SoundManager>();
            }
            return m_inst;
        }
    }

    private static SoundManager m_inst;

    [Header("BGMs")]
    public AudioSource BGM;
    public AudioSource CountDown;
    public AudioSource EndSound;
    public Slider Slide_BGM;

    [Header("Effect")]
    public AudioSource GetPoint;
    public AudioSource BtnClick;
    public AudioSource PincetMove;
    public Slider Slide_Effect;

    private void Start()
    {        
        Slide_BGM.value = SaveManager.instance.SaveData.BGMVolum;
        Slide_Effect.value = SaveManager.instance.SaveData.EffectVolum;
    }

    //Awake 에서 저장값 Load하고 소리값 조절
    private void Update()
    {
        //BGMs 소리 조절
        BGM.volume = Slide_BGM.value;
        CountDown.volume = Slide_BGM.value;
        EndSound.volume = Slide_BGM.value;

        //Effect의 소리조절
        GetPoint.volume = Slide_Effect.value;
        BtnClick.volume = Slide_Effect.value;
        PincetMove.volume = Slide_Effect.value;
        
    }

    //BGM
    public void BGMOn()
    {
        BGM.Play();
    }
    public void BGMStop()
    {
        BGM.Stop();
    }
   
    public void CountDownOn()
    {
        CountDown.Play();
    }
    public void CountDownStop()
    {
        CountDown.Stop();
    }

    public void EndSoundOn()
    {
        EndSound.Play();
    }
    public void EndSoundStop()
    {
        EndSound.Stop();
    }

    //Effect
    public void GetPointOn()
    {
        GetPoint.Play();
    }

    public void PincetMoveOn()
    {
        PincetMove.Play();
    }
}
