using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SpaceLocation
{
    PlayerInventory,
    SiloInventory
}

public class InventorySpace : MonoBehaviour
{
    public bool filled;
    public SpaceLocation location;
    //public List<InventoryItem> itemStack;
    public InventoryItem item;
    NewInventory newInv;
    private void OnMouseDown()
    {
        newInv.Click(this);
    }

    private void OnMouseOver()
    {
        //GetComponent<Image>().color = 
    }

    private void OnMouseExit()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        newInv = GameObject.FindObjectOfType<NewInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
