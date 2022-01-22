using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class PlayerMovement : MonoBehaviour
{
    float xMove = 0f;
    float zMove = 0f;
    Vector3 movement;
    public float speed = 15f;
    public float runSpeedBoost = 3f;

    public CharacterController characterController;

    //gravity
    public Vector3 velocity;
    float gravity = -9.8f;
    public Transform groundCheck;

    bool isGrounded;
    public LayerMask groundMask;
    float groundCheckDistance = 0.5f;

    public float jumpHeight = 5f;
    public bool inputAllowed = true;
    public bool gravityPaused = false;

    private AudioSource bounce;

    void Move()
    {
        if(inputAllowed)
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckDistance, groundMask);
            if(isGrounded && velocity.y < 0)
            {
                velocity.y = -1;
            }
        
            if(gravityPaused == false)
            {
                velocity.y += gravity * Time.deltaTime;
                characterController.Move(velocity * Time.deltaTime);
            }

            xMove = Input.GetAxis("Horizontal");

            zMove = Input.GetAxis("Vertical");

            movement = new Vector3(Camera.main.transform.right.x, 0, Camera.main.transform.right.z) * xMove + new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z) * zMove;

            if(Input.GetKey(KeyCode.LeftShift))
            {
                characterController.Move(movement * (speed+runSpeedBoost) * Time.deltaTime);
            }
            else
            {
                characterController.Move(movement * speed * Time.deltaTime);
            }
        }
    }

    void Jump()
    {
        if(Input.GetButton("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        bounce = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(inputAllowed)
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        Move();
    }
}
