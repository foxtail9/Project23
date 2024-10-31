using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConditions : MonoBehaviour
{
    public float curValueHP;
    public float maxValueHP;

    public float curValueStamina;
    public float maxValueStamina;
    public float passiveValueStamina;

    // Start is called before the first frame update
    void Start()
    {
        curValueHP = maxValueHP;
        curValueStamina = maxValueStamina;
    }

    // Update is called once per frame
    void Update()
    {
        //if(curValueStamina < maxValueStamina)
            
    }

    public void Add(float value)
    {

    }
}
