using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScripts : MonoBehaviour
{
    public Rigidbody projectile;
    public Rigidbody shell;
    public float speed = 1000;
    public float shellSpeed = 1000;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            var newBullet = Instantiate(projectile, transform.position, transform.rotation);
            var newShell = Instantiate(shell, transform.position, transform.rotation);
            // Might have to tweak the direction using -transform.forward according to your needs
            newBullet.velocity = transform.forward * speed;
            newShell.velocity = transform.up * shellSpeed;

            Debug.Log("Shoot!");
        }
    }

}
