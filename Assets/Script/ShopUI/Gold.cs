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
        goldText.text = $"���� �ݾ�: {GameManager.Instance.money}$";
    }

    public void UpdateMoney()
    {
        goldText.text = $"���� �ݾ�: {GameManager.Instance.money}$";
    }
}
