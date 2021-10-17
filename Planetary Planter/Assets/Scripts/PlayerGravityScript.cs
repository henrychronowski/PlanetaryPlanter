using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGravityScript : MonoBehaviour
{
    public PlanetGravityScript planet;
    public Vector3 gravityDir;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        gravityDir = planet.Attract(transform);
    }

    public void SetNewPlanet(PlanetGravityScript newPlanet)
    {
        planet = newPlanet;
    }
    private void OnDrawGizmos()
    {
        Ray ray = new Ray(transform.position, -gravityDir);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, ray.GetPoint(100f));
    }
}
