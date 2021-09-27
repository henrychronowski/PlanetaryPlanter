using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowPlanet : MonoBehaviour
{
    public LayerMask observatoryMarker;
    void CheckInput()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                if(hitInfo.collider.gameObject.layer == 8)
                {
                    hitInfo.collider.gameObject.GetComponent<ObservatoryPlanetSpot>().PlaceObject(Inventory.instance.PopItem());
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
    }

    private void OnDrawGizmos()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit hitInfo);
        Gizmos.DrawLine(ray.origin, hitInfo.point);
    }
}
