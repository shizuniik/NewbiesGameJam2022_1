using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO; 

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

    private static SavedData dataInfo;

    private float offset = 10f;

    private void Awake()
    {
        Level = 1;
        Score = 0;

        LoadInfo(); 
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

            SaveInfo(); 
        }
    }

    private void UpdateLevel()
    {
        if (Level < Mathf.FloorToInt(Score / pointsToUpdLevel) + 1)
        {
            AudioManager.Instance.Play("ChangeLevel"); 
            Level = Mathf.FloorToInt(Score / pointsToUpdLevel) + 1;
            OnChangeLevel?.Invoke();

            PowerupSpawn(); 
        }
    }

    private void GameOverScene()
    {
        SceneManager.LoadScene(2); 
    }

    [System.Serializable]
    class SavedData
    {
        public int highScore; 
    }

    public void SaveInfo()
    {
        SavedData data; 
        if (dataInfo == null)
        {
            data = new SavedData();
        }
        else
        {
            data = dataInfo;  
        }

        data.highScore = HighScore;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json",json); 
    }

    public void LoadInfo()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SavedData data = JsonUtility.FromJson<SavedData>(json);

            HighScore = data.highScore;
            dataInfo = data;
            //Debug.Log(Application.persistentDataPath); 
        }
    }

    private void PowerupSpawn()
    {
        Vector3 pos = new Vector3(Random.Range(Bounds.MinX + offset, Bounds.MaxX - offset), Bounds.MaxY,
            Random.Range(Bounds.MinZ + offset, Bounds.MaxZ - offset));

        GameObject powerup = ObjectPoolManager.SharedInstance.SpawnFromPool("Powerup", pos, Quaternion.identity);
    }
}
