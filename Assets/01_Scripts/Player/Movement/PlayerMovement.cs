using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpCooldown;
    [SerializeField] private float airMultiplier;
    [SerializeField] private float groundDrag;
    [SerializeField] private Transform orientation;

    [Header("Ground Check")]
    [SerializeField] private float rayLength = 1;

    private float horizontalInput;
    private float verticalInput;
    private bool grounded;
    private bool readyToJump;
    private Vector3 direction;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;
    }

    void Update()
    {
        GetInput();

        Ray ray = new Ray(transform.position, Vector3.down);
        grounded = Physics.Raycast(ray, rayLength);

        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0f;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        if (Input.GetKey(KeyCode.Space) && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void MovePlayer()
    {
        direction = orientation.forward * verticalInput + orientation.right * horizontalInput;
        float multiplier = moveSpeed * 5f;

        if (!grounded)
            multiplier *= airMultiplier;

        rb.AddForce(direction.normalized * multiplier, ForceMode.Force);
        LimitSpeed();
    }

    private void LimitSpeed()
    {
        Vector3 flatVeloctiy = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if (flatVeloctiy.magnitude > moveSpeed)
        {
            Vector3 limited = flatVeloctiy.normalized * moveSpeed;
            rb.velocity = new Vector3(limited.x, rb.velocity.y, limited.z);
        }
    }

    private void Jump()
    {
        //reset velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }
}
