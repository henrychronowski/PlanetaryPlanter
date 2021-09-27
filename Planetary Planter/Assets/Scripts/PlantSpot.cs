using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantSpot : MonoBehaviour
{
    public Plant placedPlant;
    public GameObject basicPlantObject;

    public void PlacePlant()
    {
        if(transform.childCount == 0)
        {
            Instantiate(basicPlantObject, transform);
        }
    }

    void TakePlant()
    {
        if(transform.childCount == 1)
        {
            if(Inventory.instance.AddItem(transform.GetChild(0).gameObject))
            {
                Debug.Log("Added to inv");
            }
        }
    }

    public void Interact()
    {
        if (transform.childCount == 0)
        {
            Instantiate(basicPlantObject, transform);
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
