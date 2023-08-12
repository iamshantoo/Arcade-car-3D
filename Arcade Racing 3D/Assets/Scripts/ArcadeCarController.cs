using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcadeCarController : MonoBehaviour
{
    public Rigidbody sphereRb;
    
    private float moveInput;
    private float turnInput;

    private bool isCarGrounded;

    public float airDrag;
    public float groundDrag;

    public float forwardSpeed;
    public float reverseSpeed;
    public float turnSpeed;

    public LayerMask groundLayer;

    void Start()
    {
        sphereRb.transform.parent = null;
    }

    void Update()
    {
        moveInput = Input.GetAxisRaw("Vertical");
        turnInput = Input.GetAxisRaw("Horizontal");

        moveInput *= moveInput > 0 ? forwardSpeed : reverseSpeed;

        transform.position = sphereRb.position;

        float newRotation = turnInput * turnSpeed * Time.deltaTime * Input.GetAxisRaw("Vertical");
        transform.Rotate(0, newRotation, 0, Space.World);

        RaycastHit hit;
        isCarGrounded = Physics.Raycast(transform.position, -transform.up, out hit, 1f, groundLayer);

        transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;

        if (isCarGrounded)
        {
            sphereRb.drag = groundDrag;
        }
        else
        {
            sphereRb.drag = airDrag;
        }
    }

    private void FixedUpdate()
    {
        if (isCarGrounded)
        {
            sphereRb.AddForce(transform.forward * moveInput, ForceMode.Acceleration);
        }
        else
        {
            sphereRb.AddForce(transform.up * -30f);
        }
    }
}
