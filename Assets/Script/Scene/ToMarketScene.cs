using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToMarketScene : MonoBehaviour, IInteractable
{
    public string GetInteractPrompt()
    {
        return "전당포로 이동합니다";
    }

    public void OnInteract()
    {
        SceneManager.LoadScene("Market");
    }
}
