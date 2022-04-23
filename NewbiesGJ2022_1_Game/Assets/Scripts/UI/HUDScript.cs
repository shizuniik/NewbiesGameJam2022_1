using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class HUDScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI highScoreText;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI instructionText;

    private void Awake()
    {
        HighScoreTextUpdate();
    }

    private void Start()
    {
        if(GameManager.GameStarted == false) { HideInstructions(false);  }
    }

    private void HideInstructions(bool hide)
    {
        instructionText.gameObject.SetActive(!hide);
    }

    private void OnEnable()
    {
        GameManager.OnChangeScore += ScoreTextUpdate;
        GameManager.OnChangeHighScore += HighScoreTextUpdate;
        GameManager.OnChangeLevel += LevelTextUpdate;
    }

    private void OnDisable()
    {
        GameManager.OnChangeScore -= ScoreTextUpdate;
        GameManager.OnChangeHighScore -= HighScoreTextUpdate;
        GameManager.OnChangeLevel -= LevelTextUpdate;
    }

    private void ScoreTextUpdate()
    {
        if (GameManager.Score == 1) { HideInstructions(true); }

        scoreText.text = GameManager.Score.ToString();
    }

    private void HighScoreTextUpdate()
    {
        highScoreText.text = GameManager.HighScore.ToString();
    }

    private void LevelTextUpdate()
    {
        levelText.text = GameManager.Level.ToString();

        if (GameManager.Level == GameManager.Instance.MaxLevel) { SceneManager.LoadScene(3); }
    }

    public void PauseButton()
    {
        GameManager.GamePaused = !GameManager.GamePaused;
        Time.timeScale = GameManager.GamePaused ? 0 : 1;
    }
}
