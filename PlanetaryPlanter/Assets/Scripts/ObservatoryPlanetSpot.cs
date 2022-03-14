using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlanetSpecies
{
    Asteroid,
    Planet,
    Star,
    RockPlanet,
    Comet,
    Unidentified
}

public enum PlanetType
{
    None,
    VolcanicAsh,
    FrozenCore,
    Sprout,
    MortalCoil,
    DewDrop,
    Fossilium
}


public class ObservatoryPlanetSpot : MonoBehaviour
{
    public bool filled = false;
    public LayerMask plantLayer;

    public GameObject heldObject;
    public OrbitPoint orbit;
    public Transform rotateAround;
    public float orbitSpeed;

    public PlanetType type;
    public PlanetSpecies species;

    public GameObject asteroidFruit;
    public GameObject starFruit;
    public GameObject planetFruit;

    public void PlaceObject(GameObject newObject, bool fromInventory = true)
    {
        if (newObject == null)
            return;

        if (newObject.tag != "Plant")
            return;

        if(!filled)
        {
            if(newObject.GetComponent<Plant>().type == type && newObject.GetComponent<Plant>().species == species)
            {
                if(fromInventory)
                    newObject = NewInventory.instance.PopItemInCursor();

                switch(newObject.GetComponent<Plant>().species)
                {
                    case PlanetSpecies.Asteroid:
                        {
                            newObject = Instantiate(asteroidFruit);
                            break;
                        }
                    case PlanetSpecies.Planet:
                        {
                            newObject = Instantiate(planetFruit);
                            break;
                        }
                    case PlanetSpecies.Star:
                        {
                            newObject = Instantiate(starFruit);
                            break;
                        }
                }
                heldObject = newObject;
                Vector3 scale = newObject.transform.localScale;
                newObject.transform.parent = transform;
                newObject.transform.localPosition = Vector3.zero;
                newObject.transform.localScale = scale;
                GetComponent<MeshRenderer>().enabled = false;
                filled = true;
                newObject.GetComponent<Plant>().inPot = false;
                //newObject.transform.GetComponentInChildren<TMPro.TextMeshProUGUI>().enabled = false;
            }
        }
        else
        {
            Debug.Log("Filled");
        }
        //Observatory.instance.
    }

    public void RemoveObject()
    {
        if (filled)
        {
            Destroy(heldObject);
            filled = false;
            GetComponent<MeshRenderer>().enabled = true;

        }
    }

    private void FixedUpdate()
    {
        transform.RotateAround(orbit.transform.position, new Vector3(0, 0, 1), orbitSpeed);
    
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
