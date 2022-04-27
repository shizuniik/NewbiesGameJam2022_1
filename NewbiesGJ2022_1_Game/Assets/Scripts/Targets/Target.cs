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
            GameObject explosion = Instantiate(explosionParticle, transform.position, Quaternion.identity);
            Destroy(explosion, 2f);
            AudioManager.Instance.Play("TargetExplosion");

            GameManager.UpdateScore(targetPoints);
            gameObject.SetActive(false);

            TargetSpawner.TargetsOnGame--;
        }
    }

    private void OnBecameInvisible()
    {
        if (gameObject.transform.position.x < Bounds.MinX - 5||
            gameObject.transform.position.x > Bounds.MaxX  + 5||
            gameObject.transform.position.y < Bounds.MinY - 5||
            gameObject.transform.position.y > Bounds.MaxY  + 5||
            gameObject.transform.position.z < Bounds.MinZ  - 5||
            gameObject.transform.position.z > Bounds.MaxZ + 5)
        {
            gameObject.SetActive(false);
            TargetSpawner.TargetsOnGame--;
            //Debug.Log("invisible " + TargetSpawner.TargetsOnGame); 
        }
    }


}
