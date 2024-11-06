using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMonsterSound
{
    void PlayIdleSound(AudioClip[] idleSounds);
    void PlayAttackSound(AudioClip attackSound);
}

public class MonsterSound : MonoBehaviour, IMonsterSound
{
    protected AudioSource audioSource;

    protected virtual void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayIdleSound(AudioClip[] idleSounds)
    {
        if (idleSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, idleSounds.Length);
            audioSource.clip = idleSounds[randomIndex];
            audioSource.Play();
        }
    }

    public void PlayAttackSound(AudioClip attackSound)
    {
        audioSource.clip = attackSound;
        audioSource.Play();
    }
}
