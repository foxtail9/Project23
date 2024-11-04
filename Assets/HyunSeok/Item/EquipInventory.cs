using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipInventory : MonoBehaviour
{
    public EquipSlot[] slots;

    private PlayerController controller;
    private PlayerConditions conditions;

    ItemData selectedItem;
    int selectedItemIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        controller = CharacterManager.Instance.Player.controller;
        conditions = CharacterManager.Instance.Player.condition;

        CharacterManager.Instance.Player.addEquip += AddEquip;

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
        ItemData data = CharacterManager.Instance.Player.itemData;
        EquipSlot emptySlot = GetEmptySlot();

        if (emptySlot != null)
        {
            emptySlot.item = data;
            UpdateUI();
            CharacterManager.Instance.Player.itemData = null;
            return;
        }
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                slots[i].icon.color = new Color(1,1,1,1);
                slots[i].Set();
            }
            else
            {
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
