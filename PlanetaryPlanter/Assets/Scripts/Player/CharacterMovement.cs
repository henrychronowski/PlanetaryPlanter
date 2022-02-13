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
    public Transform rayBack;
    public Transform rayMid;
    public Transform rayFront;
    public Transform rayRight;
    public Transform rayLeft;




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
        else if(grounded && velocity.y <= 0)
        {
            velocity.y = -groundedGravity;
        }
        else
        {
            velocity.y += -gravity;
        }
    }

    void SlopeRayCheck()
    {
        RaycastHit hitBack;
        RaycastHit hitMid;
        RaycastHit hitFront;
        RaycastHit hitRight;
        RaycastHit hitLeft;


        Physics.Raycast(rayBack.position, Vector3.down, out hitBack);
        Physics.Raycast(rayMid.position, Vector3.down, out hitMid);
        Physics.Raycast(rayFront.position, Vector3.down, out hitFront);
        Physics.Raycast(rayRight.position, Vector3.down, out hitRight);
        Physics.Raycast(rayLeft.position, Vector3.down, out hitLeft);


        Debug.DrawLine(rayBack.position, hitBack.point, Color.red);
        Debug.DrawLine(rayMid.position, hitMid.point, Color.green);
        Debug.DrawLine(rayFront.position, hitFront.point, Color.blue);
        Debug.DrawLine(rayRight.position, hitRight.point, Color.yellow);
        Debug.DrawLine(rayLeft.position, hitLeft.point, Color.yellow);

        Vector3 rayBackLocalHit = new Vector3(rayBack.localPosition.x, rayBack.localPosition.y - (Vector3.Distance(hitBack.point, rayBack.position)), rayBack.localPosition.z);
        Vector3 rayMidLocalHit = new Vector3(rayMid.localPosition.x, rayMid.localPosition.y - (Vector3.Distance(hitMid.point, rayMid.position)), rayMid.localPosition.z);
        Vector3 rayFrontLocalHit = new Vector3(rayFront.localPosition.x, rayFront.localPosition.y - (Vector3.Distance(hitFront.point, rayFront.position)), rayFront.localPosition.z);
        Vector3 rayRightLocalHit = new Vector3(rayRight.localPosition.x, rayRight.localPosition.y - (Vector3.Distance(hitRight.point, rayRight.position)), rayRight.localPosition.z);
        Vector3 rayLeftLocalHit = new Vector3(rayLeft.localPosition.x, rayLeft.localPosition.y - (Vector3.Distance(hitLeft.point, rayLeft.position)), rayLeft.localPosition.z);




        Debug.Log("Angle: " + Vector3.Angle(hitFront.point, hitBack.point));
        Debug.Log("Signed: " + Vector3.SignedAngle(rayFrontLocalHit, rayBackLocalHit, new Vector3(1, 0, 0)));


    }

    public void Bounce(float bouncePower)
    {
        velocity.y = bouncePower; 
    }

    void Integrate()
    {
        if(canMove)
            Move();

        JumpGravity();
        SlopeRayCheck();
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
