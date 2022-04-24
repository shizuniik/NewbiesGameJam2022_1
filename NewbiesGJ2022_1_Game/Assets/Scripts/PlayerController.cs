using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float xRange;
    [SerializeField] float yMin;
    [SerializeField] float yMax; 

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        transform.Translate(Vector3.up * Input.GetAxis("Vertical") * Time.deltaTime * speed);
        transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * Time.deltaTime * speed);

        Bounds();
    }

    private void Bounds()
    {
        if (transform.position.x <= -xRange)
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        if (transform.position.x >= xRange)
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        if (transform.position.y <= yMin)
            transform.position = new Vector3(transform.position.x, yMin, transform.position.z);
        if (transform.position.y >= yMax)
            transform.position = new Vector3(transform.position.x, yMax, transform.position.z);

        Debug.Log(Screen.height + " - " + Screen.width); 
    }
}
