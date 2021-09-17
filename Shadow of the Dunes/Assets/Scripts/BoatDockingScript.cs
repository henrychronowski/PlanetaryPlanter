using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatDockingScript : MonoBehaviour
{
    bool canDock;

    // Start is called before the first frame update
    void Start()
    {
        canDock = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter()
    {
        if(gameObject.layer == 8)
        {
            canDock = true;
        }
    }

    void OnTriggerExit()
    {
        if(gameObject.layer == 8)
        {
            canDock = false;
        }
    }
}
