using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingSlotCheckScript : MonoBehaviour
{
    public InventoryItem item;
    public bool isObject;
    public bool isModifier;

    // Start is called before the first frame update
    void Start()
    {
        isObject = false;
        isModifier = false;
    }

    // Update is called once per frame
    void Update()
    {
        item = gameObject.GetComponent<InventorySpace>().item;
        CheckTag();
    }

    void CheckTag()
    {
        if(item)
        {
            if (item.itemObject.tag == "Plant")
            {
                if (item.itemObject.GetComponent<Plant>().stage == Plant.Stage.Rotten)
                {
                    isObject = false;
                    isModifier = false;
                    return;
                }
                isObject = true;
                isModifier = false;
                return;
            }
            else if (item.itemObject.tag == "Modifier")
            {
                isModifier = true;
                isObject = false;
                return;
            }
            else
            {
                isObject = false;
                isModifier = false;
                return;
            }
        }
        else
        {
            isObject = false;
            isModifier = false;
            return;
        }
    }
}
