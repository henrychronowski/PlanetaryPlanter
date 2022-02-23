using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompostPlantScript : MonoBehaviour
{
    public NewInventory inventory;
    Transform player;

    // Start is called before the first frame update
    void Start()
    {
        inventory = FindObjectOfType<NewInventory>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CompostPlant()
    {
        
    }
}
