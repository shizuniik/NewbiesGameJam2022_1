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

    // Start is called before the first frame update
    void Start()
    {
        objectPoolManager = ObjectPoolManager.SharedInstance;

        InvokeRepeating("Spawn", 0,2f);

        TargetsOnGame = 0; 
    }

    private void Spawn()
    {
        GameObject ta = objectPoolManager.SpawnFromPool("TargetA", transform.position, Quaternion.identity);
        TargetsOnGame++;
        GameObject tb = objectPoolManager.SpawnFromPool("TargetB", transform.position, Quaternion.identity);
        TargetsOnGame++;

        OnChangeTargetsQty?.Invoke(); 

        if(TargetsOnGame > GameManager.Instance.pointsToUpdLevel)
        {
            GameManager.GameOver = true;

            OnChangeGameStatus?.Invoke();
        }
    }



}
