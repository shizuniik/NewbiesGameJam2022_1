using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class QuitApp : MonoBehaviour
{
    public void QuitButton()
    {
        AudioManager.Instance.Play("ClickButton");

        SceneManager.LoadScene(0);

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
