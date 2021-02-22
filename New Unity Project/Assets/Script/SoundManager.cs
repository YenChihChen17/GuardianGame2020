using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource audioSource;
    [SerializeField]
    private AudioClip Boss_HurtAudio, Boss_DeadAudio, Boss_HitAudio,Boss_AttackAudio, Boss_AttackFarAudio;
    [SerializeField]
    private AudioClip PlayerJumpUp, PlayerJumpDown, PlayerAttack, PlayerHurt, PlayerDead, PlayerDefense,PlayerParry;
    
    private void Awake()
    {
        instance = this;
    }

    public void BossHurtAudio()
    {
        audioSource.PlayOneShot(Boss_HurtAudio);
    }

    public void BossDeadAudio()
    {
        audioSource.PlayOneShot(Boss_DeadAudio);
    }
    public void BossHitAudio()
    {
        audioSource.PlayOneShot(Boss_HitAudio);
    }
    public void BossAttackAudio()
    {
        audioSource.PlayOneShot(Boss_AttackAudio);
    }
    public void BossAttackFarAudio()
    {
        audioSource.PlayOneShot(Boss_AttackFarAudio);
    }
    public void Player_JumpUp()
    {
        audioSource.PlayOneShot(PlayerJumpUp);
    }
    public void Player_JumpDown()
    {
        audioSource.PlayOneShot(PlayerJumpDown);
    }
    public void Player_Attack()
    {
        audioSource.PlayOneShot(PlayerAttack);
    }
    public void Player_Hurt()
    {
        audioSource.PlayOneShot(PlayerHurt);
    }
    public void Player_Dead()
    {
        audioSource.PlayOneShot(PlayerDead);
    }
    public void Player_Defense()
    {
        audioSource.PlayOneShot(PlayerDefense);
    }
    public void Player_Parry()
    {
        audioSource.PlayOneShot(PlayerParry);
    }
}
