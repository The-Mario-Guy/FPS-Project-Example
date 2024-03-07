using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScripts : MonoBehaviour
{
    public GameObject[] bullets;
    public GameObject originPoint;
    public float bulletSpeed = 10;
    public Rigidbody bullet;


    void Fire()
    {
        Rigidbody bulletsClone = (Rigidbody)Instantiate(bullet, transform.position, transform.rotation);
        bulletsClone.velocity = transform.forward * bulletSpeed;
    }
    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
            Fire();
    }
    

}
