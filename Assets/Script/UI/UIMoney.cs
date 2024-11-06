using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIMoney : MonoBehaviour
{
    TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }
    void Start()
    {
        text.text = $"money:{GameManager.Instance.money}";
    }
    void UIMoneyUpdate()
    {
        text.text = $"money:{GameManager.Instance.money}";

    }


}
