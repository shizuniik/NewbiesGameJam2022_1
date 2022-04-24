using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    ObjectPoolManager objectPoolManager; 

    public static int TargetsOnGame { get; set; }

    public delegate void ChangeTargetsQty();
    public static event ChangeTargetsQty OnChangeTargetsQty;
    public static event ChangeTargetsQty OnChangeGameStatus;

    [SerializeField] float xRange;
    [SerializeField] float zMinValue;
    [SerializeField] float zMaxValue;
    [SerializeField] float initialSpawnRate; 

    // Start is called before the first frame update
    void Start()
    {
        objectPoolManager = ObjectPoolManager.SharedInstance;
        //InvokeRepeating("Spawn", 0, initialSpawnRate); 
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
        Debug.Log("check game over: " + TargetsOnGame + " poitns to up level: " + GameManager.Instance.pointsToUpdLevel);
        if (TargetsOnGame > GameManager.Instance.pointsToUpdLevel)
        {
            GameManager.GameOver = true;

            OnChangeGameStatus?.Invoke();
        }
    }

    private Vector3 randomPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), transform.position.y, Random.Range(zMinValue, zMaxValue)); 
    }

    private void WarningNearGameOver() // COMPLETAR 
    {
        if (TargetsOnGame > GameManager.Instance.pointsToUpdLevel * 0.8)
        {
            Debug.Log("Hurry up!"); 
        }
        else
        {
            Debug.Log(TargetsOnGame);
        }
    }

    private void AdaptSpawnRate()
    {
        //initialSpawnRate -= 0.1f; 
    }

    IEnumerator SpawnCoroutine()
    {
        while (!GameManager.GameOver)
        {
            GameObject ta = objectPoolManager.SpawnFromPool("TargetA", randomPos(), Quaternion.identity);
            TargetsOnGame++;
            GameObject tb = objectPoolManager.SpawnFromPool("TargetB", randomPos(), Quaternion.identity);
            TargetsOnGame++;

            OnChangeTargetsQty?.Invoke();

            //WarningNearGameOver();
            //CheckGameOver();
 
            Debug.Log(TargetsOnGame + " spawn rate: " + initialSpawnRate);
            yield return new WaitForSeconds(initialSpawnRate);

        }

    }

    private void Spawn()
    {
        GameObject ta = objectPoolManager.SpawnFromPool("TargetA", randomPos(), Quaternion.identity);
        TargetsOnGame++;
        GameObject tb = objectPoolManager.SpawnFromPool("TargetB", randomPos(), Quaternion.identity);
        TargetsOnGame++;

        OnChangeTargetsQty?.Invoke();

        WarningNearGameOver();
        CheckGameOver();
    }

}
