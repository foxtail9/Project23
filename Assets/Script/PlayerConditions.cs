using System.Collections.Generic;
using UnityEngine;
using System;

public interface IDamagable
{
    void TakePhysicalDamage(int damageAmount);
}
public class PlayerConditions : MonoBehaviour
{
    public float curValueHP;
    public float maxValueHP;

    public float curValueStamina;
    public float maxValueStamina;
    public float passiveValueStamina;
    public bool is_Tired = false;
    public bool is_Die = false;

    public float curValueMental;
    public float maxValueMental;

    public event Action onTakeDamage;

    // Start is called before the first frame update
    void Start()
    {
        PlayerReset();
    }

    public void PlayerReset()
    {
        curValueHP = maxValueHP;
        curValueStamina = maxValueStamina;
        curValueMental = maxValueMental;
        is_Die = false;
    }

    void Update()
    {
        if (curValueStamina < maxValueStamina)
            curValueStamina = Add(curValueStamina, passiveValueStamina * Time.deltaTime);

        if(GameManager.Instance.Player.controller.isRunning == true)
            curValueStamina = Subtract(curValueStamina, passiveValueStamina * 5 * Time.deltaTime);
    }

    private void LateUpdate()
    {
        if (curValueStamina <= 0)
        {
            Debug.Log("�޸��� ����.");
            Invoke("Tired_End",3f);
            is_Tired = true;
            GameManager.Instance.Player.controller.isRunning = false;
            GameManager.Instance.Player.controller.moveSpeed = 5;
        }

        if (curValueHP <= 0 && is_Die == false)
        {
            is_Die = true;
            Die_Page();
        }
    }

    void Tired_End()
    {
        Debug.Log("�ٽ� �޸��� �ִ�.");
        is_Tired = false;
    }

    public float Add(float parent, float value)
    {
        parent += value;
        return parent;
    }
    public bool OnJumpStaminaCost()
    {
        if(curValueStamina - 8f < 0)
        {
            return false;
        }
        return true;
    }
    public float Subtract(float parent, float value)
    {
        parent -= value;
        return parent;
    }

    public void Die_Page()
    {
        
        Invoke("NextDay", 0.5f);
    }

    void NextDay()
    {
        GameManager.Instance.Player.deathPage.gameObject.SetActive(true);
    }

    public void TakePhysicalDamage(int damageAmount)
    {
        Subtract(curValueHP, damageAmount);
        onTakeDamage?.Invoke();
    }
}
