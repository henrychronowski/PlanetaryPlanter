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

    [SerializeField]
    bool grounded;

    float mouseX;

    [SerializeField]
    float rotSpeed;

    PlayerGravityScript gravity;
    // Update is called once per frame

    private void Start()
    {
        rgd = GetComponent<Rigidbody>();
        gravity = GetComponent<PlayerGravityScript>();
    }

    void Update()
    {
        moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;

        //Quaternion temp = Quaternion.AngleAxis(Camera.main.transform.rotation.y, gravity.gravityDir) * Quaternion.an;

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
        if(Input.GetKey(KeyCode.Space) && !grounded)
        {
            GetComponent<PlayerGravityScript>().gravityModifier = jumpGravityMod;
        }
        else
        {
            GetComponent<PlayerGravityScript>().gravityModifier = 1.0f;

        }
        mouseX = Input.GetAxis("Mouse X");
    }

    void FixedUpdate()
    {
        //rgd.MovePosition(GetComponent<Rigidbody>().position + transform.TransformDirection(moveDir) * moveSpeed * Time.deltaTime);
        
        

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

        transform.RotateAround(transform.position, new Vector3(0, 1, 0), mouseX*rotSpeed);
        
        
        //transform.localRotation = Quaternion.RotateTowards(transform.localRotation, 100f);
        //GetComponent<PlayerGravityScript>().planet.Attract(transform);

        Quaternion.LookRotation(transform.forward, gravity.gravityDir);

        //transform.Rotate(new Vector3(0, 1, 0), Quaternion.Angle(Quaternion.Euler(transform.forward), Camera.main.transform.rotation));

        Debug.Log("angle: " + Quaternion.Angle(Quaternion.Euler(transform.forward), Camera.main.transform.rotation));

        //transform.LookAt(new Ray(transform.position, moveDir).GetPoint(5f));
        //transform.RotateAround(transform.position, new Vector3(0, 1, 0));
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.FromToRotation(transform.up, moveDir), Time.deltaTime * 50);
    }

    private void OnDrawGizmos()
    {
        //rgd.position + transform.TransformDirection(moveDir) * moveSpeed * Time.deltaTime;
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position + transform.forward, 0.5f);
        Ray a = new Ray(transform.position, transform.TransformDirection(moveDir));
        Gizmos.DrawLine(transform.position, a.GetPoint(50));
    }
}
