using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPage : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("다음날로 이동");
        }
    }
}
