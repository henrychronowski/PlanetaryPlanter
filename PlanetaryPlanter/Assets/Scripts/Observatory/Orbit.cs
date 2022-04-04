using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    public Vector3 axis;
    public Transform axisDir;

    public void TestFunc()
    {
        axis = (axisDir.position - axis).normalized;

    }

    // Start is called before the first frame update
    void Start()
    {
        TestFunc();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
