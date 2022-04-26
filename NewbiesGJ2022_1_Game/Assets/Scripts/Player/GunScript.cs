using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    [SerializeField] Camera fpsCamera;
    [SerializeField] float range;
    [SerializeField] float fireRate;
    [SerializeField] ParticleSystem fireParticle; 

    private float nextTimeToFire = 0f; 

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f/ fireRate; 
            Shoot();
        }
    }

    private void Shoot()
    {
        //fireParticle.Play();

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
