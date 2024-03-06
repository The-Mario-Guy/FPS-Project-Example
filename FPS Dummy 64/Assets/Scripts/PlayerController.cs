using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    CharacterController characterController;
    public float MovementSpeed = 5;
    public float Gravity = 9.8f;
    public float gravityModifier;
    public float jumpSpeed;
    private float velocity = 0;
    public bool isOnGround = true;
    private Rigidbody playerRb;

    private Camera cam;

    private void Start()
    {
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
            
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
    }
}
