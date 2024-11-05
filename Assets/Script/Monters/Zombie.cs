using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum AIState
{
    AttackState,
    IdleState,
    MoveState,
    DeadState
}
public class Zombie : Monster
{
    public AudioClip[] sounds; // 일반 사운드 클립 배열
    public AudioClip attackSound; // 공격 사운드 클립
    public float minTimeBetweenSounds = 10f; // 일반 사운드 재생 간 최소 시간
    public float maxTimeBetweenSounds = 5f; // 일반 사운드 재생 간 최대 시간
    private float lastAttackTime;
    private float lastSoundTime;
    private AudioSource audioSource; // AudioSource 컴포넌트

    public override void Start()
    {
        audioSource = GetComponent<AudioSource>();
        base.Start();
        SetState(AIState.IdleState);
        StartCoroutine(PlayRandomSound());
    }

    // Update is called once per frame
    void Update()
    {
        playerDistance = Vector3.Distance(player.transform.position, transform.position);

        switch (aiState)
        {
            case AIState.IdleState:
                IdleUpdate();
                break;
            case AIState.AttackState:
                MoveUpdate();
                break;
            case AIState.MoveState:
                MoveUpdate();
                break;
            case AIState.DeadState:
                break;
        }

    }

    public void SetState(AIState state)
    {
        aiState = state;
        animator.SetBool("isWalk", aiState != AIState.IdleState);
    }

    public void IdleUpdate()
    {
        if (RecognizePlayer())
        {
            SetState(AIState.MoveState);
        }
    }

    public void MoveUpdate()
    {
        MoveToPlayer();
        if (!RecognizePlayer())
        {
            SetState(AIState.IdleState);
        }
        else if (playerDistance < data.attackDistance)
        {
            AttackUpdate();
        }
    }

    public void AttackUpdate()
    {
        if (Time.time - lastAttackTime > data.attackRate)
        {
            lastAttackTime = Time.time;
            // player takes damage
            Attack();
            PlayAttackSound();
        }
    }
    private IEnumerator PlayRandomSound()
    {
        while (true) // 무한 루프
        {
            // 일반 사운드 재생
            if (Time.time - lastSoundTime > Random.Range(minTimeBetweenSounds, maxTimeBetweenSounds))
            {
                int randomIndex = Random.Range(0, sounds.Length);
                audioSource.clip = sounds[randomIndex];
                audioSource.Play();
                lastSoundTime = Time.time;
            }
            yield return null; 
        }
    }

    private void PlayAttackSound()
    {
        if (attackSound != null)
        {
            audioSource.clip = attackSound;
            audioSource.Play();
        }
    }
}