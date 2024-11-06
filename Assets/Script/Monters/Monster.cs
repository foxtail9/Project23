using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public MonsterData data;
    protected GameObject player;
    protected PlayerConditions conditions;
    protected Rigidbody rgbd;
    protected Animator animator;
    protected int curHealth;
    protected AIState aiState;
    protected float playerDistance;



    private void Awake()
    {
        rgbd = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        curHealth = data.health;      
    }

    public virtual void Start()
    {
        player = GameManager.Instance.Player.gameObject;
        conditions = player.GetComponent<PlayerConditions>();
    }


    public bool RecognizePlayer()
    {
        bool recognize = playerDistance <= data.recognizeDistance;
        if (recognize)
        {
           conditions.curValueMental -= data.mentalDamage * Time.deltaTime * 0.1f;
        }
        return recognize;
    }
    public void Attack()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance <= data.attackDistance)
        {
            conditions.curValueHP -= data.attackDamage * GameManager.Instance.monsterDamageRate; 
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
