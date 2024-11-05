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
        GameManager.Instance.Player.equipInventory.slots[GameManager.Instance.Player.equipInventory.selectedItemIndex].item = null;
        GameManager.Instance.Player.equipInventory.UpdateUI();
        Destroy(gameObject);
    }
}
