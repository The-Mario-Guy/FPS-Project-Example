using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadMario : MonoBehaviour
{
    public bool touched;
    public GameObject marioPizza;
    public Animator _pizzaMario;
    // Start is called before the first frame update
    void Start()
    {
        _pizzaMario = marioPizza.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _pizzaMario.SetBool("touched", touched);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Text"))
        {
            touched = true;
        }
    }
}