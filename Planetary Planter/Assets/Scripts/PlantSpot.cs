using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantSpot : MonoBehaviour
{
    public Plant placedPlant;
    public GameObject basicPlantObject;

    private CollectSeedScript collectSeed;
    public CompostPlantScript compost;

    public void PlacePlant()
    {
        basicPlantObject = NewInventory.instance.PopItemOfTag("Seed");
        if (basicPlantObject) //if the object is not null this will run
        {
            Instantiate(basicPlantObject.GetComponent<Seed>().plantObject, transform);
        }
    }
    void TakePlant()
    {
        GameObject temp = transform.GetChild(0).transform.gameObject;
        Plant p = temp.GetComponent<Plant>();
        if (p.stage != Plant.Stage.Ripe && p.stage != Plant.Stage.Rotten)
        {
            p.AddWater(10);
            return;
        }
        if (p.stage == Plant.Stage.Rotten)
        {
            p.inPot = false;
            temp.transform.parent = null;
            temp.transform.position = new Vector3(10000, 100000);

            compost = gameObject.GetComponent<CompostPlantScript>();
            compost.CompostPlant();
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
        }
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