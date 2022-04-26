using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI highScoreText;

    private void Start()
    {
        highScoreText.text = GameManager.HighScore.ToString();
    }

    public void StartGame()
    {
        AudioManager.Instance.Play("ClickButton");
        SceneManager.LoadScene(1);
    }

    public void PlayAgain()
    {
        AudioManager.Instance.Play("ClickButton");
        TargetSpawner.TargetsOnGame = 0; 
        GameManager.GameOver = false;
        GameManager.GameStarted = true; 
        SceneManager.LoadScene(1);
    }
}
