using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantSpot : MonoBehaviour
{
    public Plant placedPlant;
    public GameObject basicPlantObject;
    public GameObject fertilizer;

    private CollectSeedScript collectSeed;
    public CompostPlantScript compost;

    public string placePlantInteract;
    public string harvestPlantInteract;
    public string waterPlantInteract;

    InteractableObject interactable;

    public GameObject fertilizerParticles;

    // Audio Manager Script is set up here
    private SoundManager soundManager;
    public void PlacePlant()
    {
        basicPlantObject = NewInventory.instance.PopItemOfTag("Seed");
        if (basicPlantObject) //if the object is not null this will run
        {
            Instantiate(basicPlantObject.GetComponent<Seed>().plantObject, transform);
            TutorialManagerScript.instance.Unlock("Maintaining Plants");
            soundManager.PlaySound("PlantPlant");
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
                p.growthNeededForEachStage -= 3;
                Instantiate(fertilizerParticles, transform);
                return true;
            }
        }
        return false;
    }

    void TakePlant()
    {
        GameObject temp = transform.GetChild(0).transform.gameObject;
        Plant p = temp.GetComponent<Plant>();
        soundManager.PlaySound("Harvest");
        if (p.stage != Plant.Stage.Ripe && p.stage != Plant.Stage.Rotten)
        {
            if (PlaceFertilizer())
                return;
            else
            {
                p.AddWater(10);
                return;
            }
        }
        if (p.stage == Plant.Stage.Rotten)
        {
            //NewInventory.instance.AddItem(fertilizer);
            AlmanacProgression.instance.Unlock("GetFertilizer");
            TutorialManagerScript.instance.Unlock("Fertilizer");
            GameObject temp2 = Instantiate(fertilizer);
            NewInventory.instance.AddItem(temp2);

            p.inPot = false;
            temp.transform.parent = null;
            temp.transform.position = new Vector3(10000, 100000);

            //compost = gameObject.GetComponent<CompostPlantScript>();
            //compost.CompostPlant();
            return;
        }
        if (NewInventory.instance.AddItem(transform.GetChild(0).gameObject))
        {
            p.inPot = false;
            //Destroy(transform.GetChild(0).gameObject);
            //transform.GetChild(0).transform.gameObject.SetActive(false);
            temp.transform.parent = null;
            temp.transform.position = new Vector3(10000, 100000); //this is dumb but its 4:30am
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
    // Start is called before the first frame update
    void Start()
    {
        //NewInventory.instance.AddItem(fertilizer);
        interactable = GetComponent<InteractableObject>();
        fertilizer = gameObject.GetComponent<FertilizerScript>().Fertilizer;
        //Audio Manager Is Opend Up here
        soundManager = SoundManager.instance;

    }
    // Update is called once per frame
    void Update()
    {
        interactable.interactText = UpdateInteractTip();
    }
}