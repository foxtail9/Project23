using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipHealth : Equip
{
    public override void OnLeftClickInput()
    {
        GameManager.Instance.Player.condition.curValueHP = Mathf.Min(
    GameManager.Instance.Player.condition.curValueHP + 30f,
    GameManager.Instance.Player.condition.maxValueHP);
        GameManager.Instance.EquipInventory.slots[GameManager.Instance.EquipInventory.selectedItemIndex].item = null;
        GameManager.Instance.EquipInventory.UpdateUI();
        Destroy(gameObject);
    }
}
