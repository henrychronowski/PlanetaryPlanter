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

    public void ShowTip()
    {
        //Set tooltip object to active
        //change tooltip object text to inventory item's text
        if(filled)
        {
            tooltipBox.gameObject.SetActive(true);

            TooltipInfo tip = item.itemObject.GetComponent<TooltipInfo>();
            if(tip != null)
                tooltipBox.SetNewText(tip.name + "\nType: " + tip.itemType + "\n" + tip.otherInfo);
        }
    }

    public void DisableTip()
    {
        if(filled)
            tooltipBox.gameObject.SetActive(false);
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

    }
}
