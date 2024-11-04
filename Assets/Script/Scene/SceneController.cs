using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void HospitalSceneLoad()
    {
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
            Application.OpenURL(https://spartacodingclub.kr/);
#else
            Application.Quit();
#endif
    }
}
