using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedInventoryScript : MonoBehaviour
{
    public int inventorySlots = 10;
    public int slotsTaken = 0;
    public GameObject[] seedInventory;

    // Start is called before the first frame update
    void Start()
    {
        seedInventory = new GameObject[inventorySlots];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddSeed(GameObject seed)
    {
        for(int i = 0; i < inventorySlots; i++)
        {
            UnityEngine.Debug.Log("3");
            if(inventorySlots > slotsTaken)
            {
                UnityEngine.Debug.Log("4");
                if (seedInventory[i] != null)
                {
                    UnityEngine.Debug.Log("5");
                    seed = seedInventory[i];
                    UnityEngine.Debug.Log("6");
                    slotsTaken++;
                }
            }
        }
    }

    void RemoveSeed(GameObject seed)
    {
        slotsTaken--;
    }
}
