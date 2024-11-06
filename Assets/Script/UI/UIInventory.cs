using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIInventory : MonoBehaviour
{
    public ItemSlot[] slots;

    public GameObject inventoryWindow;
    public Transform slotPannel;
    public Transform dropPos;

    [Header("Select Item")]
    public TextMeshProUGUI selectedItemName;
    public TextMeshProUGUI selectedItemDescription;
    public TextMeshProUGUI selectedItemSellPrice;
    public GameObject dropButton;

    private PlayerController controller;
    private PlayerConditions conditions;

    ItemData selectedItem;
    int selectedItemIndex = 0;

    private void Awake()
    {
        if (GameManager.Instance.inventory != null)
        {
            Destroy(gameObject);
            return;
        }
        GameManager.Instance.Inventory = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        controller = GameManager.Instance.Player.controller;
        conditions = GameManager.Instance.Player.condition;
        dropPos = GameManager.Instance.Player.dropPos;
        

        controller.UiInventory += Toggle;
        GameManager.Instance.Player.addItem += AddItem;

        inventoryWindow.SetActive(false);
        slots = new ItemSlot[slotPannel.childCount];

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = slotPannel.GetChild(i).GetComponent<ItemSlot>();
            slots[i].index = i;
            slots[i].inventory = this;
        }

        ClearSelectedItemWindow();
    }


    void ClearSelectedItemWindow()
    {
        selectedItemName.text = string.Empty;
        selectedItemDescription.text = string.Empty;
        selectedItemSellPrice.text = string.Empty;

        dropButton.SetActive(false);
    }

    public void Toggle()
    {
        if (IsOpen())
            inventoryWindow.SetActive(false);
        else
            inventoryWindow.SetActive(true);
    }

    public bool IsOpen()
    {
        return inventoryWindow.activeInHierarchy;
    }

    void AddItem()
    {
        ItemData data = GameManager.Instance.Player.itemData;

        ItemSlot emptySlot = GetEmptySlot();

        if (emptySlot != null)
        {
            emptySlot.item = data;
            emptySlot.quantity = 1;
            UpdateUI();
            GameManager.Instance.Player.itemData = null;
            return;
        }
        ThrowItem(data);
        GameManager.Instance.Player.itemData = null;
    }

    public void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                slots[i].Set();
            }
            else
            {
                slots[i].Clear();
            }
        }
    }

    ItemSlot GetItemSlot(ItemData data)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == data)
            {
                return slots[i];
            }
        }
        return null;
    }

    ItemSlot GetEmptySlot()
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

    void ThrowItem(ItemData data)
    {
        Instantiate(data.dropPrefab, dropPos.position, Quaternion.Euler(Vector3.one * Random.value * 360));
    }

    public void SelectItem(int index)
    {
        if (slots[index].item == null) return;

        selectedItem = slots[index].item;
        selectedItemIndex = index;

        selectedItemName.text = selectedItem.displayName;
        selectedItemDescription.text = selectedItem.description;
        selectedItemSellPrice.text = $"�ǸŰ� : {selectedItem.sellPrice}$";

        dropButton.SetActive(true);
    }

    /*public void OnUseButton()
    {
        if (selectedItem.type == ItemType.Consumable)
        {
            for (int i = 0; i < selectedItem.consumables.Length; i++)
            {
                switch (selectedItem.consumables[i].type)
                {
                    case ConsumableType.Health:
                        conditions.Heal(selectedItem.consumables[i].value);
                        break;
                    case ConsumableType.Hunger:
                        conditions.Eat(selectedItem.consumables[i].value);
                        break;
                    default:
                        break;
                }
            }
            ReMoveSelectedItem();
        }
    }*/

    public void OnDropButton()
    {
        ThrowItem(selectedItem);
        ReMoveSelectedItem();
    }

    void ReMoveSelectedItem()
    {
        slots[selectedItemIndex].quantity--;

        if (slots[selectedItemIndex].quantity <= 0)
        {
            selectedItem = null;
            slots[selectedItemIndex].item = null;
            selectedItemIndex--;
            ClearSelectedItemWindow();
        }

        UpdateUI();
    }
}
