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

    public float xRot;
    public float xRotSpeed;

    public float yRotSpeed;

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
        if(Input.GetKey(KeyCode.Mouse1))
        {

                height += Input.GetAxis("Mouse Y") * -1f * yRotSpeed;
                if(height > heightUpperLimit)
                {
                    height = heightUpperLimit;
                }
                if(height < heightLowerLimit)
                {
                    height = heightLowerLimit;
                }

            transform.RotateAround(target.position, new Vector3(0.0f, 1.0f, 0.0f), xRot);
                //target.Rotate(new Vector3(0f, xRot, 0f), Space.World);

        }


    }

    private void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        //target.Rotate(0f, xRot, 0f);
    }

    private void LateUpdate()
    {
        if(isEnabled)
        {
            CheckInput();
            float wantedRotationAngle = target.eulerAngles.y;
            float currentRotationAngle = transform.eulerAngles.y;
            float wantedHeight = target.position.y + height;
            float currentHeight = transform.position.y;
            currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotDamping * Time.deltaTime);
            //LERP the height
            currentHeight = Mathf.Lerp(currentHeight, wantedHeight, rotDamping * Time.deltaTime);
            //Get the rotation
            Quaternion currentRotation = Quaternion.Euler(0f, currentRotationAngle, 0f);
            //position the camera
            transform.position = target.transform.position;
            //set its offset distance
            transform.position -= currentRotation * Vector3.forward * distance;
            //set its offset height
            Vector3 newHeight = transform.position;
            newHeight.y = wantedHeight;
            transform.position = newHeight;
            
         

            //currentRotation.Rotate(0f, rot, 0f);
            transform.LookAt(target);
        }
        else
        {
            transform.SetPositionAndRotation(target.position, target.rotation);
        }

    }
}
