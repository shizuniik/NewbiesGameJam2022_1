using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI highScoreText;
    [SerializeField] TextMeshProUGUI levelText;

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
        scoreText.text = GameManager.Score.ToString();
    }

    private void HighScoreTextUpdate()
    {
        highScoreText.text = GameManager.HighScore.ToString();
    }

    private void LevelTextUpdate()
    {
        levelText.text = GameManager.Level.ToString();
    }

}
