using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PlanetSpecies
{
    Asteroid,
    GasPlanet,
    Star,
    RockyPlanet,
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
    public GameObject cometFruit;
    public GameObject rockyFruit;

    public bool PlaceObject(GameObject newObject, bool fromInventory = true)
    {
        if (newObject == null)
            return false;

        if (newObject.tag != "Plant")
            return false;

        if(!filled)
        {
            if(newObject.GetComponent<Plant>().type == type && newObject.GetComponent<Plant>().species == species)
            {
                if (newObject.GetComponent<Plant>().stage == Plant.Stage.Rotten)
                    return false;

                if(fromInventory)
                    newObject = NewInventory.instance.PopItemInCursor();

                GameObject fruit = null;

                switch(newObject.GetComponent<Plant>().species)
                {
                    case PlanetSpecies.Asteroid:
                        {
                            fruit = Instantiate(asteroidFruit);
                            break;
                        }
                    case PlanetSpecies.GasPlanet:
                        {
                            fruit = Instantiate(asteroidFruit);
                            break;
                        }
                    case PlanetSpecies.Star:
                        {
                            fruit = Instantiate(asteroidFruit);
                            break;
                        }
                    case PlanetSpecies.RockyPlanet:
                        {
                            fruit = Instantiate(asteroidFruit);
                            break;
                        }
                    case PlanetSpecies.Comet:
                        {
                            fruit = Instantiate(asteroidFruit);
                            break;
                        }
                }
                heldObject = fruit;
                Vector3 scale = fruit.transform.localScale;
                fruit.transform.parent = transform;
                fruit.transform.localPosition = Vector3.zero;
                fruit.transform.localScale = scale;
                GetComponent<MeshRenderer>().enabled = false;
                
                filled = true;
                if(newObject.TryGetComponent<Plant>(out Plant plant))
                {
                    Plant plantObject = newObject.GetComponent<Plant>();
                    Sprite spriteToUse = GetComponent<PlanetInformationScript>().ReturnSpriteToDisplay(plantObject.species, plantObject.type);
                    fruit.GetComponentInChildren<Image>().sprite = spriteToUse;
                    plant.GetComponent<Plant>().inPot = false;
                    //transform.parent.parent.parent.GetComponent<Observatory>().CheckForCompletion();
                }

                return true;
            }
        }
        else
        {
            return false;
        }
        return false;
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
