using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController2 : MonoBehaviour
{
    Vector3 offset;
    public float movementSpeed;
    public float cameraRotateSpeed = 5;
    public GameObject playerReference;
    public Transform cameraReference;

    void Start()
    {
        offset = playerReference.transform.position - transform.position;  //Sets distance between the player and camera
    }

    void FixedUpdate()
    {
        float keyboardVertical = Input.GetAxis("Vertical") * movementSpeed;
        transform.position += transform.forward * keyboardVertical;
    }

    void LateUpdate()
    {
        float horizontal = Input.GetAxis("Mouse X") * cameraRotateSpeed; //Sets the horizonal vaule as the mouse horizontal movement and * by the speed of rotation
        float vertical = Input.GetAxis("Mouse Y") * cameraRotateSpeed; //Sets the horizonal vaule as the mouse horizontal movement and * by the speed of rotation
        //vertical = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);

        playerReference.transform.Rotate(0, horizontal,0); //Rotates basedon the horizontal value assigned.
        cameraReference.Rotate(vertical, 0, 0, Space.Self); //makes the camera rotate up and down

        float playerAngle = playerReference.transform.eulerAngles.y; //Accessing the players angle on the y axis
 
        Quaternion rotationX = Quaternion.Euler(0, playerAngle, 0); //Creating a new rotation based on player's angle
        transform.position = playerReference.transform.position - (rotationX * offset);

        //transform.LookAt(playerReference.transform);
    }
}
