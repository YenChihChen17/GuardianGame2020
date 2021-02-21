using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource audioSource;
    [SerializeField]
    private AudioClip Boss_HurtAudio, Boss_DeadAudio;
    
    private void Awake()
    {
        instance = this;
    }

    public void BossHurtAudio()
    {
        audioSource.clip = Boss_HurtAudio;
        audioSource.Play();
    }

    public void BossDeadAudio()
    {
        audioSource.clip = Boss_DeadAudio;
        audioSource.Play();
    }
}
