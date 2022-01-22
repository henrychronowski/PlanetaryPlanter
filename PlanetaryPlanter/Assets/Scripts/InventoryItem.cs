using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    public bool initted;
    public GameObject itemObject;

    private Image iconDisplay;

    public Sprite fireAsteroid;
    public Sprite iceAsteroid;

    public Sprite firePlanet;
    public Sprite icePlanet;

    public Sprite fireStar;
    public Sprite iceStar;

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
                }

                else if (item.GetComponent<Plant>().species == PlanetSpecies.Star)
                {
                    if (item.GetComponent<Plant>().type == PlanetType.VolcanicAsh)
                    {
                        iconDisplay.sprite = fireStar;
                        AlmanacProgression.instance.Unlock("SuperheatedStar");
                    }
                    if (item.GetComponent<Plant>().type == PlanetType.FrozenCore)
                    {
                        iconDisplay.sprite = iceStar;
                        AlmanacProgression.instance.Unlock("ColdStar");
                    }
                }
                else if (item.GetComponent<Plant>().species == PlanetSpecies.Asteroid)
                {
                    if (item.GetComponent<Plant>().type == PlanetType.VolcanicAsh)
                    {
                        iconDisplay.sprite = fireAsteroid;
                        AlmanacProgression.instance.Unlock("MagmaAsteroid");
                    }
                    if (item.GetComponent<Plant>().type == PlanetType.FrozenCore)
                    {
                        iconDisplay.sprite = iceAsteroid;
                        AlmanacProgression.instance.Unlock("CometCluster");
                    }
                }
            }
        }
        initted = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
