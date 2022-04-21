using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{

    [SerializeField] float speed; 

    void Update()
    {
        if(transform.position.y < 30)
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime); 
        }

       
    }
}
