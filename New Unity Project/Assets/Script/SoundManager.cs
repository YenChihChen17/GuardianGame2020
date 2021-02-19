using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource audioSource;
    [SerializeField]
    private AudioClip BossHurtAudio, BossDeadAudio;
    
    private void Awake()
    {
        instance = this;
    }

    /*public void HurtAudio()
    {
        audioSource.clip = BossHurtAudio;
        audioSource.play();
    }

    public void DeadAudio()
    {
        audioSource.clip = BossDeadAudio;
        audioSource.play();
    }*/
}
