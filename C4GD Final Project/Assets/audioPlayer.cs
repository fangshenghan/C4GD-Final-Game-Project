using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioPlayer : MonoBehaviour
{
    [SerializeField] AudioClip HitSound;
    [SerializeField][Range(0f, 1f)] float HitVolume;

    

    [SerializeField] AudioClip eatSound;
    [SerializeField][Range(0f, 1f)] float eatVolume;

    [SerializeField] AudioClip ouchSound;
    [SerializeField][Range(0f, 1f)] float ouchVolume;

    [SerializeField] AudioClip bossHurt;
    [SerializeField][Range(0f, 1f)] float bossHurtVolume;
    
    [SerializeField] AudioClip gunSwitch;
    [SerializeField][Range(0f, 1f)] float gunSwitchVolume;

    public void PlayHitAudio()
    {
        AudioSource.PlayClipAtPoint(HitSound, Camera.main.transform.position, HitVolume);
    }

   

    public void PlayEatAudio()
    {
        AudioSource.PlayClipAtPoint(eatSound, Camera.main.transform.position, eatVolume);
    }

    public void PlayOuchAudio()
    {
        AudioSource.PlayClipAtPoint(ouchSound, Camera.main.transform.position, ouchVolume);
    }

    public void PlayBossHurtAudio()
    {
        AudioSource.PlayClipAtPoint(bossHurt, Camera.main.transform.position, bossHurtVolume);
    }

    public void PlayGunSwitchAudio()
    {
        AudioSource.PlayClipAtPoint(gunSwitch, Camera.main.transform.position, gunSwitchVolume);
    }
}
