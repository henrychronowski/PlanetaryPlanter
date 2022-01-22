using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public List<GameObject> inventory;
    public List<Transform> inventorySpots;
    public int itemsInInventory = 0;
    public bool AddItem(GameObject item)
    {
        if(itemsInInventory < inventory.Capacity)
        {
            for(int i = 0; i < inventory.Capacity; i++)
            {
                if(inventory[i] == null)
                {
                    item.GetComponent<Plant>().inPot = false;
                    inventory[i] = item;
                    item.transform.parent = inventorySpots[i];
                    item.transform.localPosition = Vector3.zero;
                    itemsInInventory++;
                    return true;
                }
            }
        }
        return false;
    }

    public GameObject PopItem()
    {
        if(itemsInInventory > 0)
        {
            for (int i = 0; i < inventory.Capacity; i++)
            {
                if (inventory[i] != null)
                {
                    GameObject temp = inventory[i];
                    inventory[i].transform.parent = null;
                    inventory[i] = null;
                    itemsInInventory--;
                    return temp;
                }
            }
        }
        return null;
    }

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
}
