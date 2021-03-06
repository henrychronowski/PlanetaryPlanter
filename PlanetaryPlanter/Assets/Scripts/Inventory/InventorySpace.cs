using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SpaceLocation
{
    PlayerInventory,
    SiloInventory,
    Trash
}

public class InventorySpace : MonoBehaviour
{
    public bool filled;
    public SpaceLocation location;
    //public List<InventoryItem> itemStack;
    public InventoryItem item;
    public InventoryTooltip tooltipBox;
    Button button;
    public bool disallowCrops = false;
    public bool disallowMods = false;


    public void ShowTip()
    {
        //Set tooltip object to active
        //change tooltip object text to inventory item's text
        if(filled)
        {
            tooltipBox.gameObject.SetActive(true);
            tooltipBox.forcedOff = false;
            TooltipInfo tip = item.itemObject.GetComponent<TooltipInfo>();
            
            if(tip != null)
            {
                tooltipBox.SetNewText(tip.name + "\nType: " + tip.itemType + "\n" + tip.otherInfo);
                tooltipBox.SetPanelActive(true);
            }
        }
        NewInventory.instance.SetSelectedInventorySpace(this);
    }

    public void DisableTip()
    {
        if(filled)
        {
            tooltipBox.SetNewText("");
            tooltipBox.SetPanelActive(false);
        }
    }

    void CheckForItem()
    {
        if((transform.childCount == 0 && location != SpaceLocation.Trash) || (transform.childCount == 1 && location == SpaceLocation.Trash))
        {
            filled = false;
            item = null;
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        tooltipBox = GameObject.FindObjectOfType<InventoryTooltip>();
        button = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckForItem();
        if(!NewInventory.instance.inventoryActive)
        {
            DisableTip();
        }
        if(item)
            item.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(GetComponent<RectTransform>().rect.width, GetComponent<RectTransform>().rect.height);
    }
}
