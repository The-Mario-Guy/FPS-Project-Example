using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    CharacterController characterController;
    public float MovementSpeed = 5;
    public float Gravity = 9.8f;
    public float gravityModifier = 1f;
    public float jumpSpeed;

    public float jumpForce;
    public bool IsOnGround = true;
    public bool isJumping = false;
    private Vector3 _defaultGravity = new Vector3(0f, -9.81f, 0f);

    public float velocity = 0;
    private Rigidbody playerRb;

    private Camera cam;

    private void Start()
    {
        Physics.gravity = _defaultGravity;
        cam = Camera.main;
        characterController = GetComponent<CharacterController>();
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
    }

    void Update()
    {
        // player movement - forward, backward, left, right
        float horizontal = Input.GetAxis("Horizontal") * MovementSpeed;
        float vertical = Input.GetAxis("Vertical") * MovementSpeed;
        characterController.Move((cam.transform.right * horizontal + cam.transform.forward * vertical) * Time.deltaTime);

        // Gravity
        if (characterController.isGrounded)
        {
            velocity = 0;
        }
        else
        {
            velocity -= Gravity * Time.deltaTime;
            characterController.Move(new Vector3(0, velocity, 0));
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            MovementSpeed = 8f;
        }
        else
        {
            MovementSpeed = 5f;
        }
        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            velocity = 0.5f + Gravity;
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
