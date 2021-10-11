using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGravityScript : MonoBehaviour
{
    public PlanetGravityScript planet;
    public bool onCylinder = false;
    public Vector3 gravityDir;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (onCylinder)
            gravityDir = planet.AttractCylinder(transform);
        else
            gravityDir = planet.Attract(transform);
    }

    private void OnDrawGizmos()
    {
        Ray ray = new Ray(transform.position, -gravityDir);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, ray.GetPoint(100f));
    }
}
