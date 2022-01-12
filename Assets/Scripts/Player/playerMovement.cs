using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 10f;
    public float gravity = -20f; //重力
    public float jumpHeight = 5f;

    public Transform groundCheck;
    public float groundDistance= 0.4f;
    public LayerMask groundMask;

    private Animator _animator;


    Vector3 velocity;
    bool isGrounded = false;

    void Start()
    {
        _animator = GetComponent<Animator>();

    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        Movement();
    }

    void Movement()
    {

        float x = Input.GetAxis("Horizontal");  //左右移動
        float z = Input.GetAxis("Vertical");    //前後移動


        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        _animator.SetFloat("x", x);
        _animator.SetFloat("z", z);
        


        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        if (isGrounded == true)
        {
            _animator.ResetTrigger("Jump");
        }
        else
        {
            _animator.SetTrigger("Jump");
        }
        

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (Input.GetButtonDown("Fire3"))
        {
            controller.transform.forward = controller.transform.position + new Vector3(0, 0, 2);
        }
    }


}

