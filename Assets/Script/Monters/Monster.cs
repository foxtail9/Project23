using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public MonsterData data;
    public GameObject player;
    public PlayerConditions conditions;
    protected Rigidbody rgbd;
    protected Animator animator;
    protected int curHealth;
    protected AIState aiState;


    private void Awake()
    {
        rgbd = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        conditions = player.GetComponent<PlayerConditions>();
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
            conditions.curValueHP -= data.attackDamage; 
            animator.SetTrigger("Attack");
        }
    }
    public void MoveToPlayer()
    {
        Vector3 dir = (player.transform.position - transform.position).normalized;
        rgbd.velocity = dir * data.moveSpeed;
        transform.forward = new Vector3(dir.x,0,dir.z).normalized;
    }
    public void TakeDamage(int damage )
    {
        curHealth -= damage;
        if (curHealth <= 0) animator.SetTrigger("Die");
        aiState = AIState.DeadState;
    }
}
