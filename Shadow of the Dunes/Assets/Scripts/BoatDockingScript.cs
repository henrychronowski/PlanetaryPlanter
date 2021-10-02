using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatDockingScript : MonoBehaviour
{
    public bool canDock;
    public bool isDocked;

    public ThirdPersonMovement boat;

    // Start is called before the first frame update
    void Start()
    {
        canDock = false;
        isDocked = false;
        boat = gameObject.GetComponent<ThirdPersonMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        DockBoat();
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            canDock = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            canDock = false;
        }
    }

    void DockBoat()
    {
        if(canDock == true && Input.GetKeyDown("space"))
        {
            if(isDocked == true)
            {
                isDocked = false;
                boat.LockMovement(isDocked);
            }
            else
            {
                isDocked = true;
                boat.LockMovement(isDocked);
            }
        }
    }
}
