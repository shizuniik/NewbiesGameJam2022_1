using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float speedIncreaseRate; 

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void OnEnable()
    {
        GameManager.OnChangeLevel += ChangeSpeed; 
    }

    private void OnDisable()
    {
        GameManager.OnChangeLevel -= ChangeSpeed; 
    }

    private void ChangeSpeed()
    {
        if(speed <= 100f)
            speed += speedIncreaseRate; 
    }

    private void Movement()
    {
        transform.Translate(Vector3.up * Input.GetAxis("Vertical") * Time.deltaTime * speed);
        transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * Time.deltaTime * speed);

        BoundsControl();
    }

    private void BoundsControl()
    {
        if (transform.position.x <= Bounds.MinX)
            transform.position = new Vector3(Bounds.MinX, transform.position.y, transform.position.z);
        if (transform.position.x >= Bounds.MaxX)
            transform.position = new Vector3(Bounds.MaxX, transform.position.y, transform.position.z);
        if (transform.position.y <= Bounds.MinY)
            transform.position = new Vector3(transform.position.x, Bounds.MinY, transform.position.z);
        if (transform.position.y >= Bounds.MaxY)
            transform.position = new Vector3(transform.position.x, Bounds.MaxY, transform.position.z);

    }

}
