using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitPoint : MonoBehaviour
{
    public Vector3 axis;
    public Transform axisDir;
    // Start is called before the first frame update
    void Start()
    {
        axis = (axisDir.position - axis).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
