using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


// Character controller based movement
// Daniel Hartman
public class CharacterMovement : MonoBehaviour
{
    public CharacterController characterController;
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
    public bool grounded;
    
    [SerializeField]
    public bool canMove = true;

    [SerializeField]
    public bool canParkour = false;

    [SerializeField]
    public bool canSlide = false;

    [SerializeField]
    public bool ledgeGrabAllowed = false;
    
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

    [SerializeField]
    float airTurnSmoothTime = 0.1f;

    float turnSmoothVelocity;

    public Vector3 axis;

    [SerializeField] private AudioSource jumpSound;
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

    Vector3 actualMovementDirection;
    public Vector3 actualVelocity;
    Vector3 previousPos;
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
    public float crouchSlideSpeed;
    public float crouchSlideDrag;
    public float crouchSlideMaxSpeed;
    public float slidingControlModifier;

    public float slopeForce;
    public float slopeForceRayLength;

    public bool wallClimbing;
    public Transform ledgeGrabRayLocation;
    public float ledgeGrabRayLength;
    public bool grabbingLedge;
    public float ledgeJumpPower;
    public float dropLedgeInputWindow;
    public float ledgeGrabCooldown;
    public float timeSinceLastLedgeGrab;
    public bool canLedgeGrab;
    public float ledgeShuffleSpeed;

