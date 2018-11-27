using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody playerRB;

    public float speed;
    float dragForce = 5;

    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        playerRB.drag = dragForce;
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            playerRB.AddForce(transform.forward * speed);
        }

        if (Input.GetKey(KeyCode.S))
        {
            playerRB.AddForce(-transform.forward * speed);
        }

        if (Input.GetKey(KeyCode.A))
        {
            playerRB.AddForce(-transform.right * speed);
        }

        if (Input.GetKey(KeyCode.D))
        {
            playerRB.AddForce(transform.right * speed);
        }
        //Implement get AXIS for the player movement.
    }
}
