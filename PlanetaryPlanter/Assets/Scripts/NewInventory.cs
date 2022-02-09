using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Cinemachine;

//This class holds all code relating to inventory management. 
//Any questions regarding this code should be directed to Dan Hartman
public class NewInventory : MonoBehaviour
{
    public static NewInventory instance;

    private void Awake()
    {
        if (instance != null)
            Destroy(this.gameObject);
        else
            instance = this;
    }

    public InventoryItem selectedItem; // The inventory item your cursor is holding
    public List<InventorySpace> spaces;
    public bool itemInCursor; // Is your cursor holding an item?
    public GameObject emptyInventoryObject;
    public InventorySpace selectedSpace;
    public GameObject selectionIndicator;
    public GameObject grayOutLevelPanel;
    public int selectedIndex;
    public float scrollWheel;
    public bool inventoryActive; // True when the mouse has been unconfined and can click on things, only when UI is open
    public bool forceActive;
    public CloseAllMenus menuCloser;
    public AudioSource collectPop;

    // This offsets the item in the mouse cursor's position so it is not obstructed by the mouse and avoids any potential bugs 
    // related to holding items in cursors preventing the cursor from clicking buttons
    public int heldItemPositionOffset = 25;
    void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SetSpacesActiveToggle();
        }
        scrollWheel = Input.mouseScrollDelta.y * -5;
        
        int selectedIndex = spaces.IndexOf(selectedSpace);
        if (scrollWheel > 0)
        {
            if ((selectedIndex + 1) < spaces.Count)
                selectedSpace = spaces[selectedIndex + 1];
            else
                selectedSpace = spaces[0];
        }
        else if(scrollWheel < 0)
        {
            if ((selectedIndex - 1) >= 0)
                selectedSpace = spaces[selectedIndex - 1];
            else
                selectedSpace = spaces[spaces.Count - 1];
        }

        selectionIndicator.transform.position = selectedSpace.gameObject.transform.position;

    }

    //Effectively "opens" the inventory by locking the camera
    public void SetSpacesActive(bool active)
    {
        if (forceActive)
        {
            if(!inventoryActive)
            {
                foreach (InventorySpace space in spaces)
                {
                    space.GetComponent<Button>().interactable = active;

                }
            }
            inventoryActive = true;
            Cursor.lockState = CursorLockMode.None;
            //Need to remove cameraFollow references since that script is trashed
            //GameObject.FindObjectOfType<CinemachineFreeLook>().enabled = false;
            grayOutLevelPanel.SetActive(false);
            return;
        }
        else if(active)
        {
            grayOutLevelPanel.SetActive(true);
        }

        inventoryActive = active;
        foreach(InventorySpace space in spaces)
        {
            space.GetComponent<Button>().interactable = active;

        }
        if(active)
        {
            Cursor.lockState = CursorLockMode.None;
            GameObject.FindObjectOfType<CinemachineFreeLook>().enabled = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            GameObject.FindObjectOfType<CinemachineFreeLook>().enabled = true;
            grayOutLevelPanel.SetActive(false);
        }
    }

    public void ForceActive()
    {
        grayOutLevelPanel.SetActive(false);
        forceActive = true;
        SetSpacesActive(true);
    }
    public void DisableForceActive()
    {
        forceActive = false;
        SetSpacesActive(false);
    }
    public void SetSpacesActiveToggle()
    {
        inventoryActive = !inventoryActive;
        SetSpacesActive(inventoryActive);
        
    }

    // Runs whenever an inventory space is clicked on.
    // This includes clicking on spaces that aren't necessarily 
    // the player's inventory, such as silo spaces or crafting table slots.
    public void Click(InventorySpace space)
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
            else
            {
                InventoryItem temp = space.item;
                space.item = selectedItem;
                selectedItem = temp;

                space.item.transform.parent = space.transform;
                space.item.transform.localPosition = Vector3.zero;

            }
        }
    }

    public GameObject GetItemInCursor()
    {
        if (itemInCursor)
            return selectedItem.itemObject;
        else
            return null;
    }

    public GameObject PopItemInCursor()
    {
        if (selectedItem)
        {
            GameObject temp = selectedItem.itemObject;
            //selectedSpace.filled = false;
            itemInCursor = false;
            Destroy(selectedItem.gameObject);

            return temp;
        }
        return null;
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

                if(item.GetComponent<Plant>())
                {
                    string species = item.GetComponent<Plant>().species.ToString();

                    AlmanacProgression.instance.Unlock("Collect" + species + "Plant");
                }

                if(item.GetComponent<Seed>())
                {
                    string species = item.GetComponent<Seed>().species.ToString();
                    Debug.Log("Collect" + species + "Seed");
                    AlmanacProgression.instance.Unlock("Collect" + species + "Seed");
                }

                if(item.GetComponent<Modifier>())
                {
                    AlmanacProgression.instance.Unlock(item.GetComponent<Modifier>().modifierToApply.ToString());
                }
                collectPop.pitch = Random.Range(0.5f, 1f);
                collectPop.Play();

                if(SpacesAvailable() <= 0)
                {
                    TutorialManagerScript.instance.Unlock("Full Inventory");
                }
                return true;
            }
        }
        return false;
    }

    public int SpacesAvailable()
    {
        int spacesOpen = 0;
        for (int i = 0; i < spaces.Count; i++)
        {
            if (!spaces[i].item)
            {
                spacesOpen++;
            }
        }
        return spacesOpen;
    }

    public GameObject GetItem(InventorySpace slot)
    {
        if(slot.item)
        {
            return slot.item.itemObject;
        }
        return null;

    }

    public GameObject GetItem()
    {
        if (selectedSpace.item)
        {
            return selectedSpace.item.itemObject;
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

    public GameObject PopItem()
    {
        if (selectedSpace.item)
        {
            GameObject temp = selectedSpace.item.itemObject;
            selectedSpace.filled = false;
            Destroy(selectedSpace.item.gameObject);

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
            selectedSpace.filled = false;

            return temp;
        }
        return null;
    }

    //Moves the item held in the cursor to the 
    void UpdateHeldItemPos()
    {
        if(itemInCursor)
        {
            if(inventoryActive)
            {
                selectedItem.gameObject.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);

            }
            else
            {
                selectedItem.gameObject.transform.position = new Vector3(10000, 10000);
            }
            RectTransform itemTransform = selectedItem.gameObject.GetComponent<RectTransform>();
            selectedItem.gameObject.transform.position = new Vector3(Input.mousePosition.x + (itemTransform.rect.width),
                Input.mousePosition.y + (itemTransform.rect.height + heldItemPositionOffset), Input.mousePosition.z);

        }
    }

    void CheckForObservatoryState()
    {
        if(!GameObject.Find("PlayerVCam").GetComponent<Cinemachine.CinemachineFreeLook>().enabled && !inventoryActive)
        {
            SetSpacesActive(true);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //DontDestroyOnLoad(this);
        SetSpacesActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        UpdateHeldItemPos();
        if(!forceActive)
        {
            if (menuCloser.AreMenusOpen() && !forceActive)
            {
                ForceActive();
            }
        }
        else if(!menuCloser.AreMenusOpen())
        {
            DisableForceActive();
        }
        if (!inventoryActive)
        {
            CheckForObservatoryState();
            
        }
        
    }
}

