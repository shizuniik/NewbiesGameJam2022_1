using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    ObjectPoolManager objectPoolManager; 

    // Start is called before the first frame update
    void Start()
    {
        objectPoolManager = ObjectPoolManager.SharedInstance;

        InvokeRepeating("Spawn", 0, 1f); 
    }

    private void FixedUpdate()
    {
        
    }

    private void Spawn()
    {
        GameObject ta = objectPoolManager.SpawnFromPool("TargetA", transform.position, Quaternion.identity);

        ta.GetComponent<Rigidbody>().AddForce(Vector3.up, ForceMode.Impulse); 

        GameObject tb = objectPoolManager.SpawnFromPool("TargetB", transform.position, Quaternion.identity);

        tb.GetComponent<Rigidbody>().AddForce(Vector3.down, ForceMode.Impulse);
    }



}
