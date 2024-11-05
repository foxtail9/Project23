using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopNPC : MonoBehaviour, IInteractable
{
    public GameObject ShopUI;

    public string GetInteractPrompt()
    {
        return "������ ���ϴ�";
    }

    public void OnInteract()
    {
        ShopUI.SetActive(true);
    }

}
