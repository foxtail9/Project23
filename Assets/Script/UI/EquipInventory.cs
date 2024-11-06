using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipInventory : MonoBehaviour
{
    public EquipSlot[] slots;

    private PlayerController controller;
    private PlayerConditions conditions;

    public int selectedItemIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        controller = GameManager.Instance.Player.controller;
        conditions = GameManager.Instance.Player.condition;

        GameManager.Instance.Player.addEquip += AddEquip;
        GameManager.Instance.equipInventory = this;

        slots = new EquipSlot[gameObject.transform.childCount];

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = gameObject.transform.GetChild(i).GetComponent<EquipSlot>();
            slots[i].index = i;
            slots[i].inventory = this;
        }
    }

    void AddEquip()
    {
        ItemData data = GameManager.Instance.Player.itemData;
        EquipSlot emptySlot = GetEmptySlot();

        if (emptySlot != null)
        {
            emptySlot.item = data;
            UpdateUI();
            GameManager.Instance.Player.itemData = null;
            return;
        }
    }

    public void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                slots[i].icon.color = new Color(1, 1, 1, 1);
                slots[i].Set();
            }
            else
            {
                slots[i].icon.color = new Color(46 / 255f, 46 / 255f, 46 / 255f, 1);
                slots[i].Clear();
            }
        }
    }

    EquipSlot GetEmptySlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                return slots[i];
            }
        }
        return null;
    }
}
