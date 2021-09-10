using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormMovementTest : MonoBehaviour
{
    private Rigidbody rgd;

    [SerializeField]
    float force = 10f;

    [SerializeField]
    float clamp = 15f;
    void CheckInput()
    {
        Vector3 direction = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            direction += new Vector3(Camera.main.transform.forward.x, 0f, Camera.main.transform.forward.z);
            
        }

        if (Input.GetKey(KeyCode.A))
        {
            direction += new Vector3(Camera.main.transform.right.x * -1f, 0f, Camera.main.transform.right.z * -1f);

        }

        if (Input.GetKey(KeyCode.S))
        {
            direction += new Vector3(Camera.main.transform.forward.x *-1f, 0f, Camera.main.transform.forward.z * -1f);

        }

        if (Input.GetKey(KeyCode.D))
        {
            direction += new Vector3(Camera.main.transform.right.x, 0f, Camera.main.transform.right.z);

        }


        if (Input.GetKey(KeyCode.Space))
        {
            direction += new Vector3(0f, 1f, 0f);
        }
        Vector3 vel = direction * force;
        Vector3.ClampMagnitude(vel, 15f);
        rgd.AddForce(vel);
    }

    // Start is called before the first frame update
    void Start()
    {
        rgd = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        CheckInput();
    }
}
