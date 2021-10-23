using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompostPlantScript : MonoBehaviour
{
    public GameObject plant;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        plant = gameObject.GetComponent<PlantSpot>().basicPlantObject;
    }

    public void CompostPlant()
    {
        Debug.Log("Composting Plant");
        Destroy(this.plant);
    }
}
