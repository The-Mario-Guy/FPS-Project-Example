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
    public bool isShooting;
    public bool isIdle;
    public bool isWalking;
    public TextMeshProUGUI amoCount;
    public TextMeshProUGUI killCount;
    public float kills;
    public Animator _animator;
    public Animator _face;
    public GameObject shotGun;
    public GameObject marioSate;
    public AudioSource shot;
    public AudioSource reload;

    private Enemy enemyScript;
    public GameObject enemyObject;
    public float eHealth;

    private void Start()
    {
        _animator = shotGun.GetComponent<Animator>();
        shot = GetComponent<AudioSource>();
        _face = marioSate.GetComponent<Animator>();
        amoCount.text = amo.ToString();
        killCount.text = kills.ToString();

        enemyScript = enemyObject.GetComponent<Enemy>();
        eHealth = enemyScript.enemyHealth;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && amo > 0)
        {
            isShooting = true;
            shot.Play(1);
            amo = amo - 1;
            amoCount.text = amo.ToString();
            var newBullet = Instantiate(projectile, transform.position, transform.rotation);
       
            var newShell = Instantiate(shell, transform.position, transform.rotation);
            
            // Might have to tweak the direction using -transform.forward according to your needs
            newBullet.velocity = transform.forward * speed;
            newShell.velocity = transform.up * shellSpeed;
            



            //Debug.Log("Shoot!");
        }
        else
        {
            StartCoroutine(shootingFace());
        }
        if (amo <= 0)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                StartCoroutine(shotGunReload());
            }


        }
        
        //Gun Animations
        _animator.SetBool("isReloading", isReloading);
        _animator.SetBool("isWalking", isWalking);
        _animator.SetBool("isShooting", isShooting);

        //Mario Face Animations
        _face.SetBool("isShooting", isShooting);
    }

    private IEnumerator shotGunReload()
    {
        reload.Play(1);
        isReloading = true;
        yield return new WaitForSeconds(0.48f);
        amo = 15;
        amoCount.text = amo.ToString();
        isReloading = false;
    }

    private IEnumerator shootingFace()
    {
        yield return new WaitForSeconds(0.5f);
        isShooting = false;
    }

    public void OnCollisionEnter(Collision other)
    {
        if (projectile.gameObject.CompareTag("Enemy"))
        {
           if (eHealth <= 0)
            {
                Destroy(other.gameObject);
            }
        }
    }

}
