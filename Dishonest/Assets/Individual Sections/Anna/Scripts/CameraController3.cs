using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController3 : MonoBehaviour {

    public Transform playerLocation;
    public float distanceAway;
    public Transform cameraReference;

    private const float Y_ANGLE_MIN = 0.0f;
    private const float Y_ANGLE_MAX = 50.0F;

    private float currentX = 0;
    private float currentY = 20;



	void Start ()
    {
        cameraReference = GetComponent<Transform>();
    }
	
	void Update ()
    {
        currentX += Input.GetAxis("Mouse X");
        currentY += Input.GetAxis("Mouse Y");

        currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);
    }

    private void LateUpdate()
    {
        Vector3 direction = new Vector3(0, 0, -distanceAway);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        cameraReference.position = playerLocation.position + rotation * direction;
        cameraReference.LookAt(playerLocation.position);
    }
}
