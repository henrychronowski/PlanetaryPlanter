using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressureSwitchScript : MonoBehaviour
{
    public GameObject obj;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision key)
    {
        if(key.gameObject.tag == "PressureSwitchKey")
        {
            Destroy(obj);
        }
    }
}
