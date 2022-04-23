using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; 

public class GameManager : MonoBehaviour
{
    [SerializeField] int pointsToUpdLevel;
    [SerializeField] TextMeshProUGUI instructionText; 

    public static int Score { get;  set; }
    public static int Level { get; private set; }
    public static int HighScore { get; private set; }
    public static bool GameOver { get; set; }
    public static bool GameStarted { get; set; }

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
       /* if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject); 
        }

        DontDestroyOnLoad(instance); */ 
    }

    // Update is called once per frame
    void Update()
    {
        if(Score == 1) { HideInstructions(true);  }

        UpdateHighScore();
        UpdateLevel(); 
    }

    private void HideInstructions(bool hide)
    {
        instructionText.gameObject.SetActive(!hide);  
    }

    public static void UpdateScore(int points)
    {
        Score += points;
        OnChangeScore?.Invoke(); 
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
        if (Score > pointsToUpdLevel)
        { 
            Level = Mathf.FloorToInt(Score / pointsToUpdLevel) + 1;
            OnChangeLevel?.Invoke();
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1); 
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(1); 
    }
}
