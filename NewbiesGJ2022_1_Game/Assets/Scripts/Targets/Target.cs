using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] int targetPoints;
    public GameObject explosionParticle; 

    public void Disappear()
    {
        if (!GameManager.GamePaused && !GameManager.GameOver)
        {
            Instantiate(explosionParticle, transform.position, Quaternion.identity); 
            GameManager.UpdateScore(targetPoints);
            gameObject.SetActive(false);

            TargetSpawner.TargetsOnGame--;
        }
    }

    private void OnBecameInvisible()
    {
        if (gameObject.transform.position.x < Bounds.MinX ||
            gameObject.transform.position.x > Bounds.MaxX ||
            gameObject.transform.position.y < Bounds.MinY ||
            gameObject.transform.position.y > Bounds.MaxY ||
            gameObject.transform.position.z < Bounds.MinZ ||
            gameObject.transform.position.z > Bounds.MaxZ)
        {
            gameObject.SetActive(false);
            TargetSpawner.TargetsOnGame--;
        }
    }


}
