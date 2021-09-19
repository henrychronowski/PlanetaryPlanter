using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class HumanMovementScript : MonoBehaviour
{
    float horizontal = 0f;
    float vertical = 0f;
    public float xSpeed = 5f;
    public float zSpeed = 5f;
    Rigidbody rBody;
    // Start is called before the first frame update
    void Start()
    {
        rBody = gameObject.GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        CheckInput();
    }
    void FixedUpdate()
    {
        Move();
    }
    void CheckInput()
    {
        horizontal = Input.GetAxis("Horizontal") * xSpeed;
        vertical = Input.GetAxis("Vertical") * zSpeed;
    }
    void Move()
    {
        Vector3 forward = Camera.main.transform.forward;
        forward.y = 0f;

        Vector3 right = Camera.main.transform.right;

        Vector3 finalVel = right * horizontal + forward * vertical;

        if (horizontal != 0 && vertical != 0)
        {
            finalVel.z *= 0.77f;
            finalVel.x *= 0.77f;
        }
        rBody.velocity = finalVel;
    }
}