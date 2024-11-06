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
        GameManager.Instance.Player.gameObject.transform.position = new Vector3(-2.4f, 0.8f, 1.8f);
        SceneManager.LoadScene("Market");
    }
}
