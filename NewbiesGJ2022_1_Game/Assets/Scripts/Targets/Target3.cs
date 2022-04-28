using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target3 : Target
{
    [SerializeField] float rotationSpeed = 20f; 

    private void FixedUpdate()
    {
        gameObject.transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);

    }
}
