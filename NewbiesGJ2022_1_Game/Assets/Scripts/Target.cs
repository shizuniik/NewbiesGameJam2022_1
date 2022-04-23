using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Target : MonoBehaviour
{
    [SerializeField] int targetPoints;

    //public delegate void ChangeTargetsQty();
    //public static event ChangeTargetsQty OnChangeTargetsQty;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Disappear()
    {
        GameManager.UpdateScore(targetPoints);
        gameObject.SetActive(false);  
    }

    private void OnMouseDown()
    {
        if (!GameManager.GamePaused && !GameManager.GameOver)
        {
            Disappear();
            TargetSpawner.TargetsOnGame--;

            //OnChangeTargetsQty?.Invoke(); 
        }
    }
}
