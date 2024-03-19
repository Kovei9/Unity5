using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMovement : MonoBehaviour
{
    public float speed = 30.0f; // Speed of the movement
    public float distance = 3.0f; // Distance to move

    private float initialY; // Initial y position

    // Start is called before the first frame update
    void Start()
    {
        // Save the initial y position
        initialY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the new y position
        float newY = initialY + Mathf.PingPong(Time.time * speed, distance);

        // Update the position of the box
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}