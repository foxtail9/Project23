using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConditions : MonoBehaviour
{
    private Player player;

    public float curValueHP;
    public float maxValueHP;

    public float curValueStamina;
    public float maxValueStamina;
    public float passiveValueStamina;
    public bool is_Tired = false;


    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();

        curValueHP = maxValueHP;
        curValueStamina = maxValueStamina;
    }

    void Update()
    {
        if (curValueStamina < maxValueStamina)
            curValueStamina = Add(curValueStamina, passiveValueStamina * Time.deltaTime);

        if(player.controller.isRunning == true)
            curValueStamina = Subtract(curValueStamina, passiveValueStamina * 5 * Time.deltaTime);
    }

    private void LateUpdate()
    {
        if (curValueStamina <= 0)
        {
            Debug.Log("달릴수 없다.");
            Invoke("Tired_End",3f);
            is_Tired = true;
            player.controller.isRunning = false;
            player.controller.moveSpeed = 5;
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

    public float Subtract(float parent, float value)
    {
        parent -= value;
        return parent;
    }
}
