using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public Rigidbody rgd;
    public float speed = 5f;
    public float yRotOffset;
    public float jumpPower = 15;
    float horizontal;
    float vertical;
    Vector3 dir = Vector3.zero;
    public bool lockMovement;

    //player rotation smoothness
    public Transform cam;
    public float turnSmoothness = 0.1f;
    float turnSmoothVelocity;

    void CheckInput()
    {
        if(!lockMovement)
        {

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if(Input.GetKeyDown(KeyCode.Space))
        {
            rgd.AddForce(Vector3.up * jumpPower);
        }
        }
        else
        {
            horizontal = 0;
            vertical = 0;
            rgd.velocity = new Vector3(rgd.velocity.x / 2, rgd.velocity.y, rgd.velocity.z / 2); //lowers x and z velocity quickly but keeps the ship bobbing in water
        }
    }

    public void LockMovement(bool setLock)
    {
        lockMovement = setLock;
        Debug.Log("Locking Movement: " + setLock);
    }

    private void Move()
    {

        dir = new Vector3(horizontal, 0f, vertical).normalized;

        if (dir.magnitude >= 0.1f)
        {
            //Rotate
            float targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            //Debug.Log("Target angle: " + targetAngle);
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle + yRotOffset, ref turnSmoothVelocity, turnSmoothness);
            transform.rotation = Quaternion.Euler(0, angle, 0);

            //Moving
            Vector3 moveDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            rgd.AddForce(moveDir.normalized * speed);

        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
    }

    private void FixedUpdate()
    {
        Move();
    }
}
