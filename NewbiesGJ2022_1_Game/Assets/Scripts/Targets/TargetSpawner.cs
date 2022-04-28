using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class TargetSpawner : MonoBehaviour
{
    ObjectPoolManager objectPoolManager; 

    public static int TargetsOnGame { get; set; }

    public delegate void ChangeTargetsQty();
    public static event ChangeTargetsQty OnChangeGameStatus;

    [SerializeField] float initialSpawnRate;
    [SerializeField] TextMeshProUGUI warningText;
    [SerializeField] float offset;

    public HealthBar healthBar;
    private int qtyTargetsToGameOver; 

    // Start is called before the first frame update
    void Start()
    {
        qtyTargetsToGameOver = GameManager.Instance.pointsToUpdLevel * 2;
        healthBar.SetMaxHealth(qtyTargetsToGameOver);

        objectPoolManager = ObjectPoolManager.SharedInstance;
        StartCoroutine("SpawnCoroutine"); 

        TargetsOnGame = 0;
    }

    private void Update()
    {
        Debug.Log(TargetsOnGame);
        healthBar.SetHealth(TargetsOnGame); 
    }

    private void OnEnable()
    {
        GameManager.OnChangeLevel += AdaptSpawnRate; 
    }

    private void OnDisable()
    {
        GameManager.OnChangeLevel -= AdaptSpawnRate; 
    }

    private void CheckGameOver()
    {
        if (TargetsOnGame > qtyTargetsToGameOver)
        {
            GameManager.GameOver = true;
            AudioManager.Instance.Play("GameOver"); 
            OnChangeGameStatus?.Invoke();
        }
    }

    private Vector3 randomPos()
    {
        return new Vector3(Random.Range(Bounds.MinX + offset, Bounds.MaxX - offset), transform.position.y, 
            Random.Range(Bounds.MinZ + offset, Bounds.MaxZ - offset)); 
    }

    private void WarningNearGameOver() 
    {
        if (TargetsOnGame > qtyTargetsToGameOver * 0.75f)
        {
            AudioManager.Instance.Play("Warning");
            warningText.gameObject.SetActive(true); 
        }
        else
        {
            warningText.gameObject.SetActive(false); 
        }
    }

    private void AdaptSpawnRate()
    {
        initialSpawnRate -= GameManager.Level < 15 ? 0.1f : 0.05f; 
    }

    IEnumerator SpawnCoroutine()
    {
        while (!GameManager.GameOver)
        {
            GameObject ta = objectPoolManager.SpawnFromPool("Target1", randomPos(), Quaternion.identity, false);
            TargetsOnGame++;
            GameObject tb = objectPoolManager.SpawnFromPool("Target2", randomPos(), Quaternion.identity, true);
            TargetsOnGame++;
            GameObject tc = objectPoolManager.SpawnFromPool("Target3", randomPos(), Quaternion.identity, false);
            TargetsOnGame++;

            WarningNearGameOver();
            CheckGameOver();

            yield return new WaitForSeconds(initialSpawnRate);
        }
    }

}
