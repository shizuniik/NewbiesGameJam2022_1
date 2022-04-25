using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class TargetSpawner : MonoBehaviour
{
    ObjectPoolManager objectPoolManager; 

    public static int TargetsOnGame { get; set; }

    public delegate void ChangeTargetsQty();
    public static event ChangeTargetsQty OnChangeTargetsQty;
    public static event ChangeTargetsQty OnChangeGameStatus;

    [SerializeField] float initialSpawnRate;
    [SerializeField] TextMeshProUGUI warningText;
    [SerializeField] float offset; 

    // Start is called before the first frame update
    void Start()
    {
        objectPoolManager = ObjectPoolManager.SharedInstance;
        StartCoroutine("SpawnCoroutine"); 
        TargetsOnGame = 0; 
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
        if (TargetsOnGame > GameManager.Instance.pointsToUpdLevel)
        {
            GameManager.GameOver = true;

            OnChangeGameStatus?.Invoke();
        }
    }

    private Vector3 randomPos()
    {
        return new Vector3(Random.Range(Bounds.MinX + offset, Bounds.MaxX - offset), transform.position.y, 
            Random.Range(Bounds.MinZ + offset, Bounds.MaxZ - offset)); 
    }

    private void WarningNearGameOver() // COMPLETAR 
    {
        if (TargetsOnGame > GameManager.Instance.pointsToUpdLevel * 0.8)
        {
            warningText.gameObject.SetActive(true); 
        }
        else
        {
            warningText.gameObject.SetActive(false); 
        }
    }

    private void AdaptSpawnRate()
    {
        initialSpawnRate -= 0.1f; 
    }

    IEnumerator SpawnCoroutine()
    {
        while (!GameManager.GameOver)
        {
            GameObject ta = objectPoolManager.SpawnFromPool("TargetA", randomPos(), Quaternion.identity);
            TargetsOnGame++;
            GameObject tb = objectPoolManager.SpawnFromPool("TargetB", randomPos(), Quaternion.identity);
            TargetsOnGame++;
            GameObject tc = objectPoolManager.SpawnFromPool("TargetC", randomPos(), Quaternion.identity);
            TargetsOnGame++;

            OnChangeTargetsQty?.Invoke();

            WarningNearGameOver();
            CheckGameOver();

            yield return new WaitForSeconds(initialSpawnRate);
        }
    }

}
