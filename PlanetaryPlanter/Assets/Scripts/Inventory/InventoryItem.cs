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

    InventoryItemIndex index;

    private RectTransform rect;
    public void Init(GameObject item)
    {
        if(index == null)
            index = FindObjectOfType<InventoryItemIndex>();

        itemObject = item;
        iconDisplay = GetComponent<Image>();

        if (itemObject)
        {
            //check to see if it has an image component to use as its icon in the inventory screen
            iconDisplay.sprite = itemObject.GetComponent<IconHolder>().icon;
            if(item.tag == "Plant")
            {
                if(item.GetComponent<Plant>().species == PlanetSpecies.GasPlanet)
                {
                    GameObject plantItem = index.items[(int)ItemID.PlanetPlant];
                    if (item.GetComponent<Plant>().type == PlanetType.VolcanicAsh)
                    {
                        iconDisplay.sprite = firePlanet;
                        string itemname = plantItem.GetComponent<TooltipInfo>().fireModName.Replace(" ", "");
                        AlmanacProgression.instance.Unlock(itemname);
                        AlmanacProgression.instance.Unlock("BurningPlanet");
                    }
                    if (item.GetComponent<Plant>().type == PlanetType.FrozenCore)
                    {
                        string itemname = plantItem.GetComponent<TooltipInfo>().iceModName.Replace(" ", "");
                        AlmanacProgression.instance.Unlock(itemname);
                        iconDisplay.sprite = icePlanet;
                        AlmanacProgression.instance.Unlock("FrozenPlanet");
                    }
                    if (item.GetComponent<Plant>().type == PlanetType.MortalCoil)
                    {
                        string itemname = plantItem.GetComponent<TooltipInfo>().ghostModName.Replace(" ", "");
                        AlmanacProgression.instance.Unlock(itemname);
                        iconDisplay.sprite = ghostPlanet;
                        AlmanacProgression.instance.Unlock("GhostlyPlanet");
                    }
                    if (item.GetComponent<Plant>().type == PlanetType.Fossilium)
                    {
                        string itemname = plantItem.GetComponent<TooltipInfo>().fossilModName.Replace(" ", "");
                        AlmanacProgression.instance.Unlock(itemname);
                        iconDisplay.sprite = fossilPlanet;
                        AlmanacProgression.instance.Unlock("FossilPlanet");
                    }
                    if (item.GetComponent<Plant>().type == PlanetType.DewDrop)
                    {
                        string itemname = plantItem.GetComponent<TooltipInfo>().waterModName.Replace(" ", "");
                        AlmanacProgression.instance.Unlock(itemname);
                        iconDisplay.sprite = waterPlanet;
                        AlmanacProgression.instance.Unlock("WaterPlanet");
                    }
                    if (item.GetComponent<Plant>().type == PlanetType.Sprout)
                    {
                        string itemname = plantItem.GetComponent<TooltipInfo>().grassModName.Replace(" ", "");
                        AlmanacProgression.instance.Unlock(itemname);
                        iconDisplay.sprite = grassPlanet;
                        AlmanacProgression.instance.Unlock("GrassyPlanet");
                    }
                }

                if (item.GetComponent<Plant>().species == PlanetSpecies.Star)
                {
                    GameObject plantItem = index.items[(int)ItemID.StarPlant];

                    if (item.GetComponent<Plant>().type == PlanetType.VolcanicAsh)
                    {
                        iconDisplay.sprite = fireStar;
                        string itemname = plantItem.GetComponent<TooltipInfo>().fireModName.Replace(" ", "");
                        AlmanacProgression.instance.Unlock(itemname);
                        AlmanacProgression.instance.Unlock("BurningStar");
                    }
                    if (item.GetComponent<Plant>().type == PlanetType.FrozenCore)
                    {
                        string itemname = plantItem.GetComponent<TooltipInfo>().iceModName.Replace(" ", "");
                        AlmanacProgression.instance.Unlock(itemname);
                        iconDisplay.sprite = iceStar;

                        AlmanacProgression.instance.Unlock("FrozenStar");
                    }
                    if (item.GetComponent<Plant>().type == PlanetType.MortalCoil)
                    {
                        string itemname = plantItem.GetComponent<TooltipInfo>().ghostModName.Replace(" ", "");
                        AlmanacProgression.instance.Unlock(itemname);
                        iconDisplay.sprite = ghostStar;
                        AlmanacProgression.instance.Unlock("GhostlyStar");
                    }
                    if (item.GetComponent<Plant>().type == PlanetType.Fossilium)
                    {
                        string itemname = plantItem.GetComponent<TooltipInfo>().fossilModName.Replace(" ", "");
                        AlmanacProgression.instance.Unlock(itemname);
                        iconDisplay.sprite = fossilStar;
                        AlmanacProgression.instance.Unlock("FossilStar");
                    }
                    if (item.GetComponent<Plant>().type == PlanetType.DewDrop)
                    {
                        string itemname = plantItem.GetComponent<TooltipInfo>().waterModName.Replace(" ", "");
                        AlmanacProgression.instance.Unlock(itemname);
                        iconDisplay.sprite = waterStar;
                        AlmanacProgression.instance.Unlock("WaterStar");
                    }
                    if (item.GetComponent<Plant>().type == PlanetType.Sprout)
                    {
                        string itemname = plantItem.GetComponent<TooltipInfo>().grassModName.Replace(" ", "");
                        AlmanacProgression.instance.Unlock(itemname);
                        iconDisplay.sprite = grassStar;
                        AlmanacProgression.instance.Unlock("GrassyStar");
                    }
                }

                if (item.GetComponent<Plant>().species == PlanetSpecies.Asteroid)
                {
                    GameObject plantItem = index.items[(int)ItemID.AsteroidPlant];

                    if (item.GetComponent<Plant>().type == PlanetType.VolcanicAsh)
                    {
                        string itemname = plantItem.GetComponent<TooltipInfo>().fireModName.Replace(" ", "");
                        AlmanacProgression.instance.Unlock(itemname);
                        iconDisplay.sprite = fireAsteroid;
                        AlmanacProgression.instance.Unlock("BurningAsteroid");
                    }
                    if (item.GetComponent<Plant>().type == PlanetType.FrozenCore)
                    {
                        string itemname = plantItem.GetComponent<TooltipInfo>().iceModName.Replace(" ", "");
                        AlmanacProgression.instance.Unlock(itemname);
                        iconDisplay.sprite = iceAsteroid;
                        AlmanacProgression.instance.Unlock("FrozenAsteroid");
                    }
                    if (item.GetComponent<Plant>().type == PlanetType.MortalCoil)
                    {
                        string itemname = plantItem.GetComponent<TooltipInfo>().ghostModName.Replace(" ", "");
                        AlmanacProgression.instance.Unlock(itemname);
                        iconDisplay.sprite = ghostAsteroid;
                        AlmanacProgression.instance.Unlock("GhostlyAsteroid");
                    }
                    if (item.GetComponent<Plant>().type == PlanetType.Fossilium)
                    {
                        string itemname = plantItem.GetComponent<TooltipInfo>().fossilModName.Replace(" ", "");
                        AlmanacProgression.instance.Unlock(itemname);
                        iconDisplay.sprite = fossilAsteroid;
                        AlmanacProgression.instance.Unlock("FossilAsteroid");
                    }
                    if (item.GetComponent<Plant>().type == PlanetType.DewDrop)
                    {
                        string itemname = plantItem.GetComponent<TooltipInfo>().waterModName.Replace(" ", "");
                        AlmanacProgression.instance.Unlock(itemname);
                        iconDisplay.sprite = waterAsteroid;
                        AlmanacProgression.instance.Unlock("WaterAsteroid");
                    }
                    if (item.GetComponent<Plant>().type == PlanetType.Sprout)
                    {
                        string itemname = plantItem.GetComponent<TooltipInfo>().grassModName.Replace(" ", "");
                        AlmanacProgression.instance.Unlock(itemname);
                        iconDisplay.sprite = grassAsteroid;
                        AlmanacProgression.instance.Unlock("GrassyAsteroid");
                    }
                }

                if (item.GetComponent<Plant>().species == PlanetSpecies.Comet)
                {
                    GameObject plantItem = index.items[(int)ItemID.CometPlant];

                    if (item.GetComponent<Plant>().type == PlanetType.VolcanicAsh)
                    {
                        string itemname = plantItem.GetComponent<TooltipInfo>().fireModName.Replace(" ", "");
                        AlmanacProgression.instance.Unlock(itemname);
                        iconDisplay.sprite = fireComet;
                        AlmanacProgression.instance.Unlock("BurningComet");
                    }
                    if (item.GetComponent<Plant>().type == PlanetType.FrozenCore)
                    {
                        string itemname = plantItem.GetComponent<TooltipInfo>().iceModName.Replace(" ", "");
                        AlmanacProgression.instance.Unlock(itemname);
                        iconDisplay.sprite = iceComet;
                        AlmanacProgression.instance.Unlock("FrozenComet");
                    }
                    if (item.GetComponent<Plant>().type == PlanetType.MortalCoil)
                    {
                        string itemname = plantItem.GetComponent<TooltipInfo>().ghostModName.Replace(" ", "");
                        AlmanacProgression.instance.Unlock(itemname);
                        iconDisplay.sprite = ghostComet;
                        AlmanacProgression.instance.Unlock("GhostlyComet");
                    }
                    if (item.GetComponent<Plant>().type == PlanetType.Fossilium)
                    {
                        string itemname = plantItem.GetComponent<TooltipInfo>().fossilModName.Replace(" ", "");
                        AlmanacProgression.instance.Unlock(itemname);
                        iconDisplay.sprite = fossilComet;
                        AlmanacProgression.instance.Unlock("FossilComet");
                    }
                    if (item.GetComponent<Plant>().type == PlanetType.DewDrop)
                    {
                        string itemname = plantItem.GetComponent<TooltipInfo>().waterModName.Replace(" ", "");
                        AlmanacProgression.instance.Unlock(itemname);
                        iconDisplay.sprite = waterComet;
                        AlmanacProgression.instance.Unlock("WaterComet");
                    }
                    if (item.GetComponent<Plant>().type == PlanetType.Sprout)
                    {
                        string itemname = plantItem.GetComponent<TooltipInfo>().grassModName.Replace(" ", "");
                        AlmanacProgression.instance.Unlock(itemname);
                        iconDisplay.sprite = grassComet;
                        AlmanacProgression.instance.Unlock("GrassyComet");
                    }
                }

                if (item.GetComponent<Plant>().species == PlanetSpecies.RockPlanet)
                {
                    GameObject plantItem = index.items[(int)ItemID.RockyPlanetPlant];

                    if (item.GetComponent<Plant>().type == PlanetType.VolcanicAsh)
                    {
                        string itemname = plantItem.GetComponent<TooltipInfo>().fireModName.Replace(" ", "");
                        AlmanacProgression.instance.Unlock(itemname);
                        iconDisplay.sprite = fireRockyPlanet;
                        AlmanacProgression.instance.Unlock("BurningRockyPlanet");
                    }
                    if (item.GetComponent<Plant>().type == PlanetType.FrozenCore)
                    {
                        string itemname = plantItem.GetComponent<TooltipInfo>().iceModName.Replace(" ", "");
                        AlmanacProgression.instance.Unlock(itemname);
                        iconDisplay.sprite = iceRockyPlanet;
                        AlmanacProgression.instance.Unlock("FrozenRockyPlanet");
                    }
                    if (item.GetComponent<Plant>().type == PlanetType.MortalCoil)
                    {
                        string itemname = plantItem.GetComponent<TooltipInfo>().ghostModName.Replace(" ", "");
                        AlmanacProgression.instance.Unlock(itemname);
                        iconDisplay.sprite = ghostRockyPlanet;
                        AlmanacProgression.instance.Unlock("GhostlyRockyPlanet");
                    }
                    if (item.GetComponent<Plant>().type == PlanetType.Fossilium)
                    {
                        string itemname = plantItem.GetComponent<TooltipInfo>().fossilModName.Replace(" ", "");
                        AlmanacProgression.instance.Unlock(itemname);
                        iconDisplay.sprite = fossilRockyPlanet;
                        AlmanacProgression.instance.Unlock("FossilRockyPlanet");
                    }
                    if (item.GetComponent<Plant>().type == PlanetType.DewDrop)
                    {
                        string itemname = plantItem.GetComponent<TooltipInfo>().waterModName.Replace(" ", "");
                        AlmanacProgression.instance.Unlock(itemname);
                        iconDisplay.sprite = waterRockyPlanet;
                        AlmanacProgression.instance.Unlock("WaterRockyPlanet");
                    }
                    if (item.GetComponent<Plant>().type == PlanetType.Sprout)
                    {
                        string itemname = plantItem.GetComponent<TooltipInfo>().grassModName.Replace(" ", "");
                        AlmanacProgression.instance.Unlock(itemname);
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

    private void Start()
    {
        index = FindObjectOfType<InventoryItemIndex>();
    }
}
