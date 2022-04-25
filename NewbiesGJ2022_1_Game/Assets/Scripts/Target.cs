using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] int targetPoints;

    //public delegate void ChangeTargetsQty();
    //public static event ChangeTargetsQty OnChangeTargetsQty;

    public void Disappear()
    {
        if (!GameManager.GamePaused && !GameManager.GameOver)
        {
            GameManager.UpdateScore(targetPoints);
            gameObject.SetActive(false);

            TargetSpawner.TargetsOnGame--;

            //OnChangeTargetsQty?.Invoke(); 
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.transform.CompareTag("Floor"))
            //transform.position = new Vector3(transform.position.x, collision.transform.position.y, transform.position.z); 
    }

    private void OnBecameInvisible()
    {
        if (gameObject.transform.position.x < Bounds.MinX ||
           gameObject.transform.position.x >  Bounds.MaxX ||
           gameObject.transform.position.y < Bounds.MinY  ||
           gameObject.transform.position.y > Bounds.MaxY  ||
           gameObject.transform.position.z < Bounds.MinZ  ||
           gameObject.transform.position.z > Bounds.MaxZ)
        {
            gameObject.SetActive(false);
            TargetSpawner.TargetsOnGame--;

            Debug.Log(TargetSpawner.TargetsOnGame + " fuera");
        }
    }
}