    public bool canGlide = false;
    void CheckInput()
    {
        xMove = Input.GetAxisRaw("Horizontal");
        zMove = Input.GetAxisRaw("Vertical");
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton0)) && (grounded || wallrunning || grabbingLedge))
        {
            jumping = true;
            jumpSound.Play();
        }
        else if((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.JoystickButton1)) && !grounded && canGlide)
        {
            holdingGlider = !holdingGlider; //Swaps value of holdingGlider
            if(holdingGlider)
            {
                //Play start sound
                SoundManager.instance.PlaySound("Mel2");
            }
            else
            {
                SoundManager.instance.PlaySound("Mel1");

                //Play end sound
            }
            ExitWall();
        }
        if(Input.GetKeyDown(KeyCode.LeftControl) && canSlide)
        {
            StartSlide();
            if(grabbingLedge)
                LedgeDrop();
            if (wallrunning)
                ExitWall();
        }
        if (Input.GetKeyUp(KeyCode.LeftControl) && canSlide)
        {
            if(isCrouchSliding)
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
        if (!canParkour)
            return;
        float collisionDot = Mathf.Abs(Vector3.Dot(collision.GetContact(0).normal, Vector3.up));
        if (collisionDot < 0.1f && !grounded && !holdingGlider && collision.gameObject.tag != "Player" && !grabbingLedge)
        {
            //Draw velocity ray
           // Debug.DrawRay(collision.GetContact(0).point, Vector3.ProjectOnPlane(velocity, collision.GetContact(0).normal), Color.cyan);
            currentWall = collision.gameObject;
            //Dot product with the XZ velocity since we don't care about Y velocity here
            Vector3 xzVelocity = new Vector3(velocity.x, 0, velocity.z);
            //Debug.Log(Vector3.Dot(collision.GetContact(0).normal, xzVelocity.normalized));
            if(Vector3.Dot(collision.GetContact(0).normal, xzVelocity.normalized) >= wallRunningAngleRequirement && wallNormal != collision.GetContact(0).normal)
            {
                if(!wallrunning) //Set the rotations only once when entering the wallrun
                {
                    float leftSideDistance = Vector3.Distance(rayLeft.position, collision.GetContact(0).point);
                    float rightSideDistance = Vector3.Distance(rayRight.position, collision.GetContact(0).point);
                    if (leftSideDistance > rightSideDistance)
                    {
                        playerModel.transform.Rotate(Vector3.forward, wallrunRotation);
                    }
                    else 
                    {
                        playerModel.transform.Rotate(Vector3.forward, -wallrunRotation);
                    }
                    wallrunning = true;
                }
                //Debug.Log("Wallrun");
                wallNormal = collision.GetContact(0).normal;
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if(grabbingLedge) //Important for ledge shuffling
        {
            wallNormal = collision.GetContact(0).normal;
        }
        if (!canParkour)
            return;
        float collisionDot = Mathf.Abs(Vector3.Dot(collision.GetContact(0).normal, Vector3.up));
        if (collisionDot < 0.1f && !grounded && !holdingGlider && collision.gameObject.tag != "Player" && wallrunning)
        {
            //Velocity ray
            Debug.DrawRay(collision.GetContact(0).point, Vector3.ProjectOnPlane(velocity, collision.GetContact(0).normal), Color.cyan);
            velocity = Vector3.ProjectOnPlane(velocity, collision.GetContact(0).normal);
            if (Vector3.Angle(wallNormal, collision.GetContact(0).normal) > maxAngleChange)
            {
                ExitWall();
            }
            Vector3 xzVelocity = new Vector3(velocity.x, 0, velocity.z); 
            if(xzVelocity.magnitude < minWallrunSpeed) //Ends wallrun prematurely if speed gets low enough
            {
                ExitWall();
            }

        }
        else if(grounded && wallrunning) //Exit when hitting ground
        {
            ExitWall();
        }
        
    }

    private void OnCollisionExit(Collision collision)
    {
        if (!canParkour)
            return;
        if (collision.gameObject == currentWall)
        {
            if(!grabbingLedge && ledgeGrabAllowed) //Prevents leaving the wall unintentionally when grabbing ledge
            {
                ExitWall();
                wallClimbing = false;
                grabbingLedge = false;
                timeSinceLastLedgeGrab = 0;
            }
        }
    }
    
    void ExitWall()
    {
        wallClimbing = false;
        wallrunning = false;
        currentWall = null;
        playerModel.transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
    }

    void LedgeDrop()
    {
        if(grabbingLedge)
        {
            velocity = (wallNormal + Vector3.down).normalized * wallJumpPower;
            ExitWall();
            grabbingLedge = false;
            timeSinceLastLedgeGrab = 0;
        }
    }

    void GrabLedge()
    {
        if (wallrunning || !canLedgeGrab || !ledgeGrabAllowed)
            return;

        Ray ray = new Ray(ledgeGrabRayLocation.position, Vector3.down);
        Physics.Raycast(ledgeGrabRayLocation.position, Vector3.down, out RaycastHit hitInfo, ledgeGrabRayLength);
        //Debug.DrawLine(ledgeGrabRayLocation.position, ray.GetPoint(ledgeGrabRayLength), Color.red);
        if (velocity.y <= 0)
        {
            //Debug.Log("Normal" + hitInfo.normal);
            if(Vector3.Dot(hitInfo.normal, Vector3.up) > 0.1f)
            {
                grabbingLedge = true;
                characterController.enabled = false;
                transform.position = new Vector3(transform.position.x, hitInfo.point.y - (ledgeGrabRayLocation.position.y - transform.position.y)
                    + (ledgeGrabRayLength/2), transform.position.z);
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
            //Face towards the ledge
            targetAngle = Mathf.Atan2(-wallNormal.x, -wallNormal.z) * Mathf.Rad2Deg; 
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, targetAngle, transform.eulerAngles.z);
            

            //Ledge shuffling
            float ledgeShuffleAngle = Mathf.Atan2(playerMovement.x, playerMovement.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            moveDir = Quaternion.Euler(0f, ledgeShuffleAngle, 0f) * Vector3.forward;
            velocity = Vector3.ProjectOnPlane(moveDir, wallNormal).normalized * ledgeShuffleSpeed;

            if(Vector3.Dot(moveDir, wallNormal) > dropLedgeInputWindow && playerMovement != Vector3.zero)
            {
                //Drop off the ledge
                LedgeDrop();
            }

            if(playerMovement == Vector3.zero || jumping)
            {
                velocity.x = 0;
                velocity.z = 0;
            }
            Debug.DrawRay(transform.position, velocity);
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
            else if(grounded)
            {
                targetAngle = Mathf.Atan2(playerMovement.x, playerMovement.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                transform.rotation = Quaternion.Euler(transform.eulerAngles.x, angle, transform.eulerAngles.z);
            }
            else
            {
                targetAngle = Mathf.Atan2(playerMovement.x, playerMovement.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, airTurnSmoothTime);
                moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                transform.rotation = Quaternion.Euler(transform.eulerAngles.x, angle, transform.eulerAngles.z);
            }

            if (wallrunning)
            {
                targetAngle = Mathf.Atan2(velocity.x, velocity.z) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(transform.eulerAngles.x, targetAngle, transform.eulerAngles.z);
            }

            
            if(isCrouchSliding)
                velocity += moveDir.normalized * (speed * slidingControlModifier) * Time.deltaTime;
            else if (grounded)
                velocity += moveDir.normalized * speed * Time.deltaTime;
            else if(isGliding)
                velocity += moveDir.normalized * glideAirSpeed * Time.deltaTime;
            else
                velocity += moveDir.normalized * airSpeed * Time.deltaTime;

            if (isCrouchSliding)
                velocity += GetSlideForce();
            else
                velocity += GetSlopeForce();

            if(isCrouchSliding)
            {
                velocity.x *= crouchSlideDrag;
                velocity.z *= crouchSlideDrag;
            }
            else if (grounded)
            {
                velocity.x *= movementDrag;
                velocity.z *= movementDrag;
            }
        }
        else 
        {
            if (isCrouchSliding)
                velocity += GetSlideForce();
            if (isCrouchSliding) //use the same drag regardless of input
            {
                velocity.x *= crouchSlideDrag;
                velocity.z *= crouchSlideDrag;
            }
            else if (grounded)
            {
                velocity.x *= stoppedDrag;
                velocity.z *= stoppedDrag;
                //This is to prevent the inspector from showing incredibly small and insignificant numbers
                if (Mathf.Abs(velocity.x) < 0.01f)
                {
                    velocity.x = 0;
                }
                if (Mathf.Abs(velocity.z) < 0.01f)
                {
                    velocity.z = 0;
                }
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
        if(isCrouchSliding)
        {
            xzMovement = Vector3.ClampMagnitude(xzMovement, crouchSlideMaxSpeed);
        }
        else
        {
            xzMovement = Vector3.ClampMagnitude(xzMovement, maxSpeed);
        }
        velocity.x = xzMovement.x;
        velocity.z = xzMovement.z;

    }

    void JumpGravity()
    {
        if(grabbingLedge)
        {
            velocity.y = 0;
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
        if(!grounded && (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.JoystickButton0))) //Holding jump while airborne
        {
            if (velocity.y == -groundedGravity)
            {
                velocity.y = -holdJumpGravity;
            }
            else
            {
                velocity.y += -holdJumpGravity;
            }
        }
        else if(grounded && velocity.y <= 0) //Gravity when grounded
        {
            velocity.y = -groundedGravity; //Always should be set to be higher than max fall speed
        }
        else //Gravity while airborne and not holding jump
        {
            if(velocity.y == -groundedGravity) //this is meant to run the first frame the player leaves the ground and only then
            {
                velocity.y = -gravity;
            }
            else
            {
                velocity.y += -gravity;
            }
        }
        if(velocity.y < -maxFallSpeed && !grounded)
        {
            //Debug.Log("Velocity hit " + velocity.y);
            velocity.y = -maxFallSpeed;
        }
    }

    void SlipRayCheck()
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


        Physics.Raycast(rayBack.position, Vector3.down, out hitBack, 25f, LayerMask.GetMask("Ground", "Interactable"));
        Physics.Raycast(rayMid.position, Vector3.down, out hitMid, 25f, LayerMask.GetMask("Ground", "Interactable"));
        Physics.Raycast(rayFront.position, Vector3.down, out hitFront, 25f, LayerMask.GetMask("Ground", "Interactable"));
        Physics.Raycast(rayRight.position, Vector3.down, out hitRight, 25f, LayerMask.GetMask("Ground", "Interactable"));
        Physics.Raycast(rayLeft.position, Vector3.down, out hitLeft, 25f, LayerMask.GetMask("Ground", "Interactable"));
        Physics.Raycast(rayFrontRight.position, Vector3.down, out hitFrontRight, 25f, LayerMask.GetMask("Ground", "Interactable"));
        Physics.Raycast(rayFrontLeft.position, Vector3.down, out hitFrontLeft, 25f, LayerMask.GetMask("Ground", "Interactable"));
        Physics.Raycast(rayBackLeft.position, Vector3.down, out hitBackLeft, 25f, LayerMask.GetMask("Ground", "Interactable"));
        Physics.Raycast(rayBackRight.position, Vector3.down, out hitBackRight, 25f, LayerMask.GetMask("Ground", "Interactable"));


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


        if (grounded && ((Vector3.Distance(hitMid.point, rayMid.transform.position) > midRayMaxDistance)))
        {
            Vector3 slideDir = Vector3.zero;
            float slideAngle;
            slideAngle = slopeSlideAngle;
            
            List<float> angles = new List<float>();            
            angles.Add(frontBackAngle);
            angles.Add(leftRightAngle);
            angles.Add(diagonalLeftAngle);
            angles.Add(diagonalRightAngle);
            angles.Sort();

            if (angles[angles.Count-1] == frontBackAngle) //Front/back slope check
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
            else if (leftRightAngle == angles[angles.Count-1]) //Front/back slope check
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
            else if (diagonalRightAngle == angles[angles.Count-1]) //Front/back slope check
            {
                if (hitBackLeft.point.y > hitFrontRight.point.y)
                {
                    slideDir += (hitFrontRight.point - hitBackLeft.point).normalized;
                    //move backwards and down the slope
                }
                else if (hitBackLeft.point.y < hitFrontRight.point.y)
                {
                    slideDir += (hitBackLeft.point - hitFrontRight.point).normalized;
                    //move forwards and down the slope
                }
            }
            else if (diagonalLeftAngle == angles[angles.Count-1]) //Front/back slope check
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

    }

    public void Bounce(float bouncePower)
    {
        velocity.y = bouncePower; 
    }

    public void AddForce(Vector3 forceToAdd)
    {
        velocity += forceToAdd;
    }

    public void Teleport(Transform newPosition)
    {
        characterController.enabled = false;
        transform.position = newPosition.position;
        characterController.enabled = true;
    }

    void Integrate()
    {
        previousPos = transform.position;
        if (canMove) //Prevents movement in cutscenes and such
            Move();
        else
        {
            velocity.x *= stoppedDrag;
            velocity.z *= stoppedDrag;
           
            animator.SetBool("moving", false);
        }
        
        JumpGravity();
        SlipRayCheck();
        GrabLedge();
        if((characterController.collisionFlags & CollisionFlags.Above) != 0 && velocity.y > 0)
        {
            velocity.y = 0;
        }

        characterController.Move(Time.deltaTime * velocity);
        if ((characterController.collisionFlags & CollisionFlags.Sides) != 0)
        {
            touchingWall = true;
            actualMovementDirection = ((transform.position - previousPos) / Time.deltaTime).normalized;

            actualVelocity = (((transform.position - previousPos).normalized * Vector3.Distance(transform.position, previousPos)) / Time.deltaTime);
            animator.SetFloat("runSpeedModifier", new Vector3(actualVelocity.x, 0, actualVelocity.z).magnitude / maxSpeed);
        }
        else 
        {
            Vector3 xzVel = new Vector3(velocity.x, 0, velocity.z);
            animator.SetFloat("runSpeedModifier", xzVel.magnitude / maxSpeed);
            if (touchingWall)
            {
                touchingWall = false;
                velocity = new Vector3(actualVelocity.x, velocity.y, actualVelocity.z);
            }
        }
        //Applied when leaving walls to prevent odd bursts of speed when running against a wall and leaving the wall

    }

    Vector3 GetSlopeForce()
    {
        RaycastHit slopeHit;
        Physics.Raycast(transform.position, Vector3.down, out slopeHit, characterController.height/2 * slopeForceRayLength);

        Ray ray = new Ray(transform.position, Vector3.down);
        //Debug.DrawLine(transform.position, ray.GetPoint(characterController.height / 2 * slopeForceRayLength), Color.green);

        if(slopeHit.normal != Vector3.up && slopeHit.normal != Vector3.zero && velocity.y <= 0)
        {
            Vector3 force = Vector3.down * characterController.height / 2 * slopeForce;
            //Debug.Log(force);
            //grounded = true;
            return force;
        }

        return Vector3.zero;
    }
    
    Vector3 GetSlideForce()
    {
        RaycastHit slopeHit;
        Physics.Raycast(transform.position, Vector3.down, out slopeHit, characterController.height / 2 * slopeForceRayLength);

        if (slopeHit.normal != Vector3.up && slopeHit.normal != Vector3.zero && velocity.y <= 0)
        {
            float slopeFactor = Mathf.Abs(1 - Vector3.Dot(Vector3.up, slopeHit.normal));
            Vector3 slideForce = Vector3.ProjectOnPlane(Vector3.down, slopeHit.normal).normalized * slopeFactor * crouchSlideSpeed;
            //Ray sticks around for 5 seconds
            //Debug.DrawRay(slopeHit.point, slideForce, Color.green, 5f);

            return slideForce;
        }
        //even ground, nothing needed
        return Vector3.zero;
    }

    void GroundCheck()
    {
        if (Physics.CheckSphere(groundChecker.position, jumpDetectRadius, ground) && velocity.y <= 0)
        {
            grounded = true;
            jumping = false;

            if(holdingGlider)
            {
                //play exit sound
            }

            holdingGlider = false;
            wallNormal = Vector3.zero;
        }
        else if (Physics.CheckSphere(groundChecker.position, jumpDetectRadius, 8) && velocity.y <= 0)
        {
            grounded = true;
            jumping = false;

            if (holdingGlider)
            {
                //play exit sound
            }

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

    public void Teleport(Vector3 position)
    {
        characterController.enabled = false;
        transform.position = position;
        characterController.enabled = true;
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
        timeSinceLastLedgeGrab += Time.deltaTime;
        canLedgeGrab = timeSinceLastLedgeGrab > ledgeGrabCooldown;
        animator.SetBool("grounded", grounded || wallrunning);

        //Debug.Log(velocity.magnitude);
        //previousPos = transform.position;
        //Integrate();
        //GroundCheck();
        //GlideCheck();
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
