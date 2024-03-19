using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform Player;
    private FPSController FPSControllerscript;
    public GameObject playerObject;
    public Transform enemyHome;
    public float playerHealth;
    public int MoveSpeed = 4;
    public int noSpeed = 0;
    public int MaxDist = 10;
    public int MinDist = 5;
    public float enemyHealth = 2;
    public FPSController PlayerController;

    public float killed;




    void Start()
    {
        FPSControllerscript = playerObject.GetComponent<FPSController>();
        playerHealth = FPSControllerscript.health;
    }

    void Update()
    {
        transform.LookAt(Player);

        if (Vector3.Distance(transform.position, Player.position) >= MinDist)
        {

            transform.position += transform.forward * MoveSpeed * Time.deltaTime;



            if (Vector3.Distance(transform.position, Player.position) <= MaxDist)
            {
                transform.position += transform.forward * -MoveSpeed * Time.deltaTime;
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


        }
        if (FPSControllerscript.health == 0)
        {
            MoveSpeed = MoveSpeed - 8;
        }
    }
}