using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float characterSpeed = 25.0f;
    public float jumpForce = 20.0f;
    public float gravity = 9.81f;
    public Vector3 velocity = Vector3.zero;
    bool isTouchingGround;

    private CharacterController controller;
    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }

    void Update()
    {
        //Set Grounded
        isTouchingGround = controller.isGrounded;
        //Check for Grounded
        if (isTouchingGround && velocity.y < 0)
        {
            velocity.y = 0f;
        }

        //Movement X
        Vector3 movementX = Input.GetAxisRaw("Vertical") * Vector3.forward * characterSpeed * Time.deltaTime;

        //Movement Y
        Vector3 movementZ = Input.GetAxisRaw("Horizontal") * Vector3.right * characterSpeed * Time.deltaTime;

        //Set Movement
        Vector3 movement = transform.TransformDirection(movementZ + movementX);
        
        //Move
        controller.Move(movement);

        //Setting Jumping
        if (Input.GetButtonDown("Jump") && isTouchingGround)
        {
            velocity.y += Mathf.Sqrt(jumpForce * -2f * gravity);
            print("CharacterController is grounded");
        }
        velocity.y += gravity * Time.deltaTime;

        //Jumping
        controller.Move(velocity * Time.deltaTime);
    }
}
