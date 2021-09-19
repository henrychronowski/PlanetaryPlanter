using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuoyancyPoint : MonoBehaviour
{
    public bool submerged = false;
    public float buoyancyFactor = 1;
    public LayerMask sand;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Physics.CheckBox(GetComponent<BoxCollider>().center, new Vector3(0.5f, 0.45f, 0.5f), Quaternion.identity, sand))
        {
            submerged = true;
        }
    }
}
