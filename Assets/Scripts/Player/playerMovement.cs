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
    public float groundDistance= 0.4f;
    public LayerMask groundMask;

    private Animator _animator;


    Vector3 velocity;
    bool isGrounded;

    void Start()
    {
        _animator = GetComponent<Animator>();
        
    }

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

        _animator.SetFloat("Foword", Mathf.Abs(z));
        _animator.SetFloat("Turn", Mathf.Abs(x));

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (Input.GetButtonDown("Fire3"))
        {
            controller.transform.forward = controller.transform.position + new Vector3(0, 0, 2);
        }
    }

    void animatorCtrl()
    {
        
    }
}

