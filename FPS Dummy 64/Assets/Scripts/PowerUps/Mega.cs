using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mega : MonoBehaviour
{
    public Transform Player;
    public GameObject playerObject;
    public float MoveSpeed = -0.5f;
    public int MaxDist = 10;
    public int MinDist = 5;







    void Start()
    {

    }

    void Update()
    {
        transform.LookAt(Player);


        if (Vector3.Distance(transform.position, Player.position) >= MinDist)
        {
            transform.position += transform.forward * MoveSpeed * Time.deltaTime;
        }
        if (Vector3.Distance(transform.position, Player.position) <= MaxDist)
        {


        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}