using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompostPlantScript : MonoBehaviour
{
    public Plant plant;

    // Start is called before the first frame update
    void Start()
    {
        //plant = gameObject.GetComponent<Plant>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CompostPlant()
    {
        Destroy(plant);
    }
}
