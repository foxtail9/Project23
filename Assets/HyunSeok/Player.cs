using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController controller;
    public PlayerConditions condition;

    private void Awake()
    {
        controller = GetComponent<PlayerController>();
        condition = GetComponent<PlayerConditions>();
    }
}
