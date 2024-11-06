using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToNextDay : MonoBehaviour,IInteractable
{
    public string GetInteractPrompt()
    {
        return "다음날로 이동";
    }

    public void OnInteract()
    {
        GameManager.Instance.dayCount++;
        if (GameManager.Instance.dayCount ==4)
        {
            SceneManager.LoadScene("Ending");
        }
        else
        {
            for (int i = 0; i < GameManager.Instance.inventory.slots.Length; i++)
            {
                if (GameManager.Instance.inventory.slots[i] == null) break;
                GameManager.Instance.inventory.slots[i].item = null;
            }
            GameManager.Instance.Player.condition.PlayerReset();
            GameManager.Instance.inventory.UpdateUI();
            GameManager.Instance.Player.gameObject.transform.position = new Vector3(0f,1.4f,0f);
            SceneManager.LoadScene("ClosedHospital");
        }
    }

    
}
