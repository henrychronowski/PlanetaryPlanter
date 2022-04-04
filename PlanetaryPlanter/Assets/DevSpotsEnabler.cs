using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevSpotsEnabler : MonoBehaviour
{
    public GameObject container;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            if(Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.Q) && Input.GetKey(KeyCode.U) && Input.GetKey(KeyCode.I) && Input.GetKey(KeyCode.M) && Input.GetKey(KeyCode.B))
            {
                container.SetActive(true);
            }
    }
}
