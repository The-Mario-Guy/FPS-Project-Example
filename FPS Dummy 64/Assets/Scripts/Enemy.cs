using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform Player;
    private FPSController FPSControllerscript;
    public GameObject playerObject;
    public float playerHealth;
    public int MoveSpeed = 4;
    public int MaxDist = 10;
    public int MinDist = 5;
    public float enemyHealth = 2;

    public bool isHurt;
    private FPSController PlayerController;

    public float killed;

    //Gun Stuff
    public Rigidbody projectile;
    public float bulletSpeed = 1000;
    public bool canShoot;
    public GameObject finger;
    private bool isShooting = false;
    private float gunHeat;

    private const float TimeBetweenShots = 2f;



    void Start()
    {
        FPSControllerscript = playerObject.GetComponent<FPSController>();
        playerHealth = FPSControllerscript.health;
        isHurt = FPSControllerscript.isHurt;
        finger.SetActive(false);
    }

    void Update()
    {
        transform.LookAt(Player);

        playerHealth = FPSControllerscript.health;
        if (Vector3.Distance(transform.position, Player.position) >= MinDist)
        {
            finger.SetActive(false);
            transform.position += transform.forward * MoveSpeed * Time.deltaTime;

        if (Vector3.Distance(transform.position, Player.position) <= MaxDist && canShoot == true)
        {
                finger.SetActive(true);
                StartCoroutine(shooting());
         }



        }

        if (FPSControllerscript.health == 0)
        {
            MoveSpeed = -MoveSpeed;
        }
        if (enemyHealth == 0)
        {
            killed = killed + 1;
            Destroy(this.gameObject);
        }
        if (gunHeat > 0)
        {
            gunHeat -= Time.deltaTime;
        }

    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Bullet") && enemyHealth > 0)
        {
            Debug.Log("Hit!");
            enemyHealth = enemyHealth - 1;
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && playerHealth > 0 )
        {
            Debug.Log("Player Hit!");
            playerHealth = playerHealth - 1;
            FPSControllerscript.health = playerHealth;
            isHurt = true;

        }
        if (FPSControllerscript.health == 0)
        {
            MoveSpeed = MoveSpeed - 8;
        }
    }

    private IEnumerator shooting()
    {
        while (canShoot)
        {
            isShooting = true; // Set flag to indicate that shooting is in progress
            var newBullet = Instantiate(projectile, transform.position, transform.rotation);
            newBullet.velocity = transform.forward * bulletSpeed;
            yield return new WaitForSeconds(TimeBetweenShots); // Wait for the specified delay
            isShooting = false; // Reset flag to indicate that shooting has finished
        }
           
    }
}