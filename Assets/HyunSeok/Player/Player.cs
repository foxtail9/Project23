using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    public PlayerController controller;
    public PlayerConditions condition;
    public EquipInventory equipInventory;
    public Equipment equipment;

    public ItemData itemData;

    public Action addEquip;
    public Action addItem;

    private void Awake()
    {
        CharacterManager.Instance.Player = this;
        controller = GetComponent<PlayerController>();
        condition = GetComponent<PlayerConditions>();
        equipment = GetComponent<Equipment>();
    }
}
