using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipSlot : MonoBehaviour
{
    public ItemData item;

    public EquipInventory inventory;
    public int index;
    public Image icon;

    public Outline outline;

    private void Awake()
    {
        outline = GetComponent<Outline>();
    }

    public void Set()
    {
        icon.gameObject.SetActive(true);
        icon.sprite = item.icon;
        //icon.color = new Color(1, 1, 1, 1);
    }

    public void Clear()
    {
        item = null;
        //icon.gameObject.SetActive(false);
    }
}
