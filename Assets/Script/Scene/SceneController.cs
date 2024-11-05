using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public GameObject playbtn;
    public GameObject btnAM7;
    public GameObject btnPM9;

    private DayAndNight dayAndNight; // DayAndNight 클래스의 인스턴스

    private void Start()
    {
        dayAndNight = FindObjectOfType<DayAndNight>();
    }

    public void TimeChoiceOn()
    {
        playbtn.SetActive(false);
        btnAM7.SetActive(true);
        btnPM9.SetActive(true);
    }

    public void HospitalSceneLoad_AM7()
    {
        SceneManager.LoadScene("ClosedHospital");
        if (dayAndNight != null) // null 체크 후 startTime 설정
        {
            dayAndNight.StartTime = 7;
        }
    }

    public void HospitalSceneLoad_PM9()
    {
        SceneManager.LoadScene("ClosedHospital");
        if (dayAndNight != null) // null 체크 후 startTime 설정
        {
            dayAndNight.StartTime = 21;
        }
    }

    public void TitleSceneLoad()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void ExitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
        Application.OpenURL("https://spartacodingclub.kr/");
#else
        Application.Quit();
#endif
    }
}
