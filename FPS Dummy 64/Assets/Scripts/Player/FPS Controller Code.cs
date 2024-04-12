using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


[RequireComponent(typeof(CharacterController))]
public class FPSController : MonoBehaviour
{
    public Camera playerCamera;
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float jumpPower = 7f;
    public float gravity = 10f;

    public TextMeshProUGUI healthCounter;

    //Health
    public float health = 100f;
    private float healthMinus = -1f;
    public GameObject shotGun;
    public GameObject gModeHealth;
    //Mario Face
    public GameObject marioSate;
    public Animator _face;
    public Animator _death;
    public bool isHurt;
    public bool isDead;
    public bool gMode;

    public float lookSpeed = 2f;
    public float lookXLimit = 45f;

    //Starting Game
    public AudioSource letsgo;

    //MegaMushroom
    public float scaleMultiplier = 4f;
    public float duration = 8f;
    private Vector3 originalScale;
    private bool isMega;

    //Star
    private bool hasStar;
    public BoxCollider boxCol; 

    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    public bool canMove = true;


    CharacterController characterController;
    void Start()
    {
        letsgo = GetComponent<AudioSource>();
        letsgo.Play(1);
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        healthCounter.text = health.ToString();
        _face = marioSate.GetComponent<Animator>();
        _death = playerCamera.GetComponent<Animator>();
        gModeHealth.SetActive(false);
        originalScale = transform.localScale; //Player's OG scale
    }

    void Update()
    {

        #region Handles Movment
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        #endregion

        #region Handles Jumping
        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        #endregion

        #region Handles Rotation
        characterController.Move(moveDirection * Time.deltaTime);

        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
        _face.SetBool("isHurt", isHurt);
        _face.SetBool("isDead", isDead);
        _death.SetBool("isDead", isDead);
        _face.SetBool("gMode", gMode);
        #endregion

        //health

        if (health <=0)
        {
            StartCoroutine(dead());
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            gMode = true;
            health = 99999999999999999;
            gModeHealth.SetActive(true);
        }

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Goomba"))
        {
            StartCoroutine(Hurt());
        }
        else
        {
            isHurt = false;
        }

        if (other.gameObject.CompareTag("EnemyBullet"))
        {
            StartCoroutine(Hurt());
        }
        else
        {
            isHurt = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Goomba"))
        {
            StartCoroutine(Hurt());
        }
        else
        {
            isHurt = false;
        }

        if (other.gameObject.CompareTag("EnemyBullet"))
        {
            StartCoroutine(Hurt());
        }
        else
        {
            isHurt = false;
        }

        if (other.gameObject.CompareTag("Mega") && !isMega)
        {
            // Destroy(other.gameObject);
            letsgo.Play(1);
            StartCoroutine(MegaMushroom());
        }

        if (other.gameObject.CompareTag("Star") && !hasStar)
        {

            StartCoroutine(starPowerUp());
        }
    }

    private IEnumerator Hurt()
    {
        isHurt = true;
        health = health - 1;
        healthCounter.text = health.ToString();
        yield return new WaitForSeconds(0.4f);
        isHurt = false;
    }
    private IEnumerator dead()
    {
        health = 0;
        healthCounter.text = health.ToString();
        isDead = true;
        shotGun.SetActive(false);
        walkSpeed = 0;
        runSpeed = 0;
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private IEnumerator MegaMushroom()
    {
        isMega = true;
        // Scale the player
        transform.localScale *= scaleMultiplier;
        health = health + 50;
        healthCounter.text = health.ToString();

        // Wait for the specified duration
        yield return new WaitForSeconds(duration);

        // Reset the scale of the player back to its original scale
        transform.localScale = originalScale;

        health = health - 50;
        healthCounter.text = health.ToString();
        isMega = false;
    }
    private IEnumerator starPowerUp()
    {
        hasStar = true;
        health = health + 9999999;
        healthCounter.text = health.ToString();
        walkSpeed = walkSpeed + 6;
        runSpeed = runSpeed + 6;
        yield return new WaitForSeconds(10f);
        health = health - 9999999;
        walkSpeed = walkSpeed - 6;
        runSpeed = runSpeed - 6;
        hasStar = false;
    }
}