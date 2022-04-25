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
        transform.gameObject.GetComponent<Rigidbody>().useGravity = false; 
    }

}
