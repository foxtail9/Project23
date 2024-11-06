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

    private config GameTime; 

    private void Start()
    {
        GameTime = FindObjectOfType<config>();
    }

    public void TimeChoiceOn()
    {
        playbtn.SetActive(false);
        btnAM7.SetActive(true);
        btnPM9.SetActive(true);
    }

    public void HospitalSceneLoad_AM7()
    {
        GameTime.SettingTime = 7f;
        SceneManager.LoadScene("ClosedHospital");
    }

    public void HospitalSceneLoad_PM9()
    {
        GameTime.SettingTime = 21f;
        SceneManager.LoadScene("ClosedHospital");
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
