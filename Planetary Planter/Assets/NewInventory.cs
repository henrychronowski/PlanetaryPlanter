using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewInventory : MonoBehaviour
{
    public static NewInventory instance;
    public InventoryItem selectedItem;
    public List<InventorySpace> spaces;
    public bool itemInCursor;
    public GameObject emptyInventoryObject;
    public InventorySpace selectedSpace;
    public GameObject selectionIndicator;
    void CheckInput()
    {
        //if(Input.GetKeyDown(KeyCode.E))
        //{
        //    if(gameObject.activeInHierarchy)
        //        gameObject.SetActive(false);
        //    else
        //        gameObject.SetActive(false);
        //}
    }

    public void Click(InventorySpace space)
    {
        if(selectedSpace == space)
        {
            selectedSpace = null;
            
        }
        selectedSpace = space;
        selectionIndicator.transform.position = space.gameObject.transform.position;

        if(Input.GetKey(KeyCode.Mouse1))
        {
            if(!itemInCursor)
            {
                selectedItem = space.item;
                space.item = null;
                space.filled = false;
                selectedItem.gameObject.transform.parent = transform;
                itemInCursor = true;
            }
            else
            {
                if(space.item == null)
                {
                    space.item = selectedItem;
                    //selectedItem = null;
                    itemInCursor = false;
                    space.filled = true;
                    space.item.transform.parent = space.transform;
                    space.item.transform.localPosition = Vector3.zero;
                }
            }
        }
    }

    public bool AddItem(GameObject item)
    {
        for(int i = 0; i < spaces.Count; i++)
        {
            if(!spaces[i].item)
            {
                GameObject inventoryObject = Instantiate(emptyInventoryObject, spaces[i].gameObject.transform);

                inventoryObject.GetComponent<InventoryItem>().Init(item);

                spaces[i].item = inventoryObject.GetComponent<InventoryItem>();
                spaces[i].filled = true;
                spaces[i].item.gameObject.transform.localPosition = Vector3.zero;
                return true;
            }
        }
        return false;
    }

    public GameObject GetItem(InventorySpace slot)
    {
        if(slot.item)
        {
            return slot.item.itemObject;
        }
        return null;

    }

    public GameObject PopItem(InventorySpace slot)
    {
        if (slot.item)
        {
            GameObject temp = slot.item.itemObject;
            slot.filled = false;
            Destroy(slot.item.gameObject);
            
            return temp;
        }
        return null;
    }

    public GameObject PopItemOfTag(string tag)
    {
        if (selectedSpace.item && selectedSpace.item.itemObject.tag == tag)
        {
            GameObject temp = selectedSpace.item.itemObject;
            Destroy(selectedSpace.item.gameObject);

            return temp;
        }
        return null;
    }

    void UpdateHeldItemPos()
    {
        if(itemInCursor)
        {
            RectTransform itemTransform = selectedItem.gameObject.GetComponent<RectTransform>();
            selectedItem.gameObject.transform.position = new Vector3(Input.mousePosition.x + (itemTransform.rect.width/2), Input.mousePosition.y + (itemTransform.rect.height / 2 + 25), Input.mousePosition.z);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHeldItemPos();
    }
    private void Awake()
    {
        if (instance != null)
            Destroy(this.gameObject);
        else
            instance = this;
    }

}
