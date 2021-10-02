using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Renderer;

public class HumanDockingScript : MonoBehaviour
{
    public bool canDock;
    public bool canReturn;

    public bool isDocked;
    public bool isSailing;

    public GameObject player;
    public HumanMovementScript movement;

    Renderer render;

    // Start is called before the first frame update
    void Start()
    {
        canDock = false;
        isDocked = false;

        canReturn = true;
        isSailing = true;

        movement = gameObject.GetComponent<HumanMovementScript>();
        render = player.GetComponent<Renderer>();
        render.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        DockBoat();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Dock")
        {
            canDock = true;
        }
        else if(col.gameObject.tag == "Boat")
        {
            canReturn = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Dock")
        {
            canDock = false;
        }
        else if (col.gameObject.tag == "Boat")
        {
            canReturn = false;
        }
    }

    void DockBoat()
    {
        if(isDocked && canReturn && Input.GetKeyDown(KeyCode.E))
        {
            isDocked = false;
            isSailing = true;
            movement.LockMovement(isSailing);
            render.enabled = isDocked;
        }
        else if(isSailing && canDock && Input.GetKeyDown(KeyCode.E))
        {
            isDocked = true;
            isSailing = false;
            movement.LockMovement(isSailing);
            render.enabled = isDocked;
        }
    }
}
