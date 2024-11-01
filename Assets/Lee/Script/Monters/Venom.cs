using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Venom : Monster
{
    private float lastAttackTime;
    private float playerDistance;
    public int RequiredJumpTimes;
    private int JumpTimes = 0;


    // Start is called before the first frame update
    void Start()
    {
        SetState(AIState.IdleState);
        // subscribes onjump event
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
            SetState(AIState.AttackState);
        }
    }

    public void AttackUpdate()
    {
        if (Time.time - lastAttackTime > data.attackRate)
        {
            lastAttackTime = Time.time;
            // player takes damage
            Attack();
        }
        transform.position = player.transform.position;
    }

    public void JumpOnHold()
    {
        if (aiState == AIState.AttackState)
        {
            JumpTimes++;
            if (JumpTimes >= RequiredJumpTimes)
            {
                animator.SetTrigger("Die");
            }
        }
    }
}
