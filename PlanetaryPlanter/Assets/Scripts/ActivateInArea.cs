using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateInArea : MonoBehaviour
{
    public bool deactiveStart = true;
    public GameObject[] affectedObjects;
    
    //If deactive at start is selected it will deactivate all affected objects
    void Start()
    {
        if(deactiveStart)
        {
            foreach (GameObject go in affectedObjects)
            {
                go.SetActive(false);
            }
        }

    }

    //Activates all affected objects when player enters area
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
            foreach (GameObject go in affectedObjects)
            {
                go.SetActive(true);
            }
    }

    //Deactivates all affected objects when player enters area
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            foreach (GameObject go in affectedObjects)
            {
                go.SetActive(false);
            }
    }
}
