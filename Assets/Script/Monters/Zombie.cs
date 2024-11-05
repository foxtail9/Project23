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
    public AudioClip[] sounds; // �Ϲ� ���� Ŭ�� �迭
    public AudioClip attackSound; // ���� ���� Ŭ��
    public float minTimeBetweenSounds = 10f; // �Ϲ� ���� ��� �� �ּ� �ð�
    public float maxTimeBetweenSounds = 5f; // �Ϲ� ���� ��� �� �ִ� �ð�
    private float lastAttackTime;
    private float lastSoundTime;
    private AudioSource audioSource; // AudioSource ������Ʈ

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
        while (true) // ���� ����
        {
            // �Ϲ� ���� ���
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