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
        if (data.price > 0)
            str += $"\n{data.price}$";
        return str;
    }

    public void OnInteract()
    {
        CharacterManager.Instance.Player.itemData = data;
        if (CharacterManager.Instance.Player.itemData.cnaEquip == true)
        {
            if (CharacterManager.Instance.Player.equipInventory.slots.Take(4).Any(slot => slot.item == null))
            {
                CharacterManager.Instance.Player.addEquip?.Invoke();
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
            CharacterManager.Instance.Player.addItem?.Invoke();
            Debug.Log("자원 아이템");
        }
        Destroy(gameObject);
    }
}
