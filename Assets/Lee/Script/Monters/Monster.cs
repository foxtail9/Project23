using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public MonsterData data;
    public GameObject player;
    protected Rigidbody rgbd;
    protected Animator animator;
    protected int curHealth;
    protected AIState aiState;


    private void Awake()
    {
        rgbd = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        curHealth = data.health;
    }


    public bool RecognizePlayer()
    {
        float distance = Vector3.Distance(player.transform.position,transform.position);
        return distance <= data.recognizeDistance;
    }
    public void Attack()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance <= data.attackDistance)
        {
            animator.SetTrigger("Attack");
        }
    }
    public void MoveToPlayer()
    {
        Vector3 dir = (player.transform.position - transform.position).normalized;
        transform.forward = dir;
        rgbd.velocity = dir * data.moveSpeed;
    }
    public void TakeDamage(int damage )
    {
        curHealth -= damage;
        if (curHealth <= 0) animator.SetTrigger("Die");
        aiState = AIState.DeadState;
    }
}
