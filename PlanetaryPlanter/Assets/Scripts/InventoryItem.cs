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
                //change this for rock planet specific info
                if (item.GetComponent<PlantTool>().planetSpecies == PlanetSpecies.RockPlanet)
                {
                    if (item.GetComponent<PlantTool>().modifier == ModifierTypes.VolcanicAsh)
                    {
                        iconDisplay.sprite = firePlanet;
                        AlmanacProgression.instance.Unlock("BurningPlanet");
                    }
                    if (item.GetComponent<PlantTool>().modifier == ModifierTypes.FrozenCore)
                    {
                        iconDisplay.sprite = icePlanet;
                        AlmanacProgression.instance.Unlock("FrozenPlanet");
                    }
                }

                //have to change this to match gas planet specific info
                if (item.GetComponent<PlantTool>().planetSpecies == PlanetSpecies.GasPlanet)
                {
                    if (item.GetComponent<PlantTool>().modifier == ModifierTypes.VolcanicAsh)
                    {
                        iconDisplay.sprite = firePlanet;
                        AlmanacProgression.instance.Unlock("BurningPlanet");
                    }
                    if (item.GetComponent<PlantTool>().modifier == ModifierTypes.FrozenCore)
                    {
                        iconDisplay.sprite = icePlanet;
                        AlmanacProgression.instance.Unlock("FrozenPlanet");
                    }
                }

                else if (item.GetComponent<PlantTool>().planetSpecies == PlanetSpecies.Star)
                {
                    if (item.GetComponent<PlantTool>().modifier == ModifierTypes.VolcanicAsh)
                    {
                        iconDisplay.sprite = fireStar;
                        AlmanacProgression.instance.Unlock("SuperheatedStar");
                    }
                    if (item.GetComponent<PlantTool>().modifier == ModifierTypes.FrozenCore)
                    {
                        iconDisplay.sprite = iceStar;
                        AlmanacProgression.instance.Unlock("ColdStar");
                    }
                }
                else if (item.GetComponent<PlantTool>().planetSpecies == PlanetSpecies.Asteroid)
                {
                    if (item.GetComponent<PlantTool>().modifier == ModifierTypes.VolcanicAsh)
                    {
                        iconDisplay.sprite = fireAsteroid;
                        AlmanacProgression.instance.Unlock("MagmaAsteroid");
                    }
                    if (item.GetComponent<PlantTool>().modifier == ModifierTypes.FrozenCore)
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
