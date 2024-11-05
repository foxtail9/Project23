using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Venom : Monster
{
    private float lastAttackTime;
    public int RequiredJumpTimes;
    public int JumpTimes = 0;
    public GameObject playerSight;



    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
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
        transform.rotation = Quaternion.Euler(90,player.transform.localEulerAngles.y,0);
        transform.position = player.transform.position + (playerSight.transform.forward +player.transform.forward).normalized + new Vector3 (0,0.5f,0);
    }

    public void JumpOnHold(InputAction.CallbackContext context)
    {
        if (aiState == AIState.AttackState)
        {
            JumpTimes++;
            if (JumpTimes >= RequiredJumpTimes)
            {
                SetState(AIState.DeadState);
                transform.rotation = Quaternion.Euler(0,0,0);
                animator.SetTrigger("Die");
            }
        }
    }
}
