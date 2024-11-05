using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Venom : Monster, IMonsterSound // Monster Ŭ������ ��ӹް� �������̽� ����
{
    private float lastAttackTime;
    public int RequiredJumpTimes = 3;
    public int JumpTimes = 0;
    public GameObject playerSight;

    // ���� Ŭ�� ����
    public AudioClip[] idleSounds; // Idle ���� ���� Ŭ�� �迭
    public AudioClip[] moveSounds; // Move ���� ���� Ŭ�� �迭
    public AudioClip attackSound;   // ���� ���� Ŭ��

    private AudioSource audioSource; // AudioSource ������Ʈ
    private Coroutine soundCoroutine; // Idle �Ǵ� Move ���� ��� �ڷ�ƾ

    public override void Start()
    {
        base.Start();
        audioSource = GetComponent<AudioSource>();
        playerSight = player.GetComponentInChildren<Camera>().transform.parent.gameObject;
        SetState(AIState.IdleState);
        GameManager.Instance.Player.controller.JumpEvent += JumpOnHold;
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
                AttackUpdate();
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

        // ���¿� ���� ���� �ڷ�ƾ ���� �Ǵ� ����
        if (aiState == AIState.IdleState || aiState == AIState.MoveState)
        {
            if (soundCoroutine == null)
            {
                soundCoroutine = StartCoroutine(PlaySounds());
            }
        }
        else // IdleState�� MoveState�� �ƴ� ��� �ڷ�ƾ ����
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
        transform.rotation = Quaternion.Euler(90, player.transform.localEulerAngles.y, 0);
        transform.position = player.transform.position + (playerSight.transform.forward + player.transform.forward).normalized + new Vector3(0, 0.5f, 0);
    }

    public void JumpOnHold(InputAction.CallbackContext context)
    {
        if (aiState == AIState.AttackState)
        {
            JumpTimes++;
            if (JumpTimes >= RequiredJumpTimes)
            {
                SetState(AIState.DeadState);
                transform.rotation = Quaternion.Euler(0, 0, 0);
                animator.SetTrigger("Die");
            }
        }
    }

    // Idle �Ǵ� Move ���� ��� �ڷ�ƾ
    private IEnumerator PlaySounds()
    {
        while (aiState == AIState.IdleState || aiState == AIState.MoveState)
        {
            if (aiState == AIState.IdleState)
            {
                PlayIdleSound(idleSounds); // Idle ���� ���� ���
            }
            else if (aiState == AIState.MoveState)
            {
                PlayMoveSound(moveSounds); // Move ���� ���� ���
            }

            yield return new WaitForSeconds(1.7f); // 5�� �������� �ݺ� (�ʿ信 ���� ���� ����)
        }
    }

    // IMonsterSound �������̽� �޼��� ����
    public void PlayIdleSound(AudioClip[] idleSounds)
    {
        if (idleSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, idleSounds.Length);
            audioSource.clip = idleSounds[randomIndex]; // �������� ���� Ŭ�� ����
            audioSource.Play(); // ���õ� ���� Ŭ�� ���
        }
    }

    // Move ���� ���� ���
    private void PlayMoveSound(AudioClip[] moveSounds)
    {
        if (moveSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, moveSounds.Length);
            audioSource.clip = moveSounds[randomIndex]; // �������� ���� Ŭ�� ����
            audioSource.Play(); // ���õ� ���� Ŭ�� ���
        }
    }

    public void PlayAttackSound(AudioClip attackSound)
    {
        if (attackSound != null) // ���� ���� Ŭ���� null�� �ƴ� ���
        {
            audioSource.clip = attackSound; // ���� ���� Ŭ�� ����
            audioSource.Play(); // ���� ���� ���
        }
    }
}
