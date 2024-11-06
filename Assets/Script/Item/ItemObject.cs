using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public interface IInteractable
{
    public string GetInteractPrompt();
    public void OnInteract();
}
public class ItemObject : MonoBehaviour, IInteractable
{
    public ItemData data;

    public string GetInteractPrompt()
    {
        string str = $"{data.displayName}\n{data.description}";
        if (data.buyPrice > 0)
            str += $"\n{data.buyPrice}$";
        return str;
    }

    public void OnInteract()
    {
        GameManager.Instance.Player.itemData = data;
        if (GameManager.Instance.Player.itemData.cnaEquip == true)
        {
            if (GameManager.Instance.EquipInventory.slots.Take(4).Any(slot => slot.item == null))
            {
                GameManager.Instance.Player.addEquip?.Invoke();
                Debug.Log("장착 아이템");
            }
            else
            {
                Debug.Log("가득 찼습니다.");
                return;
            }
        }
        else
        {
            GameManager.Instance.Player.addItem?.Invoke();
            Debug.Log("자원 아이템");
        }
        Destroy(gameObject);
    }
}
