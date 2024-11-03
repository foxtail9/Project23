using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameTime : MonoBehaviour
{
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

    // �� �����Ӹ��� ȣ���Ͽ� �ð��� ������Ʈ�ϴ� �޼���
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

        // ���� ���� �ð��� 24�ð��� �ʰ����� �ʵ��� ����
        if (gameWorldTime >= 24f)
        {
            gameWorldTime -= 24f;
        }
    }

    // ���� ���� �ð��� ������ �� �ִ� �޼��� (�׽�Ʈ �Ǵ� �ʱ�ȭ �� ���)
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

    // UI �ؽ�Ʈ�� �����ϴ� �޼���
    private void UpdateTimeText()
    {
        if (timeText != null)
        {
            // �ð� ������ "HH:MM"���� ��ȯ�Ͽ� �ؽ�Ʈ�� ǥ��
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
    }

    void Update()
    {
        UpdateGameWorldTime();
    }
}
