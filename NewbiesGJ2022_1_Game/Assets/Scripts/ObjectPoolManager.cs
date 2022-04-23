using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager: MonoBehaviour 
{
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size; 
    }

    [SerializeField] List<Pool> pools;

    public static ObjectPoolManager SharedInstance;

    private void Awake()
    {
        SharedInstance = this; 
    }

    private void Start()
    {
        CreatePools(); 
    }

    private void CreatePools()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            if (poolDictionary.ContainsKey(pool.tag))
            {
                Debug.LogError("Repeated tag in pools list!");
            }
            else
            {
                Queue<GameObject> objPool = new Queue<GameObject>();
                for (int i = 0; i < pool.size; i++)
                {
                    GameObject obj = Instantiate(pool.prefab, transform.position, Quaternion.identity);
                    obj.SetActive(false);
                    objPool.Enqueue(obj);
                }
                poolDictionary.Add(pool.tag, objPool);
            }
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if(!poolDictionary.ContainsKey(tag))
        {
            Debug.LogError("Tag " + tag + " doesn't exist!");
            return null; 
        }

        GameObject obj = poolDictionary[tag].Dequeue();
        obj.SetActive(true);
        obj.transform.position = position;
        obj.transform.rotation = rotation;


        poolDictionary[tag].Enqueue(obj); 

        return obj; 
    }
}
