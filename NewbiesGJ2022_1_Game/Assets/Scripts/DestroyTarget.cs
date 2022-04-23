using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTarget : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Shooting(); 
    }

    private void Shooting()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Fire!");
            FindObjectOfType<Target>().Disappear(); // BORRAR 
        }
    }
}
