using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    public bool initted;
    public GameObject itemObject;

    private Image iconDisplay;
    public InventoryItem(GameObject obj)
    {
        itemObject = obj;
    }
    // Start is called before the first frame update
    void Start()
    {
        iconDisplay = GetComponent<Image>();
        if(itemObject)
        {
            //check to see if it has an image component to use as its icon in the inventory screen
            iconDisplay.sprite = GetComponent<IconHolder>().icon;
            
        }
    }

    public void Init(GameObject item)
    {
        itemObject = item;
        iconDisplay = GetComponent<Image>();
        if (itemObject)
        {
            //check to see if it has an image component to use as its icon in the inventory screen
            iconDisplay.sprite = itemObject.GetComponent<IconHolder>().icon;

        }
        initted = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
