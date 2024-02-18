using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerTwo : MonoBehaviour
{
    public float speed = 3.0f;
    public float jumpSpeed = 15f;
    public float gravity = -20f;

    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.isTrigger = true;
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
}