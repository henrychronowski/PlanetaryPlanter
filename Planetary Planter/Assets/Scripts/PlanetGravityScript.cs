using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetGravityScript : MonoBehaviour
{
    public float gravity = -10;
    public Vector3 updatedPosForZ;
    public Transform center;
    public float range;
    public bool isCylinder;
    public Vector3 AttractSphere(Transform body)
    {
        
        Vector3 gravityUp = (body.position - transform.position).normalized;
        Vector3 bodyUp = body.up;

        body.GetComponent<Rigidbody>().AddForce(gravityUp * gravity);

        Quaternion targetRotation = Quaternion.FromToRotation(bodyUp, gravityUp) * body.rotation;
        body.rotation = Quaternion.Slerp(body.rotation, targetRotation, 50 * Time.deltaTime);
        return gravityUp;
    }

    public Vector3 AttractCylinder(Transform body)
    {
        updatedPosForZ = new Vector3(center.position.x, center.position.y, body.position.z);
        Vector3 gravityUp = (body.position - updatedPosForZ).normalized;
        Vector3 bodyUp = body.up;

        body.GetComponent<Rigidbody>().AddForce(gravityUp * gravity);

        Quaternion targetRotation = Quaternion.FromToRotation(bodyUp, gravityUp) * body.rotation;
        body.rotation = Quaternion.Slerp(body.rotation, targetRotation, 50 * Time.deltaTime);

        return gravityUp;
    }

    public Vector3 Attract(Transform body)
    {
        if (isCylinder)
            return AttractCylinder(body);
        else
            return AttractSphere(body);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(updatedPosForZ, 1.0f);
    }
}
