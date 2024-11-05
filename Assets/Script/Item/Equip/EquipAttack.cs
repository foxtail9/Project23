using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipAttack : Equip
{
    public float attackRate;
    private bool attacking;
    public float attackDistance;

    [Header("Combat")]
    public bool doesDealDamage;
    public int damage;

    public Animator animator;
    private Camera camera;

    private void Awake()
    {
        camera = Camera.main;
        animator = GetComponent<Animator>();
    }

    public override void OnLeftClickInput()
    {
        if (!attacking)
        {
            attacking = true;
            animator.SetTrigger("Attack");
            Invoke("OnCanAttack", attackRate);
        }
    }

    void OnCanAttack()
    {
        attacking = false;
    }
}
