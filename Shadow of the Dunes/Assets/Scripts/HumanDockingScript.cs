using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanDockingScript : MonoBehaviour
{
    public bool canDock;
    public bool canReturn;

    public bool isDocked;
    public bool isSailing;

    public HumanMovementScript human;

    // Start is called before the first frame update
    void Start()
    {
        canDock = false;
        isDocked = false;

        canReturn = true;
        isSailing = true;

        human = gameObject.GetComponent<HumanMovementScript>();
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
            canReturn = true;
        }
    }

    void DockBoat()
    {
        if(isDocked && canReturn && Input.GetKeyDown(KeyCode.E))
        {
            isDocked = false;
            isSailing = true;
            human.LockMovement(isSailing);
        }
        else if(isSailing && canDock && Input.GetKeyDown(KeyCode.E))
        {
            isDocked = true;
            isSailing = false;
            human.LockMovement(isSailing);
        }
    }
}
