using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantSpot : MonoBehaviour
{
    public Plant placedPlant;
    public GameObject plantObject;
    public GameObject fertilizer;

    private CollectSeedScript collectSeed;
    public CompostPlantScript compost;

    public string placePlantInteract;
    public string harvestPlantInteract;
    public string waterPlantInteract;

    public Sprite placePlantSprite;
    public Sprite harvestPlantSprite;
    public Sprite waterPlantSprite;

    InteractableObject interactable;

    public GameObject fertilizerParticles;
    public InventoryItemIndex items;

    // Audio Manager Script is set up here
    private SoundManager soundManager;
    public void PlacePlant()
    {
        plantObject = NewInventory.instance.PopItemOfTag("Seed");
        if (plantObject) //if the object is not null this will run
        {
            plantObject = Instantiate(plantObject.GetComponent<Seed>().plantObject, transform);
            placedPlant = plantObject.GetComponent<Plant>();
            TutorialManagerScript.instance.Unlock("Maintaining Plants");
            soundManager.PlaySound("PlantingPlant");
        }
    }

    public void PlacePlantOfType(PlanetSpecies species, float growth)
    {
        switch(species)
        {
            case PlanetSpecies.Asteroid:
                {
                    plantObject = Instantiate(items.items[(int)ItemID.AsteroidPlant], transform);
                    placedPlant = plantObject.GetComponent<Plant>();
                    placedPlant.AddElapsedHours((int)growth);

                    break;
                }
            case PlanetSpecies.GasPlanet:
                {
                    plantObject = Instantiate(items.items[(int)ItemID.PlanetPlant], transform);
                    placedPlant = plantObject.GetComponent<Plant>();
                    placedPlant.AddElapsedHours((int)growth);

                    break;
                }
            case PlanetSpecies.Star:
                {
                    plantObject = Instantiate(items.items[(int)ItemID.StarPlant], transform);
                    placedPlant = plantObject.GetComponent<Plant>();
                    placedPlant.AddElapsedHours((int)growth);

                    break;
                }
            case PlanetSpecies.Comet:
                {
                    plantObject = Instantiate(items.items[(int)ItemID.CometPlant], transform);
                    placedPlant = plantObject.GetComponent<Plant>();
                    placedPlant.AddElapsedHours((int)growth);
                    break;
                }
            case PlanetSpecies.RockyPlanet:
                {
                    plantObject = Instantiate(items.items[(int)ItemID.RockyPlanetPlant], transform);
                    placedPlant = plantObject.GetComponent<Plant>();
                    placedPlant.AddElapsedHours((int)growth);

                    break;
                }

        }
        
    }

    public bool PlaceFertilizer()
    {

        GameObject temp = transform.GetChild(0).transform.gameObject;
        Plant p = temp.GetComponent<Plant>();
        if (p.inPot == true)
        {
            GameObject fertilizerCheck;
            fertilizerCheck = NewInventory.instance.PopItemOfTag("Fertilizer");
            if(fertilizerCheck)
            {
                p.stage = Plant.Stage.Ripe;
                p.plantModels[0].SetActive(false);
                p.plantModels[1].SetActive(false);
                p.plantModels[2].SetActive(true);
                p.plantModels[3].SetActive(false);



                Instantiate(fertilizerParticles, transform);
                return true;
            }
        }
        return false;
    }

    public bool GetPlantStatus()
    {
        if(placedPlant == null)
            return false;

        return placedPlant.stage == Plant.Stage.Ripe || placedPlant.stage == Plant.Stage.Rotten;
    }

    void TakePlant()
    {
        GameObject temp = transform.GetChild(0).transform.gameObject;
        Plant p = temp.GetComponent<Plant>();
        if (p.stage != Plant.Stage.Ripe && p.stage != Plant.Stage.Rotten)
        {
            if (PlaceFertilizer())
                return;
            else
            {
                return;
            }
        }
        if (p.stage == Plant.Stage.Rotten)
        {
            soundManager.PlaySound("Harvest");
            AlmanacProgression.instance.Unlock("GetFertilizer");
            TutorialManagerScript.instance.Unlock("Fertilizer");
            GameObject temp2 = Instantiate(p.rottenPlant);
            if(NewInventory.instance.AddItem(temp2))
            {
                p.inPot = false;
                temp.transform.parent = null;
                temp.transform.position = new Vector3(0, 1000);
                plantObject = null;
                placedPlant = null;
            }
            return;
        }
        if (NewInventory.instance.AddItem(transform.GetChild(0).gameObject)) //Returns false when inventory is full
        {
            soundManager.PlaySound("Harvest");
            p.inPot = false;
            temp.transform.parent = null;
            temp.transform.position = new Vector3(0, -1000); //this is dumb but its 4:30am
            plantObject = null;
            placedPlant = null;
            Debug.Log("Added to inv");
        }
    }
    public void Interact()
    {
        if (transform.childCount == 0)
        {
            PlacePlant();
        }
        else
        {
            TakePlant();
            PlaceFertilizer();
        }
    }

    string UpdateInteractTip()
    {
        if (transform.childCount == 0)
        {
            return placePlantInteract;
        }
        if(GetComponentInChildren<Plant>().stage == Plant.Stage.Ripe || (GetComponentInChildren<Plant>().stage == Plant.Stage.Rotten))
        {
            return harvestPlantInteract;
        }
        else
        {
            return waterPlantInteract;
        }
    }

    Sprite updateInteractSprite()
    {
        if (transform.childCount == 0)
        {
            return placePlantSprite;
        }
        if (GetComponentInChildren<Plant>().stage == Plant.Stage.Ripe || (GetComponentInChildren<Plant>().stage == Plant.Stage.Rotten))
        {
            return harvestPlantSprite;
        }
        else
        {
            return waterPlantSprite;
        }

    }
        // Start is called before the first frame update
     void Start()
    {
        //NewInventory.instance.AddItem(fertilizer);
        interactable = GetComponent<InteractableObject>();
        fertilizer = gameObject.GetComponent<FertilizerScript>().Fertilizer;

        //Audio Manager Is Opend Up here
        soundManager = SoundManager.instance;


        items = GameObject.FindObjectOfType<InventoryItemIndex>();
        gameObject.name = transform.parent.name + " Bed"; //every plantspot gameobject name needs to be different for saving to work and I REALLY dont want to go rename each individual one

    }
    // Update is called once per frame
    void Update()
    {
        interactable.interactText = UpdateInteractTip();
        interactable.interactSprite = updateInteractSprite();
    }
}