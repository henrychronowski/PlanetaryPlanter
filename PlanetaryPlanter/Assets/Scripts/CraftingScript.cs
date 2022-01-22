using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingScript : MonoBehaviour
{
    public GameObject slot1;
    public GameObject slot2;
    public GameObject craftButton;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        ActivateCraft();

    }

    void ActivateCraft()
    {
        if ((slot1.GetComponent<CraftingSlotCheckScript>().isModifier == true && slot2.GetComponent<CraftingSlotCheckScript>().isObject == true)
            || (slot1.GetComponent<CraftingSlotCheckScript>().isObject == true && slot2.GetComponent<CraftingSlotCheckScript>().isModifier == true))
        {
            craftButton.GetComponent<Button>().interactable = true;
        }
        else
        {
            craftButton.GetComponent<Button>().interactable = false;
        }
    }

    public void CraftItems()
    {
        if (slot1.GetComponent<CraftingSlotCheckScript>().isObject == true && slot2.GetComponent<CraftingSlotCheckScript>().isModifier == true)
        {
            slot1.GetComponent<CraftingSlotCheckScript>().item.itemObject.GetComponent<Plant>().type = 
                slot2.GetComponent<CraftingSlotCheckScript>().item.itemObject.GetComponent<Modifier>().modifierToApply;
            Destroy(slot2.GetComponent<CraftingSlotCheckScript>().item.gameObject);
            slot1.GetComponent<CraftingSlotCheckScript>().item.Init(slot1.GetComponent<CraftingSlotCheckScript>().item.itemObject);
            return;
        }

        else if (slot2.GetComponent<CraftingSlotCheckScript>().isObject == true && slot1.GetComponent<CraftingSlotCheckScript>().isModifier == true)
        {
            slot2.GetComponent<CraftingSlotCheckScript>().item.itemObject.GetComponent<Plant>().type =
                slot1.GetComponent<CraftingSlotCheckScript>().item.itemObject.GetComponent<Modifier>().modifierToApply;
            Destroy(slot1.GetComponent<CraftingSlotCheckScript>().item.gameObject);
            slot2.GetComponent<CraftingSlotCheckScript>().item.Init(slot2.GetComponent<CraftingSlotCheckScript>().item.itemObject);
            return;
        }
    }
}
