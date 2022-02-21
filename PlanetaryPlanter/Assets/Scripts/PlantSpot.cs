using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantSpot : MonoBehaviour
{
    public PlantTool placedPlant;
    public GameObject basicPlantObject;
    public GameObject fertilizer;

    private CollectSeedScript collectSeed;
    public CompostPlantScript compost;

    public string placePlantInteract;
    public string harvestPlantInteract;
    public string waterPlantInteract;

    InteractableObject interactable;

    public GameObject fertilizerParticles;

    public void PlacePlant()
    {
        basicPlantObject = NewInventory.instance.PopItemOfTag("Seed");
        if (basicPlantObject) //if the object is not null this will run
        {
            Instantiate(basicPlantObject.GetComponent<Seed>().plantObject, transform);
            TutorialManagerScript.instance.Unlock("Maintaining Plants");
        }
    }

    public bool PlaceFertilizer()
    {

        GameObject temp = transform.GetChild(0).transform.gameObject;
        PlantTool p = temp.GetComponent<PlantTool>();
        if (p.potted == true)
        {
            GameObject fertilizerCheck;
            fertilizerCheck = NewInventory.instance.PopItemOfTag("Fertilizer");

            if(fertilizerCheck)
            {
                p.growthPerStage -= 3;
                Instantiate(fertilizerParticles, transform);
                return true;
            }
        }
        return false;
    }

    void TakePlant()
    {
        GameObject temp = transform.GetChild(0).transform.gameObject;
        PlantTool p = temp.GetComponent<PlantTool>();
        if (p.currentStage != PlantTool.Stage.Ripe && p.currentStage != PlantTool.Stage.Rotten)
        {
            if (PlaceFertilizer())
                return;
            else
            {
                p.AddWater(10);
                return;
            }
        }
        if (p.currentStage == PlantTool.Stage.Rotten)
        {
            //NewInventory.instance.AddItem(fertilizer);
            AlmanacProgression.instance.Unlock("GetFertilizer");
            TutorialManagerScript.instance.Unlock("Fertilizer");
            GameObject temp2 = Instantiate(fertilizer);
            NewInventory.instance.AddItem(temp2);

            p.potted = false;
            temp.transform.parent = null;
            temp.transform.position = new Vector3(10000, 100000);

            //compost = gameObject.GetComponent<CompostPlantScript>();
            //compost.CompostPlant();
            return;
        }
        if (NewInventory.instance.AddItem(transform.GetChild(0).gameObject))
        {
            p.potted = false;
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
        if (GetComponentInChildren<PlantTool>().currentStage == PlantTool.Stage.Ripe || (GetComponentInChildren<PlantTool>().currentStage == PlantTool.Stage.Rotten))
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
    }
    // Update is called once per frame
    void Update()
    {
        interactable.interactText = UpdateInteractTip();
    }
}