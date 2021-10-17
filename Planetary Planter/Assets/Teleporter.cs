using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public PlanetGravityScript exitPlanetGravity;
    public Transform exitTransform;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.gameObject.transform.position = exitTransform.position;
            other.gameObject.GetComponent<PlayerGravityScript>().planet = exitPlanetGravity;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
