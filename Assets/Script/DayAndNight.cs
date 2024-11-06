using UnityEngine;
using TMPro;

public class DayAndNight : MonoBehaviour
{
    private float currentGameHour;

    public float CurrentGameHour
    {
        get { return currentGameHour; }
        set { currentGameHour = value; }
    }


    private void Update()
    {
        UpdateFogDensity();
    }

    private void UpdateFogDensity()
    {
        if (currentGameHour >= 7f && currentGameHour < 11f)
        {
            RenderSettings.fogDensity = 0.08f; // 07:00 ~ 11:00
        }
        else if (currentGameHour >= 11f && currentGameHour < 16f)
        {
            RenderSettings.fogDensity = 0.05f; // 11:00 ~ 16:00
        }
        else if (currentGameHour >= 16f && currentGameHour < 18f)
        {
            RenderSettings.fogDensity = 0.08f; // 16:00 ~ 18:00
        }
        else if (currentGameHour >= 18f && currentGameHour < 20f)
        {
            RenderSettings.fogDensity = 0.13f; // 18:00 ~ 20:00
        }
        else // 20:00 ~ 07:00
        {
            RenderSettings.fogDensity = 0.23f; // 20:00 ~ 07:00
        }
    }
}
