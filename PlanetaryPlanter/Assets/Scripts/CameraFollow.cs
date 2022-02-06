using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float height = 5f;
    public float heightUpperLimit;
    public float heightLowerLimit;
    public float distance = 10f;
    public float distanceLowerLimit;
    public float distanceUpperLimit;
    public float rotDamping = 5f;
    public bool isEnabled = true;

    public Transform right;

    public float xRot;
    public float xRotSpeed;

    public float yRot;
    public float yRotSpeed;

    public float rotTest;
    public float yRotLimit = 70f;
    public float yRotLowerLimit = -70f;


    void CheckInput()
    {
        if(Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            distance--;
            if(distance < distanceLowerLimit)
            {
                distance = distanceLowerLimit;
            }
        }
        else if(Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            distance++;
            if (distance > distanceUpperLimit)
            {
                distance = distanceUpperLimit;
            }
        }

        xRot = Input.GetAxis("Mouse X") * xRotSpeed;
        yRot = Input.GetAxis("Mouse Y") * yRotSpeed * -1;

        height += Input.GetAxis("Mouse Y") * -1f * yRotSpeed;
        if (height > heightUpperLimit)
        {
            height = heightUpperLimit;
        }
        if(height < heightLowerLimit)
        {
            height = heightLowerLimit;
        }
        
        //Horizontal camera movement
        transform.RotateAround(target.position, target.transform.up, xRot);
        //transform.RotateAround(target.position, target.GetComponent<PlayerGravityScript>().gravityDir, xRot);

        //Vertical camera movement
        if(Mathf.Abs(rotTest + yRot) < yRotLimit)
        {
            rotTest += yRot;
            transform.RotateAround(target.position, (right.position - target.transform.position).normalized, yRot);
        }
        //else
        //{
        //    if (rotTest + yRot > yRotLimit)
        //    {
        //        rotTest = yRotLimit;
        //    }
        //    else
        //    {
        //        rotTest = -yRotLimit;
        //    }
        //}
        //target.RotateAround(target.position, transform.forward target.GetComponent<PlayerGravityScript>().gravityDir, xRot);

    }

    private void Start()
    {
        rotTest = transform.localRotation.x;
    }

    private void LateUpdate()
    {
        if(isEnabled)
        {
            CheckInput();
        }
        else
        {
            transform.SetPositionAndRotation(target.position, target.rotation);
        }

    }
}
