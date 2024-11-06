using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class config : MonoBehaviour
{
    public float startTime;

    public float SettingTime
    {
        set { startTime = value; }
    }
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}