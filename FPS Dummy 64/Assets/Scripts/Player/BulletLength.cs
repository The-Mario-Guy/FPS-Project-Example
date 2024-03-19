using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLength : MonoBehaviour
{
    public float TimeToLive = 1f;
    private void Start()
    {
        Destroy(gameObject, TimeToLive);
    }

    /*public void OnCollisionEnter(Collision other)
    {

        Destroy(gameObject);
    }*/
}
