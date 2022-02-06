using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private CharacterController characterController;
    public Transform camera;

    [SerializeField]
    private float speed = 1f;
    public Animator animator;

    [SerializeField]
    Transform groundChecker;

    [SerializeField]
    float jumpPower;

    [SerializeField]
    float maxSpeed;

    [SerializeField]
    float gravity;

    float xMove;

    float zMove;

    public bool grounded;

    [SerializeField]
    LayerMask ground;

    [SerializeField]
    Vector3 velocity;

    [SerializeField]
    Vector3 jumpDetect;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;


    void CheckInput()
    {
        xMove = Input.GetAxisRaw("Horizontal");
        zMove = Input.GetAxisRaw("Vertical");
    }

    void Move()
    {

        //velocity += new Vector3(xMove, 0, yMove) * speed;
        Vector3 playerMovement = new Vector3(xMove, 0, zMove);
        animator.SetBool("moving", playerMovement != Vector3.zero);

        if (playerMovement != Vector3.zero)
        {
            //https://youtu.be/4HpC--2iowE this helped
            float targetAngle = Mathf.Atan2(playerMovement.x, playerMovement.z) * Mathf.Rad2Deg + camera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            characterController.Move(moveDir.normalized * speed * Time.deltaTime);
        }
    }

    void JumpGravity()
    {
        float playerY;
        if(Input.GetKeyDown(KeyCode.Space))
        {
            playerY = Mathf.Sqrt(jumpPower * 3.0f * gravity);
        }
        else
        {
            playerY = -gravity;
        }

        velocity += new Vector3(0, playerY, 0);
    }


    void Integrate()
    {
        Move();
        JumpGravity();
        characterController.Move(Time.deltaTime * velocity);
        
        velocity = Vector3.zero;
    }

    void GroundCheck()
    {
        if (Physics.CheckBox(groundChecker.position, jumpDetect, transform.rotation, ground))
        {
            grounded = true;
        }
        else if (Physics.CheckBox(groundChecker.position, jumpDetect, transform.rotation, 8))
        {
            grounded = true;
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
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        animator.SetBool("grounded", grounded);

        Integrate();
    }

    private void FixedUpdate()
    {
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(groundChecker.position, jumpDetect);
    }
}
