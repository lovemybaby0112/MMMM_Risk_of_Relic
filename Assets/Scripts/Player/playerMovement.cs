using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f; //���O
    public float jumpHeight = 9f;

    public Transform groundCheck;
    public float groundDistance= 0.94f;
    public LayerMask groundMask;


    Vector3 velocity;
    bool isGrounded;


    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; 
        }

        Movement();
    }

    void Movement()
    {
        float x = Input.GetAxis("Horizontal");  //���k����
        float z = Input.GetAxis("Vertical");    //�e�Ჾ��

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime); 
    }
  
}

