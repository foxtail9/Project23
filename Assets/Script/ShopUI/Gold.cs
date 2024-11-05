using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Gold : MonoBehaviour
{

    TextMeshProUGUI goldText;
    // Start is called before the first frame update
    void Start()
    {
        goldText = GetComponent<TextMeshProUGUI>();
        goldText.text = $"보유 금액: {GameManager.Instance.money}$";
    }

    public void UpdateMoney()
    {
        goldText.text = $"보유 금액: {GameManager.Instance.money}$";
    }
}
