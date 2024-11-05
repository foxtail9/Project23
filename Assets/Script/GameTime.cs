using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameTime : MonoBehaviour
{
    public static int startTime;  //신컨트롤러에서 받아오며 7이면 am7시이고 21이면 pm9시입니다.

    private float gameWorldTime;
    private float realTimeElapsed;
    private const float TimeIncrement = 0.25f;
    private const float RealMinute = 60f;
    

    [SerializeField]
    private TextMeshProUGUI timeText;

    public GameTime()
    {
        gameWorldTime = 0f;
        realTimeElapsed = 0f;
    }
    public float GameWorldTime
    {
        get { return gameWorldTime; }
    }

    // 매 프레임마다 호출하여 시간을 업데이트하는 메서드
    public void UpdateGameWorldTime()
    {
        realTimeElapsed += Time.deltaTime;

        if (realTimeElapsed >= RealMinute)
        {
            IncrementGameWorldTime();
            realTimeElapsed = 0f; 
            UpdateTimeText(); 
        }
    }

    private void IncrementGameWorldTime()
    {
        gameWorldTime += TimeIncrement;
        if (gameWorldTime == GameManager.Instance.monsterSpawnTime)
        {
            // TODO : 
        }

        if (gameWorldTime == GameManager.Instance.monsterDamageTime)
        {
            GameManager.Instance.monsterDamageRate = 2;
        }

        // 게임 월드 시간이 24시간을 초과하지 않도록 조정
        if (gameWorldTime >= 24f)
        {
            gameWorldTime -= 24f;
        }
    }

    // 게임 월드 시간을 설정할 수 있는 메서드 (테스트 또는 초기화 시 사용)
    public void SetGameWorldTime(float newTime)
    {
        if (newTime >= 0f && newTime < 24f)
        {
            gameWorldTime = newTime;
            UpdateTimeText(); 
        }
        else
        {
            Debug.LogWarning("Invalid time. Game world time must be between 0 and 24.");
        }
    }

    // UI 텍스트를 갱신하는 메서드
    private void UpdateTimeText()
    {
        if (timeText != null)
        {
            // 시간 형식을 "HH:MM"으로 변환하여 텍스트에 표시
            int hours = Mathf.FloorToInt(gameWorldTime);
            int minutes = Mathf.FloorToInt((gameWorldTime - hours) * 60);
            timeText.text = $"{hours:00}:{minutes:00}";
        }
        else
        {
            Debug.LogWarning("Time TextMeshProUGUI is not assigned.");
        }
    }

    private void Awake()
    {
        SetGameWorldTime(7f);
        GameManager.Instance.monsterDamageRate = 1;
    }

    void Update()
    {
        UpdateGameWorldTime();
    }
}
