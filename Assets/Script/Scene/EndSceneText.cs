using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndSceneText : MonoBehaviour
{

    public TextMeshProUGUI endText;

    private void Awake()
    {
        endText = GetComponent<TextMeshProUGUI>();

    }
    void Start()
    {
        endText.text = $"{GameManager.Instance.money}$";

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
