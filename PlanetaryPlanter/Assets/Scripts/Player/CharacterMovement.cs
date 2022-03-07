using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    float maxFallSpeed;

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
    float verticalVelocity;

    [SerializeField]
    float jumpDetectRadius;

    [SerializeField]
    float turnSmoothTime = 0.1f;
    
    float turnSmoothVelocity;

    public Vector3 axis;

    private AudioSource jumpSound;
    public Transform rayBack;
    public Transform rayMid;
    public Transform rayFront;
    public Transform rayRight;
    public Transform rayLeft;
    public Transform rayFrontRight;
    public Transform rayFrontLeft;

    public Transform rayBackLeft;
    public Transform rayBackRight;
    public float slopeSlideAngle;
    public float slideSpeed;
    public float slidingJumpPower;
    public float midRayMaxDistance;
    public bool isSliding;

    public bool isGliding;
    public bool holdingGlider;
    public float glidingGravity;
    public float glideAirSpeed;
    public float glideTurnSpeed;
    public GameObject gliderIndicator;

    public Vector3 actualMovementDirection;
    public Vector3 actualVelocity;
    public Vector3 previousPos;
    public float moveAlongWallAngleDifference;
    public float collisionMovementAngleDifference;
    public bool touchingWall;
    public float touchingWallMaxSpeed;

    public bool wallrunning;
    public float wallrunningGravity;
    public float wallRunningAngleRequirement;
    public float maxAngleChange;
    public Vector3 wallNormal;
    public GameObject currentWall;
    public GameObject playerModel;
    public float wallJumpPower;
    public float minWallrunSpeed;
    public float wallrunRotation;
    public bool isCrouchSliding;
    public float slopeCrouchSlideAngle;

    public bool wallClimbing;
    public Transform ledgeGrabRayLocation;
    public float ledgeGrabRayLength;
    public bool grabbingLedge;
    public float ledgeJumpPower;
    void CheckInput()
    {
        xMove = Input.GetAxisRaw("Horizontal");
        zMove = Input.GetAxisRaw("Vertical");
        if (Input.GetKeyDown(KeyCode.Space) && (grounded || wallrunning || grabbingLedge))
        {
            jumping = true;
            jumpSound.Play();
        }
        else if(Input.GetKeyDown(KeyCode.LeftShift) && !grounded)
        {
            holdingGlider = !holdingGlider; //Swaps value of holdingGlider
            ExitWall();
        }
        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            StartSlide();
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            ExitSlide();
        }
    }

    void StartSlide()
    {
        if(grounded)
        {
            playerModel.transform.Rotate(new Vector3(-1, 0, 0), 60);
            isCrouchSliding = true;
        }

    }

    void ExitSlide()
    {
        playerModel.transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        isCrouchSliding = false;
    }

    // https://dude123code.medium.com/finally-a-good-wall-run-in-unity-4de42bcb7289
    private void OnCollisionEnter(Collision collision)
    {
        float collisionDot = Mathf.Abs(Vector3.Dot(collision.GetContact(0).normal, Vector3.up));
        if (collisionDot < 0.1f && !grounded && !holdingGlider && collision.gameObject.tag != "Player")
        {
            
            Vector3 tempVel = velocity; //Used only for gizmo
            Debug.DrawRay(collision.GetContact(0).point, Vector3.ProjectOnPlane(tempVel, collision.GetContact(0).normal), Color.cyan);
            currentWall = collision.gameObject;
            if(Vector3.Dot(collision.GetContact(0).normal, velocity) >= wallRunningAngleRequirement && wallNormal != collision.GetContact(0).normal)
            {
                if(!wallrunning)
                {
                    float leftSideDistance = Vector3.Distance(rayLeft.position, collision.GetContact(0).point);
                    float rightSideDistance = Vector3.Distance(rayRight.position, collision.GetContact(0).point);
                    float frontSideDistance = Vector3.Distance(rayFront.position, collision.GetContact(0).point);
                    if(frontSideDistance < leftSideDistance && frontSideDistance < rightSideDistance)
                    {
                        playerModel.transform.Rotate(new Vector3(-1, 0, 0), wallrunRotation);

                    }
                    else if (leftSideDistance > rightSideDistance)
                    {
                        playerModel.transform.Rotate(Vector3.forward, wallrunRotation);
                    }
                    else 
                    {
                        playerModel.transform.Rotate(Vector3.forward, -wallrunRotation);
                    }
                    wallrunning = true;
                }
                Debug.Log("Wallrun");
                wallNormal = collision.GetContact(0).normal;
            }
            else
            {
                Debug.Log("Wallclimb");
                wallClimbing = true;
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        float collisionDot = Mathf.Abs(Vector3.Dot(collision.GetContact(0).normal, Vector3.up));
        if (collisionDot < 0.1f && !grounded && !holdingGlider && collision.gameObject.tag != "Player" && wallrunning)
        {
            //Debug.Log(collision.gameObject.name);

            Debug.DrawRay(collision.GetContact(0).point, Vector3.ProjectOnPlane(velocity, collision.GetContact(0).normal), Color.cyan);
            velocity = Vector3.ProjectOnPlane(velocity, collision.GetContact(0).normal);
            if (Vector3.Angle(wallNormal, collision.GetContact(0).normal) > maxAngleChange)
            {
                ExitWall();
            }
            if(velocity.magnitude < minWallrunSpeed)
            {
                ExitWall();
            }

        }
        else if(grounded)
        {
            ExitWall();
        }
        if(grabbingLedge)
        {
            wallNormal = collision.GetContact(0).normal;
        }
        
    }

    private void OnCollisionExit(Collision collision)
    { 
        if(collision.gameObject == currentWall)
        {
            
            ExitWall();
            wallClimbing = false;
            grabbingLedge = false;
        }
    }

    void ExitWall()
    {
        wallClimbing = false;
        wallrunning = false;
        currentWall = null;
        playerModel.transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
    }

    void GrabLedge()
    {
        if (wallrunning)
            return;

        Ray ray = new Ray(ledgeGrabRayLocation.position, Vector3.down);
        Physics.Raycast(ledgeGrabRayLocation.position, Vector3.down, out RaycastHit hitInfo, ledgeGrabRayLength);
        Debug.DrawLine(ledgeGrabRayLocation.position, ray.GetPoint(ledgeGrabRayLength), Color.red);
        if (velocity.y <= 0)
        {
            Debug.Log("Normal" + hitInfo.normal);
            if(Vector3.Dot(hitInfo.normal, Vector3.up) > 0.1f)
            {
                grabbingLedge = true;
                characterController.enabled = false;
                transform.position = new Vector3(transform.position.x, hitInfo.point.y - (ledgeGrabRayLocation.position.y - transform.position.y) + (ledgeGrabRayLength/2), transform.position.z);
                characterController.enabled = true;
            }
            else
            {
                grabbingLedge = false;
            }
        }

    }

    void Move()
    {
        Vector3 playerMovement = new Vector3(xMove, 0, zMove);
        animator.SetBool("moving", (playerMovement != Vector3.zero) || wallrunning);
        
        float targetAngle;
        float angle;
        Vector3 moveDir;
        if (grabbingLedge)
        {
            targetAngle = Mathf.Atan2(-wallNormal.x, -wallNormal.z) * Mathf.Rad2Deg;
            angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, angle, transform.eulerAngles.z);
        }
        else if (playerMovement != Vector3.zero)
        {
            //https://youtu.be/4HpC--2iowE this helped with some math here
            
            if(isGliding)
            {
                targetAngle = Mathf.Atan2(playerMovement.x, playerMovement.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, glideTurnSpeed);
                moveDir = Quaternion.Euler(0f, angle, 0f) * Vector3.forward;
                transform.rotation = Quaternion.Euler(transform.eulerAngles.x, angle, transform.eulerAngles.x);
            }
            else 
            {
                targetAngle = Mathf.Atan2(playerMovement.x, playerMovement.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                transform.rotation = Quaternion.Euler(transform.eulerAngles.x, angle, transform.eulerAngles.z);
            }


            if (wallrunning)
            {
                targetAngle = Mathf.Atan2(velocity.x, velocity.z) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(transform.eulerAngles.x, targetAngle, transform.eulerAngles.z);
            }

            

            if (grounded)
                velocity += moveDir.normalized * speed * Time.deltaTime;
            else if(isGliding)
                velocity += moveDir.normalized * glideAirSpeed * Time.deltaTime;
            else
                velocity += moveDir.normalized * airSpeed * Time.deltaTime;

            if (grounded)
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
            else if(wallrunning)
            {
                velocity.x *= airDrag * 1.1f;
                velocity.z *= airDrag * 1.1f;
            }
            else if(!isGliding)
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
        if(grabbingLedge)
        {
            velocity = Vector3.zero;
            if(jumping)
            {
                velocity += (Vector3.up).normalized * ledgeJumpPower;
                ExitWall();
                jumping = false;
                grabbingLedge = false;
            }
            return;
        }
        if(wallrunning)
        {
            velocity.y -= wallrunningGravity;
            if (jumping)
            {
                if (velocity.y < 0)
                    velocity.y = 0;

                velocity += (wallNormal + Vector3.up).normalized * wallJumpPower;
                ExitWall();
                jumping = false;
            }
            return;
        }
        if(isGliding && !grounded) //Holding a glider and in the air
        {
            if(velocity.y > 0) //Gliding only kicks in when falling
            {
                velocity.y += -gravity;
            }
            else
            {
                velocity.y = -glidingGravity;
            }

            return;
        }
        if (jumping && grounded && isSliding) //Sliding and grounded
        {
            velocity.y = slidingJumpPower;
            grounded = false;
        }
        else if (jumping && grounded) //Grounded but pressed jump
        {
            velocity.y = jumpHeight;
            grounded = false;
            jumping = false;
        }
        if(!grounded && Input.GetKey(KeyCode.Space)) //Holding jump while airborne
        {
            velocity.y += -holdJumpGravity;
        }
        else if(grounded && velocity.y <= 0) //Gravity when grounded
        {
            velocity.y = -groundedGravity;
        }
        else //Gravity while airborne and not holding jump
        {
            velocity.y += -gravity;
        }
        if(velocity.y < -maxFallSpeed)
        {
            velocity.y = -maxFallSpeed;
        }
    }

    void SlopeRayCheck()
    {
        RaycastHit hitBack;
        RaycastHit hitMid;
        RaycastHit hitFront;
        RaycastHit hitRight;
        RaycastHit hitLeft;
        RaycastHit hitFrontRight;
        RaycastHit hitFrontLeft;
        RaycastHit hitBackLeft;
        RaycastHit hitBackRight;


        Physics.Raycast(rayBack.position, Vector3.down, out hitBack);
        Physics.Raycast(rayMid.position, Vector3.down, out hitMid);
        Physics.Raycast(rayFront.position, Vector3.down, out hitFront);
        Physics.Raycast(rayRight.position, Vector3.down, out hitRight);
        Physics.Raycast(rayLeft.position, Vector3.down, out hitLeft);
        Physics.Raycast(rayFrontRight.position, Vector3.down, out hitFrontRight);
        Physics.Raycast(rayFrontLeft.position, Vector3.down, out hitFrontLeft);
        Physics.Raycast(rayBackLeft.position, Vector3.down, out hitBackLeft);
        Physics.Raycast(rayBackRight.position, Vector3.down, out hitBackRight);


        Debug.DrawLine(rayBack.position, hitBack.point, Color.red);
        Debug.DrawLine(rayMid.position, hitMid.point, Color.green);
        Debug.DrawLine(rayFront.position, hitFront.point, Color.blue);
        Debug.DrawLine(rayRight.position, hitRight.point, Color.yellow);
        Debug.DrawLine(rayLeft.position, hitLeft.point, Color.yellow);
        Debug.DrawLine(rayFrontRight.position, hitFrontRight.point, Color.cyan);
        Debug.DrawLine(rayFrontLeft.position, hitFrontLeft.point, Color.grey);
        Debug.DrawLine(rayBackLeft.position, hitBackLeft.point, Color.cyan);
        Debug.DrawLine(rayBackRight.position, hitBackRight.point, Color.grey);



        float frontBackAngle = Vector3.Angle(hitFront.point, hitBack.point);
        float leftRightAngle = Vector3.Angle(hitLeft.point, hitRight.point);
        float diagonalRightAngle = Vector3.Angle(hitFrontRight.point, hitBackLeft.point);
        float diagonalLeftAngle = Vector3.Angle(hitFrontLeft.point, hitBackRight.point);


        if (grounded && ((Vector3.Distance(hitMid.point, rayMid.transform.position) > midRayMaxDistance) || isCrouchSliding))
        {
            Vector3 slideDir = Vector3.zero;
            float slideAngle;
            if (isCrouchSliding)
                slideAngle = slopeCrouchSlideAngle;
            else
                slideAngle = slopeSlideAngle;

            if (frontBackAngle > slideAngle) //Front/back slope check
            {
                if(hitFront.point.y > hitBack.point.y)
                {
                    //move backwards and down the slope
                    slideDir += (hitBack.point - hitFront.point).normalized;
                }
                else if(hitFront.point.y < hitBack.point.y)
                {
                    //move forwards and down the slope
                    slideDir += (hitFront.point - hitBack.point).normalized;
                }
            }
            if (leftRightAngle > slideAngle) //Front/back slope check
            {
                if (hitLeft.point.y > hitRight.point.y)
                {
                    //move backwards and down the slope
                    slideDir += (hitRight.point - hitLeft.point).normalized;
                }
                else if (hitLeft.point.y < hitRight.point.y)
                {
                    //move forwards and down the slope
                    slideDir += (hitLeft.point - hitRight.point).normalized;
                }
            }
            if (diagonalRightAngle > slideAngle) //Front/back slope check
            {
                if (hitBackLeft.point.y > hitFrontRight.point.y)
                {
                    //move backwards and down the slope
                    slideDir += (hitFrontRight.point - hitBackLeft.point).normalized;
                }
                else if (hitBackLeft.point.y < hitFrontRight.point.y)
                {
                    //move forwards and down the slope
                    slideDir += (hitBackLeft.point - hitFrontRight.point).normalized;
                }
            }
            if (diagonalLeftAngle > slideAngle) //Front/back slope check
            {
                if (hitBackRight.point.y > hitFrontLeft.point.y)
                {
                    //move backwards and down the slope
                    slideDir += (hitFrontLeft.point - hitBackRight.point).normalized;
                }
                else if (hitBackRight.point.y < hitFrontLeft.point.y)
                {
                    //move forwards and down the slope
                    slideDir += (hitBackRight.point - hitFrontLeft.point).normalized;
                }
            }
            if (slideDir != Vector3.zero)
                isSliding = true;
            else
                isSliding = false;
            characterController.Move(slideDir.normalized * slideSpeed);
        }
        else
        {
            isSliding = false;
        }

        //Debug.Log("Angle: " + Vector3.Angle(hitFront.point, hitBack.point));
        //GameObject.Find("Angle").GetComponent<TextMeshProUGUI>().text = Vector3.Angle(hitFront.point, hitBack.point).ToString();
        //GameObject.Find("Angle2").GetComponent<TextMeshProUGUI>().text = Vector3.Distance(hitMid.point, rayMid.transform.position).ToString();


    }

    public void Bounce(float bouncePower)
    {
        velocity.y = bouncePower; 
    }

    void Integrate()
    {
        if (canMove) //Prevents movement in cutscenes and such
            Move();
        else
        {
            velocity.x *= stoppedDrag;
            velocity.z *= stoppedDrag;
            animator.SetBool("moving", velocity != Vector3.zero);
        }

        JumpGravity();
        SlopeRayCheck();
        GrabLedge();
        if((characterController.collisionFlags & CollisionFlags.Above) != 0 && velocity.y > 0)
        {
            velocity.y = 0;
        }

        if ((characterController.collisionFlags & CollisionFlags.Sides) != 0)
        {
            Vector3 xzMove = new Vector3(velocity.x, 0, velocity.z);
            float actualVelocityMagnitude = new Vector2(actualVelocity.x, actualVelocity.z).magnitude;
            xzMove = Vector3.ClampMagnitude(xzMove, maxSpeed - (maxSpeed * (collisionMovementAngleDifference/90)));
            velocity.x = xzMove.x;
            velocity.z = xzMove.z;
        }
        characterController.Move(Time.deltaTime * velocity);
        if ((characterController.collisionFlags & CollisionFlags.Sides) != 0)
        {
            touchingWall = true;
            actualMovementDirection = ((transform.position - previousPos) /Time.deltaTime).normalized;
            actualVelocity = (((transform.position - previousPos).normalized * Vector3.Distance(transform.position, previousPos)) / Time.deltaTime);
            
            //Debug.DrawRay(transform.position, actualMovementDirection);
        }
        //Applied when leaving walls to prevent odd bursts of speed when running against a wall and leaving the wall

    }

    void GroundCheck()
    {
        if (Physics.CheckSphere(groundChecker.position, jumpDetectRadius, ground) && velocity.y <= 0)
        {
            grounded = true;
            jumping = false;
            holdingGlider = false;
            wallNormal = Vector3.zero;
        }
        else if (Physics.CheckSphere(groundChecker.position, jumpDetectRadius, 8) && velocity.y <= 0)
        {
            grounded = true;
            jumping = false;
            holdingGlider = false;
        }
        else
        {
            grounded = false;
        }
    }

    void GlideCheck()
    {
        gliderIndicator.SetActive(holdingGlider);
        
        if(holdingGlider && !grounded && velocity.y < 0)
        {
            isGliding = true;
        }
        else
        {
            isGliding = false;
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
        animator.SetBool("grounded", grounded || wallrunning);
    }

    private void FixedUpdate()
    {
        previousPos = transform.position;
        Integrate();
        GroundCheck();
        GlideCheck();

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(groundChecker.position, jumpDetectRadius);
        //Gizmos.DrawSphere(groundChecker.position, jumpDetectRadius);
    }
}
