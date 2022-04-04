using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowPlanet : MonoBehaviour
{
    public LayerMask observatoryMarker;

    // Audio Manager Script is set up here
    private SoundManager soundManager;
    void PlacePlanet()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                if (hitInfo.collider.gameObject.layer == 8 && hitInfo.collider.gameObject.GetComponent<ObservatoryPlanetSpot>().filled == false)
                {
                    if (hitInfo.collider.gameObject.GetComponent<ObservatoryPlanetSpot>().PlaceObject(NewInventory.instance.GetItemInCursor()))
                        soundManager.PlaySound("PlacePlanet"); //only plays when placing successfully

                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //Audio Manager Is Opend Up here
        soundManager = SoundManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        PlacePlanet();
    }

    private void OnDrawGizmos()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit hitInfo);
        Gizmos.DrawLine(ray.origin, hitInfo.point);
    }
}
