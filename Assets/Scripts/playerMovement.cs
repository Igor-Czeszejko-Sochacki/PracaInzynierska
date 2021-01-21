using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public CharacterController controller;

    public Transform groundCheck;
    public LayerMask groundMask;
    Vector3 velocity;
    bool isGrounded;
    bool isCrouching = false;
    public float groundDistance = 0.4f;
    public float speed = 12f;
    public float jumpHeight = 3f;
    public float gravity = -9.81f;
    public float crouchHeight = 0.5f;
    public float jumpRecharge = 0f;
    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        jumpRecharge -= Time.deltaTime;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        float xAxis = Input.GetAxis("Horizontal");
        float zAxis = Input.GetAxis("Vertical");

        Vector3 move = transform.right * xAxis + transform.forward * zAxis;
        if (isCrouching == true)
        {
            speed = 5f;
            controller.Move(move * speed * Time.deltaTime);
        }
        else
        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && jumpRecharge <= 0f)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            jumpRecharge = 1f;
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            controller.height = crouchHeight;
            isCrouching = true;
            speed = 5f;
        }
        else
        {
            controller.height = 3.5f;
            isCrouching = false;
            speed = 12f;
        }

        if (Input.GetKey(KeyCode.LeftShift) && zAxis == 1 && isCrouching == false && isGrounded == true)
            speed = 20f;
        else
            speed = 12f;
    }
}
