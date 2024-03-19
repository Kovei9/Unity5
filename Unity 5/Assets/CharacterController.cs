using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class PlayerOne : MonoBehaviour
{
    public float speed = 3.0f;
    public float jumpSpeed = 15f;
    public float gravity = -20f;

    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;

    public Score scoreCounter;
    public Text scoreText;

    private bool isCooldown = false;
    private float cooldownTime = 1.0f;

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
        moveDirection.x = Input.GetAxis("Horizontal") * speed;
        moveDirection.z = Input.GetAxis("Vertical") * speed;

        if (characterController.isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
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
        // Check if the collided object is PlayerTwo and we're not in cooldown
        if (other.gameObject.CompareTag("PlayerTwo") && !isCooldown)
        {   
            if (transform.position.y > other.transform.position.y)
            {
                scoreCounter.IncrementScore();
                GameManager.Instance.CheckScore(scoreCounter.GetScore(), "PlayerOne");

                // Update score text
                scoreText.text = "Score: " + scoreCounter.GetScore();

                // Start cooldown
                StartCoroutine(Cooldown());
            }
        }
        // Check if the collided object is BounceWall
        else if (other.gameObject.CompareTag("BounceWall"))
        {
            // Apply a bounce effect
            moveDirection.y = jumpSpeed;

            // Temporarily disable gravity
            gravity = 0;

            // Re-enable gravity after a short delay
            StartCoroutine(EnableGravity());
        }
    }

    IEnumerator Cooldown()
    {
        isCooldown = true;
        yield return new WaitForSeconds(cooldownTime);
        isCooldown = false;
    }

    IEnumerator EnableGravity()
    {
        yield return new WaitForSeconds(0.2f);
        gravity = -20f;
    }
}