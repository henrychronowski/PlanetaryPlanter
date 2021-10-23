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

    [SerializeField]
    bool grounded;
    // Update is called once per frame

    private void Start()
    {
        rgd = GetComponent<Rigidbody>();
    }

    void Update()
    {
        moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        if(Physics.CheckSphere(groundCheck.position, groundCheckRadius, ground))
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
            rgd.AddForce(transform.up * jumpPower);
        }
    }

    void FixedUpdate()
    {
        //rgd.MovePosition(GetComponent<Rigidbody>().position + transform.TransformDirection(moveDir) * moveSpeed * Time.deltaTime);
        rgd.AddForce(transform.TransformDirection(moveDir) * moveSpeed);
       
        if(grounded)
            rgd.velocity = Vector3.ClampMagnitude(rgd.velocity, maxSpeed); 
        else
            rgd.velocity = Vector3.ClampMagnitude(rgd.velocity, maxSpeed*2);


        //GetComponent<PlayerGravityScript>().planet.Attract(transform);

        //transform.LookAt(new Ray(transform.position, moveDir).GetPoint(5f));
        //transform.RotateAround(transform.position, new Vector3(0, 1, 0));
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.FromToRotation(transform.up, moveDir), Time.deltaTime * 50);
    }

    private void OnDrawGizmos()
    {
        //rgd.position + transform.TransformDirection(moveDir) * moveSpeed * Time.deltaTime;
        
        Ray a = new Ray(transform.position, transform.TransformDirection(moveDir));
        Gizmos.DrawLine(transform.position, a.GetPoint(50));
    }
}
