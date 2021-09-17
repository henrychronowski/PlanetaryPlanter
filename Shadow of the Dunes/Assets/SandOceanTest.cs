using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandOceanTest : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "BuoyancyChecker")
        {
            other.GetComponent<BuoyancyPoint>().submerged = true;
        }
        //Debug.Log("Trigger Enter " + other.name);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "BuoyancyChecker")
        {
            other.GetComponent<BuoyancyPoint>().submerged = false;
        }
        //Debug.Log("Trigger Exit " + other.name);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision " + collision.gameObject.name);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }
}
