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
    [SerializeField] TextMeshProUGUI lastLevelText; 
    public GameObject winExplosionParticle;
   
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
        if (GameManager.Score > 0) { HideInstructions(true); }

        scoreText.text = GameManager.Score.ToString();
    }

    private void HighScoreTextUpdate()
    {
        highScoreText.text = GameManager.HighScore.ToString();
    }

    private void LevelTextUpdate()
    {
        levelText.text = GameManager.Level.ToString();

        CheckWinGame(); 
    }

    public void PauseButton()
    {
        AudioManager.Instance.Play("ClickButton"); 
        GameManager.GamePaused = !GameManager.GamePaused;
        Time.timeScale = GameManager.GamePaused ? 0 : 1;
    }

    private void CheckWinGame()
    {
        if (GameManager.Level == GameManager.Instance.MaxLevel)
        {
            Vector3 pos = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0); 
            GameManager.GameOver = true; 
            GameObject explosion = Instantiate(winExplosionParticle, pos, Quaternion.identity);
            Destroy(explosion, 2f);
            AudioManager.Instance.Play("MassiveExplosion");

            foreach (Target t in FindObjectsOfType<Target>())
            {
                t.gameObject.SetActive(false);
            }

            Invoke("LoadWinScene", 1.5f);
        }

        if (GameManager.Level == GameManager.Instance.MaxLevel - 1)
        {
            lastLevelText.gameObject.SetActive(true);
        }
        else
        {
            lastLevelText.gameObject.SetActive(false);
        }
    }

    private void LoadWinScene()
    {
        AddExtraPoints(); 
        AudioManager.Instance.Play("WinSound");
        SceneManager.LoadScene(3);
    }

    private void AddExtraPoints()
    {
        foreach (GameObject powerup in ObjectPoolManager.SharedInstance.poolDictionary["Powerup"])
        {
            if (powerup.activeInHierarchy)
            {
                GameManager.UpdateScore(powerup.GetComponent<Powerup>().extraPoints + 5);
                Debug.Log(GameManager.Score);
            }
        }
    }
}
