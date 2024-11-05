using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopNPC : MonoBehaviour, IInteractable
{
    public GameObject ShopUI;

    public string GetInteractPrompt()
    {
        return "상점을 엽니다";
    }

    public void OnInteract()
    {
        ShopUI.SetActive(true);
    }

}
