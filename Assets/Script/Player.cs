using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    public PlayerController controller;
    public PlayerConditions condition;
    public Equipment equipment;

    public ItemData itemData;

    public Action addEquip;
    public Action addItem;

    public Transform dropPos;
    public GameObject deathPage;
    private void Awake()
    {
        if (GameManager.Instance.Player != null)
        {
            Destroy(gameObject);
            return;
        }
        GameManager.Instance.Player = this;
        controller = GetComponent<PlayerController>();
        condition = GetComponent<PlayerConditions>();
        equipment = GetComponent<Equipment>();
    }
}
