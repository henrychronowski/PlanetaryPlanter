using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Character controller based movement
// Daniel Hartman
public class CharacterMovement : MonoBehaviour
{
    private CharacterController characterController;
    public Transform cam;
    public Animator animator;

    [SerializeField]
    float speed;

    [SerializeField]
    float airSpeed;

    [SerializeField]
    float jumpHeight = 1.0f;

    [SerializeField]
    float maxSpeed;

    [SerializeField]
    float gravity;

    [SerializeField]
    float groundedGravity;

    [SerializeField]
    float movementDrag;

    [SerializeField]
    float stoppedDrag;

    [SerializeField]
    float airDrag;

    [SerializeField]
    float holdJumpGravity;

    float xMove;

    float zMove;

    [SerializeField]
    Transform groundChecker;

    [SerializeField]
    LayerMask ground;

    [SerializeField]
    bool grounded;
    
    [SerializeField]
    public bool canMove = true; 
    
    [SerializeField]
    bool jumping;
    
    [SerializeField]
    Vector3 velocity;


    [SerializeField]
    float jumpDetectRadius;

    [SerializeField]
    float turnSmoothTime = 0.1f;
    
    float turnSmoothVelocity;

    private AudioSource jumpSound;


    void CheckInput()
    {
        xMove = Input.GetAxisRaw("Horizontal");
        zMove = Input.GetAxisRaw("Vertical");
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            jumping = true;
            jumpSound.Play();
        }
    }

    void Move()
    {
        Vector3 playerMovement = new Vector3(xMove, 0, zMove);
        animator.SetBool("moving", playerMovement != Vector3.zero);
        if (playerMovement != Vector3.zero)
        {
            //https://youtu.be/4HpC--2iowE this helped with some math here
            float targetAngle = Mathf.Atan2(playerMovement.x, playerMovement.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            if(grounded)
                velocity += moveDir.normalized * speed * Time.deltaTime;
            else
                velocity += moveDir.normalized * airSpeed * Time.deltaTime;

            if(grounded)
            {
                velocity.x *= movementDrag;
                velocity.z *= movementDrag;
            }
        }
        else 
        {
            if (grounded)
            {
                velocity.x *= stoppedDrag;
                velocity.z *= stoppedDrag;
            }
            else
            {
                velocity.x *= airDrag;
                velocity.z *= airDrag;
            }
        }
        
        
        //Clamp only the x and z values
        //Use a separate vector to avoid jumping triggering the clamp, slowing the x and z movement drastically when jumping 
        Vector3 xzMovement = new Vector3(velocity.x, 0, velocity.z);
        xzMovement = Vector3.ClampMagnitude(xzMovement, maxSpeed);
        velocity.x = xzMovement.x;
        velocity.z = xzMovement.z;

    }

    void JumpGravity()
    {
        if(jumping && grounded)
        {
            velocity.y = jumpHeight;
            grounded = false;
        }
        if(!grounded && Input.GetKey(KeyCode.Space))
        {
            velocity.y += -holdJumpGravity;
        }
        else if(grounded && velocity.y < 0)
        {
            velocity.y = -groundedGravity;
        }
        else
        {
            velocity.y += -gravity;
        }
    }


    void Integrate()
    {
        if(canMove)
            Move();

        JumpGravity();
        characterController.Move(Time.deltaTime * velocity);
    }

    void GroundCheck()
    {
        if (Physics.CheckSphere(groundChecker.position, jumpDetectRadius, ground) && velocity.y <= 0)
        {
            grounded = true;
            jumping = false;
        }
        else if (Physics.CheckSphere(groundChecker.position, jumpDetectRadius, 8) && velocity.y <= 0)
        {
            grounded = true;
            jumping = false;
        }
        else
        {
            grounded = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        jumpSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        animator.SetBool("grounded", grounded);
    }

    private void FixedUpdate()
    {
        Integrate();
        GroundCheck();

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        //Gizmos.DrawSphere(groundChecker.position, jumpDetectRadius);
    }
}
