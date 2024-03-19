using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class PlayerTwo : MonoBehaviour
{
    public float speed = 3.0f;
    public float jumpSpeed = 15f;
    public float gravity = -20f;

    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;

    public Score scoreCounter;
    public Text scoreText;

    private bool isCooldown = false;
    private float cooldownTime = 1.0f; // Add 'f' suffix

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.isTrigger = true;
         
        // Initialize Score instance
        scoreCounter = new Score();
    }
    

    void Update()
    {
        float hInput = 0;
        float vInput = 0;

        if (Input.GetKey(KeyCode.J)) // move left
        {
            hInput = -1;
        }
        else if (Input.GetKey(KeyCode.L)) // move right
        {
            hInput = 1;
        }

        if (Input.GetKey(KeyCode.I)) // move forward
        {
            vInput = 1;
        }
        else if (Input.GetKey(KeyCode.K)) // move backward
        {
            vInput = -1;
        }

        moveDirection.x = hInput * speed;
        moveDirection.z = vInput * speed;

        if (characterController.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Semicolon))
            {
                moveDirection.y = jumpSpeed;
            }
        }
        else
        {
            moveDirection.y += gravity * Time.deltaTime;
        }

        characterController.Move(moveDirection * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the collided object is PlayerOne and we're not in cooldown
        if (other.gameObject.CompareTag("PlayerOne") && !isCooldown)
        {
            // Check if PlayerTwo is above PlayerOne
            if (transform.position.y > other.transform.position.y)
            {
                scoreCounter.IncrementScore();
                GameManager.Instance.CheckScore(scoreCounter.GetScore(), "PlayerTwo");

                // Update score text
                scoreText.text = "Score: " + scoreCounter.GetScore();

                // Start cooldown
                StartCoroutine(Cooldown());
            }
        }
        // Check if the collided object is BounceWall
        else if (other.gameObject.CompareTag("BounceWall"))
        {
            moveDirection.y = jumpSpeed;
        }
    }

    IEnumerator Cooldown()
    {
        isCooldown = true;
        yield return new WaitForSeconds(cooldownTime);
        isCooldown = false;
    }
}