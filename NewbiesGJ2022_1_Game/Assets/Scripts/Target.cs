using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Target : MonoBehaviour
{
    [SerializeField] int targetPoints; 
   
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
        }
    }
}
