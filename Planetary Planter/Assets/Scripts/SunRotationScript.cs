using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunRotationScript : MonoBehaviour
{
    public GameObject sun;
    float CurrentAngle = -30.0f;
    float RotationScale = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CurrentAngle += RotationScale;
        if(CurrentAngle >= 180.0f)
        {
            CurrentAngle = -CurrentAngle;
        }
        sun.transform.rotation = Quaternion.Euler(0, CurrentAngle, 0);
    }
}
