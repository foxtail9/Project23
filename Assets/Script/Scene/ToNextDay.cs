using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToNextDay : MonoBehaviour,IInteractable
{
    public string GetInteractPrompt()
    {
        return "다음날로 이동";
    }

    public void OnInteract()
    {
        //gamemanager.ins.day ++
        // to Map
        
        throw new System.NotImplementedException();
    }

    
}
