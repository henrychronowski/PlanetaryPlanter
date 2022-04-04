using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private CinemachineFreeLook freeLook;
    private CinemachineCollider collider;


    public bool canUseCamera;
    private CharacterMovement player;
    // Start is called before the first frame update
    void Start()
    {
        freeLook = GetComponent<CinemachineFreeLook>();
        collider = GetComponent<CinemachineCollider>();
        player = FindObjectOfType<CharacterMovement>();
        CinemachineCore.GetInputAxis = UpdateCinemachineAxis;
    }

    float UpdateCinemachineAxis(string axis)
    {
        if(canUseCamera)
        {
            return Input.GetAxis(axis);
        }
        else
        {
            return 0;
        }
    }

    public void CameraState(bool isActive)
    {
        canUseCamera = isActive;
        player.canMove = isActive;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
