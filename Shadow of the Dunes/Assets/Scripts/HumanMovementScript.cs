using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class HumanMovementScript : MonoBehaviour
{
    private Rigidbody rBody;
    public float xMoveSpeed = 20.0f;
    public float zMoveSpeed = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        rBody = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        float horizontal = Input.GetAxis("Horizontal") * xMoveSpeed;
        float vertical = Input.GetAxis("Vertical") * zMoveSpeed;

        Vector3 direction = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            direction += new Vector3(Camera.main.transform.forward.x, 0f, Camera.main.transform.forward.z);
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction += new Vector3(Camera.main.transform.forward.x * -1f, 0f, Camera.main.transform.forward.z * -1f);
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction += new Vector3(Camera.main.transform.right.x * -1f, 0f, Camera.main.transform.right.z * -1f);
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += new Vector3(Camera.main.transform.right.x, 0f, Camera.main.transform.right.z);
        }

        rBody.velocity = direction;
    }
}
