using UnityEngine;
using TMPro;

public class GameTime : MonoBehaviour
{
    public TextMeshProUGUI gameTimeText;
    private float currentGameHour;
    private float timeScale = 1f;

    private config setTime;
    private DayAndNight dayAndNight;

    private void Awake()
    {
        setTime = FindObjectOfType<config>();

        if (setTime != null)
        {
            currentGameHour = setTime.startTime;
        }

        dayAndNight = FindObjectOfType<DayAndNight>();
        if (dayAndNight != null)
        {
            dayAndNight.CurrentGameHour = currentGameHour;
        }
    }

    private void Update()
    {
        UpdateGameHour();
    }

    private void UpdateGameHour()
    {
        currentGameHour += Time.deltaTime * timeScale;
        if (currentGameHour >= 24f)
        {
            currentGameHour -= 24f;
        }

        int hours = Mathf.FloorToInt(currentGameHour);
        int minutes = Mathf.FloorToInt((currentGameHour - hours) * 60);
        gameTimeText.text = $"{hours:D2}:{minutes:D2}";

        if (dayAndNight != null)
        {
            dayAndNight.CurrentGameHour = currentGameHour;
        }
    }
}
