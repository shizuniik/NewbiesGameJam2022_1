using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] float force;
    [SerializeField] float speed; 

    private Rigidbody playerRb;
    private bool onFloor; 

    private void Awake()
    {
        playerRb = transform.GetComponent<Rigidbody>();
        onFloor = true; 
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
        Walk(); 
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && onFloor)
        {
            playerRb.AddForce(Vector3.up * force, ForceMode.Impulse);
            onFloor = false; 
        }
    }

    private void Walk()
    {
        transform.Translate(Input.GetAxis("Horizontal") * Vector3.right * Time.deltaTime * speed); 
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Building"))
        {
            onFloor = true; 
        }
    }
}
