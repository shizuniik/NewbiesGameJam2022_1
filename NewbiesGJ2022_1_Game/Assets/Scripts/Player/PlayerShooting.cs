using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] Transform shootPoint;
    [SerializeField] Transform bulletPrefab; 
    [SerializeField] float bulletSpeed; 

    // Update is called once per frame
    void Update()
    {
        Shoot(); 
    }

    private void Shoot()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Transform newBullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
            newBullet.GetComponent<Rigidbody>().AddForce(Vector3.right * bulletSpeed, ForceMode.Impulse); 
        }
    }
}
