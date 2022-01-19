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

    public AudioSource BGM;
    public Slider Slide_BGM;

    public AudioSource Effect;
    public Slider Slide_Effect;

    private void Start()
    {
        
        Slide_BGM.value = SaveManager.instance.SaveData.BGMVolum;
        Slide_Effect.value = SaveManager.instance.SaveData.EffectVolum;
    }

    //Awake 에서 저장값 Load하고 소리값 조절
    private void Update()
    {
        BGM.volume = Slide_BGM.value;
        Effect.volume = Slide_Effect.value;
    }

    public void BGMOn()
    {
        BGM.Play();
    }
    public void BGMStop()
    {
        BGM.Stop();
    }
}
