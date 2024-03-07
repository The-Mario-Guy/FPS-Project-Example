using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayer : MonoBehaviour
{
    CharacterController characterController;
    public float MovementSpeed = 5;
    public float gravityModifier = 1f;
    private Vector3 _defaultGravity = new Vector3(0f, -9.81f, 0f);
    public float jumpForce;
    public bool IsOnGround = true;
    public bool isJumping = false;
    private Rigidbody _rigidbody;
    public Camera cam;


    private void Start()
    {
        Physics.gravity = _defaultGravity;
        Physics.gravity *= gravityModifier;
        characterController = GetComponent<CharacterController>();
        _rigidbody = GetComponent<Rigidbody>();
        cam = Camera.main;
    }
    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal") * MovementSpeed;
        float vertical = Input.GetAxis("Vertical") * MovementSpeed;
        characterController.Move((cam.transform.right * horizontal + cam.transform.forward * vertical) * Time.deltaTime);


        if (Input.GetKeyDown(KeyCode.Space) && IsOnGround == true)
        {

            _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            IsOnGround = false;
            isJumping = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            IsOnGround = true;
            isJumping = false;

        }
    }
}