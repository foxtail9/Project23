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

    public float curValueMental;
    public float maxValueMental;

    public GameObject gameOver;
    public event Action onTakeDamage;

    // Start is called before the first frame update
    void Start()
    {
        curValueHP = maxValueHP;
        curValueStamina = maxValueStamina;
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
            Debug.Log("달릴수 없다.");
            Invoke("Tired_End",3f);
            is_Tired = true;
            GameManager.Instance.Player.controller.isRunning = false;
            GameManager.Instance.Player.controller.moveSpeed = 5;
        }

        if (curValueHP <= 0)
        {
            Die_Page();
        }
    }

    void Tired_End()
    {
        Debug.Log("다시 달릴수 있다.");
        is_Tired = false;
    }

    public float Add(float parent, float value)
    {
        parent += value;
        return parent;
    }
    public bool OnJumpStaminaCost()
    {
        if(curValueStamina - 15f < 0)
        {
            return false;
        }
        curValueStamina = Subtract(curValueStamina, 15f);
        return true;
    }
    public float Subtract(float parent, float value)
    {
        parent -= value;
        return parent;
    }

    public void Die_Page()
    {
        gameOver.SetActive(true);
        GameManager.Instance.Player.controller.canLook = false;
        Time.timeScale = 0;
        Debug.Log("플레이어가 죽었다.");
    }

    public void TakePhysicalDamage(int damageAmount)
    {
        Subtract(curValueHP, damageAmount);
        onTakeDamage?.Invoke();
    }
}
