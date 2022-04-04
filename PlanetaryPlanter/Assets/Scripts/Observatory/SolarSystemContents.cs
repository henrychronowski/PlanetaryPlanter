using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarSystemContents : MonoBehaviour
{
    public Observatory observatory;
    // Start is called before the first frame update
    void Start()
    {
        int index = int.Parse(gameObject.name);
        observatory = transform.parent.Find("SSView" + index.ToString()).gameObject.GetComponent<Observatory>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
