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
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "BuoyancyChecker")
        {
            other.GetComponent<BuoyancyPoint>().submerged = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
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
