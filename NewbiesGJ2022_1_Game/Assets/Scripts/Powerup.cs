using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField] int extraPoints; 
    public void Disappear()
    {
        extraPoints = extraPoints * GameManager.Level; 

        if (!GameManager.GamePaused && !GameManager.GameOver)
        {
            GameManager.UpdateScore(extraPoints);
            gameObject.SetActive(false);

            TargetSpawner.TargetsOnGame -= extraPoints; 
        }
    }
}
