using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public int extraPoints;
    public GameObject explosionParticle;

    public void Disappear()
    {
        int points = extraPoints; 

        if (!GameManager.GamePaused && !GameManager.GameOver)
        {
            AudioManager.Instance.Play("Powerup");
            GameManager.UpdateScore(points);
            gameObject.SetActive(false);

            DisappearAll(); 
        }
    }

    private void DisappearAll()
    {
        GameObject explosion = Instantiate(explosionParticle, transform.position, Quaternion.identity);
        Destroy(explosion, 2f);
        AudioManager.Instance.Play("MassiveExplosion"); 

        foreach (Target t in FindObjectsOfType<Target>())
        {
            t.gameObject.SetActive(false);
            TargetSpawner.TargetsOnGame = 0;
        }
    }
}
