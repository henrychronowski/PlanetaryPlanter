using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetRotationScript : MonoBehaviour
{
    public GameObject planet;
    float currentAngle = 0f;
    float rotationScale = 0.01f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentAngle += rotationScale;
        planet.transform.rotation = Quaternion.Euler(0, currentAngle, 25);
    }

}
