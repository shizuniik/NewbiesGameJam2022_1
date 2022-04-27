using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField] int extraPoints; 
    public void Disappear()
    {
        int points = extraPoints * GameManager.Level;
    
        if (!GameManager.GamePaused && !GameManager.GameOver)
        {
            AudioManager.Instance.Play("Powerup");
            GameManager.UpdateScore(points);
            gameObject.SetActive(false);

            TargetSpawner.TargetsOnGame -= points; 
        }
    }
}
