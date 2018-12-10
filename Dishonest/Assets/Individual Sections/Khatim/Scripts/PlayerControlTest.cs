using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlTest : MonoBehaviour
{
    public float speed;
    public float rotateSpeed;
    private CharacterController charController;
    [SerializeField]
    private Collider col;

    void Start()
    {
        charController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (col != null && Input.GetKeyDown(KeyCode.E))
        {
            col.gameObject.SetActive(false);
            col = null;
        }
    }
    void FixedUpdate()
    {
        transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        float curSpeed = speed * Input.GetAxis("Vertical");
        charController.SimpleMove(forward * curSpeed);
    }

    /*void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Pickup" && Input.GetKey(KeyCode.E))
        {
            other.gameObject.SetActive(false);
        }
    }*/

    //If player enters a collider, reference the other collider
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pickup")
        {
            col = other;
        }
    }

    //If player leaves a collider, remove the reference
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Pickup")
        {
            col = null;
        }
    }
}
