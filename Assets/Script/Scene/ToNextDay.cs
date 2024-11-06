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
            // to ending
        }
        else
        {
            GameManager.Instance.Player.gameObject.transform.position = new Vector3(0f,1.4f,0f);
            SceneManager.LoadScene("ClosedHospital");
        }
    }

    
}
