using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantSpot : MonoBehaviour
{
    public Plant placedPlant;
    public GameObject basicPlantObject;
    public void PlacePlant()
    {
        if (NewInventory.instance.PopItemOfTag("Seed"))
            Instantiate(basicPlantObject, transform);
    }

    void TakePlant()
    {
            GameObject temp = transform.GetChild(0).transform.gameObject;
        Plant p = temp.GetComponent<Plant>();
        if(p.stage != Plant.Stage.Final)
        {
            p.AddWater(10);
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
