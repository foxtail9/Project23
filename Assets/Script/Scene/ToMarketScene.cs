using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToMarketScene : MonoBehaviour, IInteractable
{
    public string GetInteractPrompt()
    {
        return "�������� �̵��մϴ�";
    }

    public void OnInteract()
    {
        SceneManager.LoadScene("Market");
    }
}
