using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController2 : MonoBehaviour
{
    public float movementSpeed;
    public float cameraRotateSpeed;

    public GameObject playerReference;
    public Transform cameraReference;

    public float minVerticalClamp;
    public float maxVerticalClamp;
    private float vertical;
    //private Vector3 offset;

    void Start()
    {
        //offset = playerReference.transform.position - transform.position;  //Sets distance between the player and camera
    }

    void FixedUpdate()
    {
        float keyboardVertical = Input.GetAxis("Vertical") * movementSpeed;
        float keyboardHorizontal = Input.GetAxis("Horizontal") * movementSpeed; //Forgot to add left and right movement :P
        transform.position += transform.forward * keyboardVertical;
        transform.position += transform.right * keyboardHorizontal; //Forgot to add left and right movement :P
    }
    void LateUpdate()
    {
        float horizontal = Input.GetAxis("Mouse X") * cameraRotateSpeed; //Sets the horizonal vaule as the mouse horizontal movement and * by the speed of rotation
        vertical += Input.GetAxis("Mouse Y") * cameraRotateSpeed; //Sets the horizonal vaule as the mouse horizontal movement and * by the speed of rotation

        //-----------------------------------------------  Setting rotation and movement --------------------------------------
        playerReference.transform.Rotate(0, horizontal, 0); //Rotates based on the horizontal value assigned.
        vertical = Mathf.Clamp(vertical, minVerticalClamp, maxVerticalClamp);

        //cameraReference.Rotate(-vertical, 0, 0, Space.Self); //makes the camera rotate up and down
        //float playerAngle = cameraReference.transform.eulerAngles.x; //Accessing the players angle on the y axis

        cameraReference.transform.eulerAngles = new Vector3(-vertical, cameraReference.transform.eulerAngles.y, cameraReference.transform.eulerAngles.z);

        //Quaternion rotationX = Quaternion.Euler(0, playerAngle, 0); //Creating a new rotation based on player's angle
        //transform.position = playerReference.transform.position - (rotationX * offset);
    }
}
