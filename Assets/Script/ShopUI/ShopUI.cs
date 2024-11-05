using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        GameManager.Instance.Player.controller.ToggleCursor();
    }
    private void OnDisable()
    {
        GameManager.Instance.Player.controller.ToggleCursor();
    }

}
