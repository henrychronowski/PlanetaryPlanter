using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObservatoryPlanetSpot : MonoBehaviour
{
    public bool filled = false;
    public LayerMask plantLayer;

    public GameObject heldObject;

    public void PlaceObject(GameObject newObject)
    {
        newObject.transform.position = transform.position;
        newObject.transform.localScale = Vector3.one * 2f;
        GetComponent<MeshRenderer>().enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
