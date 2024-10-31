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
    private AIState state;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveToPlayer();
        AttackState();
        
    }

    public void AttackState()
    {
        if (Time.time - lastAttackTime > data.attackRate)
        {
            lastAttackTime = Time.time;
            // player takes damage
            Attack();
        }
    }
}
