using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SellPanel : MonoBehaviour
{
    public int sumOfPrice = 0;
    public UIInventory inventory;
    public TextMeshProUGUI sellBill;
    public GameObject moneyText;
    private Gold gold;


    private void Awake()
    {
        gold = moneyText.GetComponent<Gold>();
    }
    private void OnEnable()
    {
        for (int i = 0; i <GameManager.Instance.inventory.slots.Length; i++)
        {
            if (GameManager.Instance.inventory.slots[i].item == null) break;
            sumOfPrice += GameManager.Instance.inventory.slots[i].item.sellPrice;
        }
        sellBill.text = $"소유한 모든 환금 아이템을 판매합니다. \n총 판매 가격 : {sumOfPrice}$";
    }

    public void SellAll()
    {
        for (int i = 0; i < GameManager.Instance.inventory.slots.Length; i++)
        {
            if (GameManager.Instance.inventory.slots[i] == null) break;
            GameManager.Instance.inventory.slots[i].item = null;
        }
        GameManager.Instance.inventory.UpdateUI();
        GameManager.Instance.money += sumOfPrice;
        sumOfPrice = 0;
        gold.UpdateMoney();
        gameObject.SetActive(false);
        gameObject.SetActive(true);
        sellBill.text = "판매가 완료되었습니다.";
    }


}
