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
        GameManager.Instance.Player.gameObject.transform.position = new Vector3(-2.4f, 0.8f, 1.8f);
        SceneManager.LoadScene("Market");
    }
}
