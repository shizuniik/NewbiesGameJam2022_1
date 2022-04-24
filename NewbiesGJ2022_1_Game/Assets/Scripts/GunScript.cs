using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    [SerializeField] Camera fpsCamera;
    [SerializeField] float range;

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetButtonDown("Fire1"))
        if(Input.GetButton("Fire1"))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        RaycastHit hit;
            
        if(Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
        {
            Target target = hit.transform.gameObject.GetComponent<Target>(); 

            if(target != null)
            {
                target.Disappear();
            }
        }
    }
}
