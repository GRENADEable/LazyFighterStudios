using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Movement")]
    public float speed;
    public float runningSpeed;
    public float pushPower;
    public float gravity;
    [Header("Camera Clamp")]
    public float minVerticalClamp;
    public float maxVerticalClamp;
    [Header("References")]
    public GameObject playerReference;
    public Transform cameraReference;
    public float cameraRotateSpeed;

    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;
    private float vertical;
    [SerializeField]
    private Collider col;
    private Animator anim;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (col != null && Input.GetKeyDown(KeyCode.E) && col.gameObject.tag == "Pickup")
        {
            col.gameObject.SetActive(false);
            col = null;
        }

        if (col != null && Input.GetKeyDown(KeyCode.E) && col.gameObject.tag == "Interact")
        {
            this.transform.position = new Vector3(1.0f, 6.0f);
        }

        if (col != null && Input.GetKeyDown(KeyCode.E) && col.gameObject.tag == "Interact2")
        {
            this.transform.position = new Vector3(1.0f, 2.6f);
        }

        if (Input.GetKey(KeyCode.W))
            anim.SetBool("isWalking", true);
        else
            anim.SetBool("isWalking", false);

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
        {
            anim.SetBool("isWalking", false);
            anim.SetBool("isRunning", true);
        }
        else
            anim.SetBool("isRunning", false);

        if (Input.GetKey(KeyCode.S))
            anim.SetBool("isWalkingBack", true);
        else
            anim.SetBool("isWalkingBack", false);

        if (Input.GetKey(KeyCode.A))
        {
            anim.SetBool("isSneakingLeft", true);
            anim.SetBool("isWalking", false);
            anim.SetBool("isWalkingBack", false);
            anim.SetBool("isRunning", false);
        }
        else
            anim.SetBool("isSneakingLeft", false);

        if (Input.GetKey(KeyCode.D))
        {
            anim.SetBool("isSneakingRight", true);
            anim.SetBool("isWalking", false);
            anim.SetBool("isWalkingBack", false);
            anim.SetBool("isRunning", false);
        }
        else
            anim.SetBool("isSneakingRight", false);
    }

    void FixedUpdate()
    {
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        moveDirection = transform.TransformDirection(moveDirection);

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            Debug.LogWarning("Running");
            moveDirection = moveDirection * runningSpeed;
        }
        else
            moveDirection = moveDirection * speed;

        moveDirection.y = moveDirection.y - gravity;
        controller.Move(moveDirection);
    }

    void LateUpdate()
    {
        float horizontal = Input.GetAxis("Mouse X") * cameraRotateSpeed; //Sets the horizonal vaule as the mouse horizontal movement and * by the speed of rotation
        vertical += Input.GetAxis("Mouse Y") * cameraRotateSpeed; //Sets the horizonal vaule as the mouse horizontal movement and * by the speed of rotation

        //-----------------------------------------------  Setting rotation and movement --------------------------------------
        playerReference.transform.Rotate(0, horizontal, 0); //Rotates based on the horizontal value assigned.
        vertical = Mathf.Clamp(vertical, minVerticalClamp, maxVerticalClamp);

        cameraReference.transform.eulerAngles = new Vector3(-vertical, cameraReference.transform.eulerAngles.y, cameraReference.transform.eulerAngles.z);
    }

    //If player enters a collider, reference the other collider
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pickup")
        {
            col = other;
        }

        if (other.tag == "Interact")
        {
            col = other;
        }

        if (other.tag == "Interact2")
        {
            col = other;
        }

        // if (other.tag == "Light")
        // {
        //     this.gameObject.SetActive(false);
        // }
    }

    //If player leaves a collider, remove the reference
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Pickup")
        {
            col = null;
        }

        if (other.tag == "Interact")
        {
            col = null;
        }

        if (other.tag == "Interact2")
        {
            col = null;
        }
    }

    //If player detects colliders with rigidbody in it. The player will push the object.
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody rgbody = hit.collider.attachedRigidbody;

        if (rgbody == null || rgbody.isKinematic)
        {
            return;
        }

        if (hit.moveDirection.y < -0.3f)
        {
            return;
        }

        Vector3 push = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        rgbody.velocity = push * pushPower;
    }
}
