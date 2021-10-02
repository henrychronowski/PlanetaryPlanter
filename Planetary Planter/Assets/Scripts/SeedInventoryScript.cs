using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedInventoryScript : MonoBehaviour
{
    public static SeedInventoryScript instance;

    public List<Transform> inventorySpots;

    public int slotsTaken = 0;
    public GameObject[] seedInventory;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance);
        else
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool AddSeed(GameObject seed)
    {
        for(int i = 0; i < seedInventory.Length; i++)
        {
            if(seedInventory.Length > slotsTaken)
            {
                if (seedInventory[i] == null)
                {
                    seedInventory[i] = seed;
                    slotsTaken++;
                    seed.transform.parent = inventorySpots[i];
                    seed.transform.localPosition = Vector3.zero;
                    return true;
                }
            }
        }
        return false;
    }

    public bool UseSeed()
    {
        if (slotsTaken > 0)
        {
            for (int i = seedInventory.Length-1; i >= 0; i--)
            {
                if (seedInventory[i] != null)
                {
                    GameObject temp = seedInventory[i];
                    seedInventory[i].transform.parent = null;
                    seedInventory[i] = null;
                    slotsTaken--;
                    Destroy(temp);
                    return true;
                }
            }
        }
        return false;
    }

    void RemoveSeed(GameObject seed)
    {
        slotsTaken--;
    }
}
