using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController2 : MonoBehaviour
{
    private Vector3 offset;
    public float movementSpeed;
    public float cameraRotateSpeed = 5;
    public float horizontal;
    public float vertical;

    private Vector2 mouseLook;

    public GameObject playerReference;
    public Transform cameraReference;

    public float minAngleHorizontal;
    public float minAngleVertical;

    public float maxAngleHorizontal;
    public float maxAngleVertical;


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
        horizontal = Input.GetAxis("Mouse X") * cameraRotateSpeed; //Sets the horizonal vaule as the mouse horizontal movement and * by the speed of rotation
        vertical = Input.GetAxis("Mouse Y") * cameraRotateSpeed; //Sets the horizonal vaule as the mouse horizontal movement and * by the speed of rotation

        float clampHorizontal = Mathf.Clamp(horizontal, minAngleHorizontal, maxAngleHorizontal);
        float clampVertical = Mathf.Clamp(vertical, minAngleVertical, maxAngleVertical);

        //-----------------------------------------------  Setting rotation and movement --------------------------------------
        playerReference.transform.Rotate(0, -clampHorizontal, 0); //Rotates basedon the horizontal value assigned.
        cameraReference.Rotate(vertical, 0, 0, Space.Self); //makes the camera rotate up and down

        float playerAngle = playerReference.transform.eulerAngles.y; //Accessing the players angle on the y axis

        Quaternion rotationX = Quaternion.Euler(0, playerAngle, 0); //Creating a new rotation based on player's angle
        transform.position = playerReference.transform.position - (rotationX * offset);
    }
}
