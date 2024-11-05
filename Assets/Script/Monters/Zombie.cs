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

public class Zombie : Monster, IMonsterSound
{
    private float lastAttackTime;
    // 사운드 클립 정의
    public AudioClip[] idleSounds; // Idle 상태 사운드 클립 배열
    public AudioClip[] moveSounds; // Move 상태 사운드 클립 배열
    public AudioClip attackSound;   // 공격 사운드 클립

    private AudioSource audioSource; // AudioSource 컴포넌트
    private Coroutine soundCoroutine; // Idle 또는 Move 사운드 재생 코루틴

    public override void Start()
    {
        base.Start();
        audioSource = GetComponent<AudioSource>();
        SetState(AIState.IdleState);
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

        // 상태에 따라 사운드 코루틴 시작 또는 중지
        if (aiState == AIState.IdleState || aiState == AIState.MoveState)
        {
            if (soundCoroutine == null)
            {
                soundCoroutine = StartCoroutine(PlaySounds());
            }
        }
        else // IdleState와 MoveState가 아닐 경우 코루틴 중지
        {
            if (soundCoroutine != null)
            {
                StopCoroutine(soundCoroutine);
                soundCoroutine = null;
            }
        }
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
            PlayAttackSound(attackSound);
        }
    }

    // Idle 또는 Move 사운드 재생 코루틴
    private IEnumerator PlaySounds()
    {
        while (aiState == AIState.IdleState || aiState == AIState.MoveState)
        {
            if (aiState == AIState.IdleState)
            {
                PlayIdleSound(idleSounds); // Idle 상태 사운드 재생
            }
            else if (aiState == AIState.MoveState)
            {
                PlayMoveSound(moveSounds); // Move 상태 사운드 재생
            }

            yield return new WaitForSeconds(5f); // 5초 간격으로 반복 (필요에 따라 조정 가능)
        }
    }

    // IMonsterSound 인터페이스 메서드 구현
    public void PlayIdleSound(AudioClip[] idleSounds)
    {
        if (idleSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, idleSounds.Length);
            audioSource.clip = idleSounds[randomIndex]; // 랜덤으로 사운드 클립 선택
            audioSource.Play(); // 선택된 사운드 클립 재생
        }
    }

    // Move 상태 사운드 재생
    private void PlayMoveSound(AudioClip[] moveSounds)
    {
        if (moveSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, moveSounds.Length);
            audioSource.clip = moveSounds[randomIndex]; // 랜덤으로 사운드 클립 선택
            audioSource.Play(); // 선택된 사운드 클립 재생
        }
    }

    public void PlayAttackSound(AudioClip attackSound)
    {
        if (attackSound != null) // 공격 사운드 클립이 null이 아닐 경우
        {
            audioSource.clip = attackSound; // 공격 사운드 클립 설정
            audioSource.Play(); // 공격 사운드 재생
        }
    }
}
