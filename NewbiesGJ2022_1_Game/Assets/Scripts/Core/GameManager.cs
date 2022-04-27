using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; 

public class GameManager : MonoBehaviour
{
    public int pointsToUpdLevel;
    public int MaxLevel; 

    public static int Score { get;  set; }
    public static int Level { get; private set; }
    public static int HighScore { get; private set; }
    public static bool GameOver { get; set; }
    public static bool GameStarted { get; set; }
    public static bool GamePaused { get; set; }

    public static GameManager Instance;

    public delegate void ChangeScore();
    public static event ChangeScore OnChangeScore;
    public static event ChangeScore OnChangeHighScore;
    public static event ChangeScore OnChangeLevel;

    private void Awake()
    {
        Level = 1;
        Score = 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); 
        }

        DontDestroyOnLoad(gameObject);  
    }

    private void OnEnable()
    {
        TargetSpawner.OnChangeGameStatus += GameOverScene;
    }

    private void OnDisable()
    {
        TargetSpawner.OnChangeGameStatus -= GameOverScene;
    }

    public static void UpdateScore(int points)
    {
        Score += points;
        OnChangeScore?.Invoke();

        GameManager.Instance.UpdateHighScore();
        GameManager.Instance.UpdateLevel();
    }

    private void UpdateHighScore()
    {
        if(HighScore < Score)
        {
            HighScore = Score;
            OnChangeHighScore?.Invoke();
        }
    }

    private void UpdateLevel()
    {
        if (Level < Mathf.FloorToInt(Score / pointsToUpdLevel) + 1)
        {
            AudioManager.Instance.Play("ChangeLevel"); 
            Level = Mathf.FloorToInt(Score / pointsToUpdLevel) + 1;
            OnChangeLevel?.Invoke();
        }
    }

    private void GameOverScene()
    {
        SceneManager.LoadScene(2); 
    }

}