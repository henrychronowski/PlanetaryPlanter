using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class InventoryItem : MonoBehaviour
{
    public GameObject itemObject;

    private Image iconDisplay;

    public Sprite fireAsteroid;
    public Sprite iceAsteroid;
    public Sprite ghostAsteroid;
    public Sprite fossilAsteroid;
    public Sprite waterAsteroid;
    public Sprite grassAsteroid;

    public Sprite firePlanet;
    public Sprite icePlanet;
    public Sprite ghostPlanet;
    public Sprite fossilPlanet;
    public Sprite waterPlanet;
    public Sprite grassPlanet;

    public Sprite fireStar;
    public Sprite iceStar;
    public Sprite ghostStar;
    public Sprite fossilStar;
    public Sprite waterStar;
    public Sprite grassStar;

    public Sprite fireComet;
    public Sprite iceComet;
    public Sprite ghostComet;
    public Sprite fossilComet;
    public Sprite waterComet;
    public Sprite grassComet;

    public Sprite fireRockyPlanet;
    public Sprite iceRockyPlanet;
    public Sprite ghostRockyPlanet;
    public Sprite fossilRockyPlanet;
    public Sprite waterRockyPlanet;
    public Sprite grassRockyPlanet;

    public ItemID itemID;
   
    public void Init(GameObject item)
    {
        itemObject = item;
        iconDisplay = GetComponent<Image>();
        if (itemObject)
        {
            //check to see if it has an image component to use as its icon in the inventory screen
            iconDisplay.sprite = itemObject.GetComponent<IconHolder>().icon;
            if(item.tag == "Plant")
            {
                if(item.GetComponent<Plant>().species == PlanetSpecies.Planet)
                {
                    if (item.GetComponent<Plant>().type == PlanetType.VolcanicAsh)
                    {
                        iconDisplay.sprite = firePlanet;
                        AlmanacProgression.instance.Unlock("BurningPlanet");
                    }
                    if (item.GetComponent<Plant>().type == PlanetType.FrozenCore)
                    {
                        iconDisplay.sprite = icePlanet;
                        AlmanacProgression.instance.Unlock("FrozenPlanet");
                    }
                    if (item.GetComponent<Plant>().type == PlanetType.MortalCoil)
                    {
                        iconDisplay.sprite = ghostPlanet;
                        AlmanacProgression.instance.Unlock("GhostlyPlanet");
                    }
                    if (item.GetComponent<Plant>().type == PlanetType.Fossilium)
                    {
                        iconDisplay.sprite = fossilPlanet;
                        AlmanacProgression.instance.Unlock("FossilPlanet");
                    }
                    if (item.GetComponent<Plant>().type == PlanetType.DewDrop)
                    {
                        iconDisplay.sprite = waterPlanet;
                        AlmanacProgression.instance.Unlock("WaterPlanet");
                    }
                    if (item.GetComponent<Plant>().type == PlanetType.Sprout)
                    {
                        iconDisplay.sprite = grassPlanet;
                        AlmanacProgression.instance.Unlock("GrassyPlanet");
                    }
                }

                if (item.GetComponent<Plant>().species == PlanetSpecies.Star)
                {
                    if (item.GetComponent<Plant>().type == PlanetType.VolcanicAsh)
                    {
                        iconDisplay.sprite = fireStar;
                        AlmanacProgression.instance.Unlock("BurningStar");
                    }
                    if (item.GetComponent<Plant>().type == PlanetType.FrozenCore)
                    {
                        iconDisplay.sprite = iceStar;
                        AlmanacProgression.instance.Unlock("FrozenStar");
                    }
                    if (item.GetComponent<Plant>().type == PlanetType.MortalCoil)
                    {
                        iconDisplay.sprite = ghostStar;
                        AlmanacProgression.instance.Unlock("GhostlyStar");
                    }
                    if (item.GetComponent<Plant>().type == PlanetType.Fossilium)
                    {
                        iconDisplay.sprite = fossilStar;
                        AlmanacProgression.instance.Unlock("FossilStar");
                    }
                    if (item.GetComponent<Plant>().type == PlanetType.DewDrop)
                    {
                        iconDisplay.sprite = waterStar;
                        AlmanacProgression.instance.Unlock("WaterStar");
                    }
                    if (item.GetComponent<Plant>().type == PlanetType.Sprout)
                    {
                        iconDisplay.sprite = grassStar;
                        AlmanacProgression.instance.Unlock("GrassyStar");
                    }
                }

                if (item.GetComponent<Plant>().species == PlanetSpecies.Asteroid)
                {
                    if (item.GetComponent<Plant>().type == PlanetType.VolcanicAsh)
                    {
                        iconDisplay.sprite = fireAsteroid;
                        AlmanacProgression.instance.Unlock("BurningAsteroid");
                    }
                    if (item.GetComponent<Plant>().type == PlanetType.FrozenCore)
                    {
                        iconDisplay.sprite = iceAsteroid;
                        AlmanacProgression.instance.Unlock("FrozenAsteroid");
                    }
                    if (item.GetComponent<Plant>().type == PlanetType.MortalCoil)
                    {
                        iconDisplay.sprite = ghostAsteroid;
                        AlmanacProgression.instance.Unlock("GhostlyAsteroid");
                    }
                    if (item.GetComponent<Plant>().type == PlanetType.Fossilium)
                    {
                        iconDisplay.sprite = fossilAsteroid;
                        AlmanacProgression.instance.Unlock("FossilAsteroid");
                    }
                    if (item.GetComponent<Plant>().type == PlanetType.DewDrop)
                    {
                        iconDisplay.sprite = waterAsteroid;
                        AlmanacProgression.instance.Unlock("WaterAsteroid");
                    }
                    if (item.GetComponent<Plant>().type == PlanetType.Sprout)
                    {
                        iconDisplay.sprite = grassAsteroid;
                        AlmanacProgression.instance.Unlock("GrassyAsteroid");
                    }
                }

                if (item.GetComponent<Plant>().species == PlanetSpecies.Comet)
                {
                    if (item.GetComponent<Plant>().type == PlanetType.VolcanicAsh)
                    {
                        iconDisplay.sprite = fireComet;
                        AlmanacProgression.instance.Unlock("BurningComet");
                    }
                    if (item.GetComponent<Plant>().type == PlanetType.FrozenCore)
                    {
                        iconDisplay.sprite = iceComet;
                        AlmanacProgression.instance.Unlock("FrozenComet");
                    }
                    if (item.GetComponent<Plant>().type == PlanetType.MortalCoil)
                    {
                        iconDisplay.sprite = ghostComet;
                        AlmanacProgression.instance.Unlock("GhostlyComet");
                    }
                    if (item.GetComponent<Plant>().type == PlanetType.Fossilium)
                    {
                        iconDisplay.sprite = fossilComet;
                        AlmanacProgression.instance.Unlock("FossilComet");
                    }
                    if (item.GetComponent<Plant>().type == PlanetType.DewDrop)
                    {
                        iconDisplay.sprite = waterComet;
                        AlmanacProgression.instance.Unlock("WaterComet");
                    }
                    if (item.GetComponent<Plant>().type == PlanetType.Sprout)
                    {
                        iconDisplay.sprite = grassComet;
                        AlmanacProgression.instance.Unlock("GrassyComet");
                    }
                }

                if (item.GetComponent<Plant>().species == PlanetSpecies.RockPlanet)
                {
                    if (item.GetComponent<Plant>().type == PlanetType.VolcanicAsh)
                    {
                        iconDisplay.sprite = fireRockyPlanet;
                        AlmanacProgression.instance.Unlock("BurningRockyPlanet");
                    }
                    if (item.GetComponent<Plant>().type == PlanetType.FrozenCore)
                    {
                        iconDisplay.sprite = iceRockyPlanet;
                        AlmanacProgression.instance.Unlock("FrozenRockyPlanet");
                    }
                    if (item.GetComponent<Plant>().type == PlanetType.MortalCoil)
                    {
                        iconDisplay.sprite = ghostRockyPlanet;
                        AlmanacProgression.instance.Unlock("GhostlyRockyPlanet");
                    }
                    if (item.GetComponent<Plant>().type == PlanetType.Fossilium)
                    {
                        iconDisplay.sprite = fossilRockyPlanet;
                        AlmanacProgression.instance.Unlock("FossilRockyPlanet");
                    }
                    if (item.GetComponent<Plant>().type == PlanetType.DewDrop)
                    {
                        iconDisplay.sprite = waterRockyPlanet;
                        AlmanacProgression.instance.Unlock("WaterRockyPlanet");
                    }
                    if (item.GetComponent<Plant>().type == PlanetType.Sprout)
                    {
                        iconDisplay.sprite = grassRockyPlanet;
                        AlmanacProgression.instance.Unlock("GrassyRockyPlanet");
                    }
                }

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
