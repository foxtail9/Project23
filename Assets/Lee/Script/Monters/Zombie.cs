using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum AIState
{
    AttackState,
    IdleState,
    MoveState
}
public class Zombie : Monster
{
    private float lastAttackTime;
    private AIState aiState;
    private float playerDistance;

    // Start is called before the first frame update
    void Start()
    {
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
        }
        
    }

    public void SetState(AIState state)
    {
        aiState = state;

        switch (aiState)
        {
            case AIState.IdleState:
                animator.SetBool("isWalk", aiState != AIState.IdleState);
                break;
            case AIState.AttackState:
                animator.SetBool("isWalk", aiState != AIState.IdleState);
                break;
            case AIState.MoveState:
                animator.SetBool("isWalk", aiState != AIState.IdleState);
                MoveUpdate();
                break;
        }
    }

    public void IdleUpdate()
    {
        if(RecognizePlayer())
        {
            SetState(AIState.MoveState);
        }
    }

    public void MoveUpdate()
    {
        MoveToPlayer();
        if(!RecognizePlayer())
        {
            SetState(AIState.IdleState);
        }
        else if(playerDistance <data.attackDistance)
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
        }
    }
}
