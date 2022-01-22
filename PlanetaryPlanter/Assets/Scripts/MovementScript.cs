using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    public float moveSpeed = 15.0f;
    private Vector3 moveDir;
    private Rigidbody rgd;

    public Transform groundCheck;
    public LayerMask ground;
    public float groundCheckRadius;


    public float jumpPower;
    public float maxSpeed;
    public float jumpGravityMod;
    public float groundedDragWhenNotMoving;

    [SerializeField]
    bool grounded;

    float mouseX;

    [SerializeField]
    float rotSpeed;

    AudioSource jump;

    public float sizeOfJumpDetect;
    public Vector3 jumpDetect;

    PlayerGravityScript gravity;

    public Animator animator;
    // Update is called once per frame

    private void Start()
    {
        rgd = GetComponent<Rigidbody>();
        gravity = GetComponent<PlayerGravityScript>();
        jump = GetComponent<AudioSource>();
    }

    void Update()
    {
        moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;

        animator.SetBool("moving", moveDir != Vector3.zero);

        animator.SetBool("grounded", grounded);
        //Quaternion temp = Quaternion.AngleAxis(Camera.main.transform.rotation.y, gravity.gravityDir) * Quaternion.an;

        if(Physics.CheckBox(groundCheck.position, jumpDetect, transform.rotation, ground))
        {
            grounded = true;
        }
        else if (Physics.CheckBox(groundCheck.position, jumpDetect, transform.rotation, 8))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
        CheckInput();
    }

    void CheckInput()
    {
        if(Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            Jump();
        }
        if(Input.GetKey(KeyCode.Space) && !grounded)
        {
            GetComponent<PlayerGravityScript>().gravityModifier = jumpGravityMod;
        }
        else
        {
            GetComponent<PlayerGravityScript>().gravityModifier = 1.0f;

        }

        //if (Input.GetMouseButton(2))
        //{
        //    mouseX = Input.GetAxis("Mouse X");
        //}
        //else
        //{
        //    mouseX = 0;
        //}
    }

    void Jump()
    {
        rgd.AddForce(transform.up * jumpPower);
        jump.Play();
    }

    void FixedUpdate()
    {
        if (grounded)
        {
            rgd.AddForce(transform.TransformDirection(moveDir) * moveSpeed);
            rgd.velocity = Vector3.ClampMagnitude(rgd.velocity, maxSpeed);

        }
        else
        {
            rgd.AddForce(transform.TransformDirection(moveDir) * (moveSpeed/4));
            rgd.velocity = Vector3.ClampMagnitude(rgd.velocity, maxSpeed * 2);
        }

        if(grounded && moveDir == Vector3.zero)
        {
            rgd.velocity /= groundedDragWhenNotMoving;
        }
        

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position + transform.forward, 0.5f);
        Ray a = new Ray(transform.position, transform.TransformDirection(moveDir));
        Gizmos.DrawLine(transform.position, a.GetPoint(50));
    }
}
