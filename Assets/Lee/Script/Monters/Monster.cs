using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public MonsterData data;
    public GameObject player;
    Rigidbody rgbd;
    Animator animator;
    public bool RecognizePlayer()
    {
        float distance = Vector3.Distance(player.transform.position,transform.position);
        return distance <= data.recognizeDistance;
    }
    public void Attack()
    {

    }
    public void MoveToPlayer()
    {
        Vector3 dir = (player.transform.position - transform.position).normalized;
        transform.forward = dir;
        rgbd.velocity = dir * data.moveSpeed;
        animator.SetBool("isWalk", true);
    }
    public void TakeDamage()
    {

    }

    private void Awake()
    {
        rgbd = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }
}
