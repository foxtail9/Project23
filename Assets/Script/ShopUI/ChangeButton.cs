using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeButton : MonoBehaviour
{

    private bool isShopSell = true;
    public GameObject SellPanel;
    public GameObject BuyPanel;
    public GameObject ShopUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SellButton()
    {
        SellPanel.SetActive(true);
        BuyPanel.SetActive(false);
    }

    public void BuyButton()
    {
        SellPanel.SetActive(false);
        BuyPanel.SetActive(true);
    }

    public void ExitButton()
    {
        ShopUI.SetActive(false);
    }

}
