using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GunScripts : MonoBehaviour
{
    public Rigidbody projectile;
    public Rigidbody shell;
    public float speed = 1000;
    public float shellSpeed = 1000;
    public float amo = 15;
    public bool isReloading;
    public TextMeshProUGUI amoCount;
    public Animator _animator;
    public GameObject shotGun;

    private void Start()
    {
        _animator = shotGun.GetComponent<Animator>();
        amoCount.text = amo.ToString();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && amo > 0)
        {
            amo = amo - 1;
            amoCount.text = amo.ToString();
            var newBullet = Instantiate(projectile, transform.position, transform.rotation);
            Destroy(projectile, 5f);
            var newShell = Instantiate(shell, transform.position, transform.rotation);
            Destroy(shell, 10f);
            // Might have to tweak the direction using -transform.forward according to your needs
            newBullet.velocity = transform.forward * speed;
            newShell.velocity = transform.up * shellSpeed;
            
           

            Debug.Log("Shoot!");
        }
        if (amo <= 0)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                StartCoroutine(shotGunReload());
            }


        }
        _animator.SetBool("isReloading", isReloading);
    }

    private IEnumerator shotGunReload()
    {
        isReloading = true;
        yield return new WaitForSeconds(0.48f);
        amo = 15;
        amoCount.text = amo.ToString();
        isReloading = false;
    }

}
