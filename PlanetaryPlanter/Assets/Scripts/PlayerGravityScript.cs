using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGravityScript : MonoBehaviour
{
    [SerializeField]
    PlanetGravityScript planet;
    [SerializeField]
    public Vector3 gravityDir;
    
    public float gravityModifier = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        gravityDir = planet.Attract(transform, gravityModifier);
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
