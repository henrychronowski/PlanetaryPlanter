using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Cinemachine;
using System.Linq;
using TMPro;


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
    public InventorySpace trash;
    public bool itemInCursor; // Is your cursor holding an item?
    public GameObject emptyInventoryObject;
    public InventorySpace selectedSpace;
    public GameObject selectionIndicator;
    public GameObject grayOutLevelPanel;
    public GameObject craftingMenu;
    public int selectedIndex;
    public float scrollWheel;
    public bool inventoryActive; // True when the mouse has been unconfined and can click on things, only when UI is open
    public bool forceActive;
    public CloseAllMenus menuCloser;
    public AudioSource collectPop;
    public InventoryItemIndex index;
    public TextMeshProUGUI itemNameTooltip;
    public Color originalColor;
    public Color fadeOutColor;
    [SerializeField] float timeSinceNewSelection;
    [SerializeField] float timeUntilFadeoutStart;
    [SerializeField] float timeUntilCompletelyFadedOut;

    // This offsets the item in the mouse cursor's position so it is not obstructed by the mouse and avoids any potential bugs 
    // related to holding items in cursors preventing the cursor from clicking buttons
    public int heldItemPositionOffset = 25;
    void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SetSpacesActiveToggle();
        }
        if(Input.GetKeyDown(KeyCode.C))
        {
            if(!inventoryActive)
                SetSpacesActiveToggle();

            if(craftingMenu.activeInHierarchy)
            {
                CloseCraftingMenu();
            }
            else
            {
                OpenCraftingMenu();
            }
        }
        scrollWheel = Input.mouseScrollDelta.y * -5;
        
        int selectedIndex = spaces.IndexOf(selectedSpace);
        if (scrollWheel > 0)
        {
            if ((selectedIndex + 1) < spaces.Count)
                SetSelectedInventorySpace(spaces[selectedIndex + 1]);
            else
                SetSelectedInventorySpace(spaces[0]);
        }
        else if(scrollWheel < 0)
        {
            if ((selectedIndex - 1) >= 0)
                SetSelectedInventorySpace(spaces[selectedIndex - 1]);
            else
                SetSelectedInventorySpace(spaces[spaces.Count - 1]);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetSelectedInventorySpace(spaces[0]);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetSelectedInventorySpace(spaces[1]);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetSelectedInventorySpace(spaces[2]);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SetSelectedInventorySpace(spaces[3]);
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SetSelectedInventorySpace(spaces[4]);
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            SetSelectedInventorySpace(spaces[5]);
        }

        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            SetSelectedInventorySpace(spaces[6]);
        }

        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            SetSelectedInventorySpace(spaces[7]);
        }

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            SetSelectedInventorySpace(spaces[8]);
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            SetSelectedInventorySpace(spaces[9]);
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
            GameObject.FindObjectOfType<CameraController>().CameraState(false);
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
            GameObject.FindObjectOfType<CameraController>().CameraState(false);
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            GameObject.FindObjectOfType<CameraController>().CameraState(true);
            if(itemInCursor)
                if(PlaceItemInOpenSpace(selectedItem))
                {
                    selectedItem = null;
                    itemInCursor = false;
                }

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

        if(!itemInCursor && space.item != null)
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
                selectedItem = null;
                itemInCursor = false;
                space.filled = true;
                space.item.transform.parent = space.transform;
                space.item.transform.localPosition = Vector3.zero;
            }
            else
            {
                if(space.location == SpaceLocation.Trash)
                {
                    Destroy(space.item.gameObject);
                    space.item = selectedItem;
                    //selectedItem = null;
                    itemInCursor = false;
                    space.filled = true;
                    space.item.transform.parent = space.transform;
                    space.item.transform.localPosition = Vector3.zero;
                }

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

    public int GetNumberOfItems()
    {
        int count = 0;
        foreach(InventorySpace space in spaces)
        {
            if(space.filled)
                count++;
        }

        return count;
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

    public void SetSelectedInventorySpace(InventorySpace space)
    {
        if (space.location != SpaceLocation.PlayerInventory)
            return;

        selectedSpace = space;
        timeSinceNewSelection = 0;
        if(space.filled)
        {
            itemNameTooltip.text = space.item.itemObject.GetComponent<TooltipInfo>().name;
        }
        else
        {
            itemNameTooltip.text = "";
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

                if(item.GetComponent<Plant>())
                {
                    TutorialManagerScript.instance.Unlock("Harvesting Plants");

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

    public bool AddItemToSpace(GameObject item, InventorySpace space)
    {
        if (!space.item)
        {
            GameObject inventoryObject = Instantiate(emptyInventoryObject, space.gameObject.transform);

            inventoryObject.GetComponent<InventoryItem>().Init(item);

            space.item = inventoryObject.GetComponent<InventoryItem>();
            space.filled = true;
            space.item.gameObject.transform.localPosition = Vector3.zero;

            if (item.GetComponent<Plant>())
            {
                string species = item.GetComponent<Plant>().species.ToString();

                AlmanacProgression.instance.Unlock("Collect" + species + "Plant");
            }

            if (item.GetComponent<Seed>())
            {
                string species = item.GetComponent<Seed>().species.ToString();
                Debug.Log("Collect" + species + "Seed");
                AlmanacProgression.instance.Unlock("Collect" + species + "Seed");
            }

            if (item.GetComponent<Modifier>())
            {
                AlmanacProgression.instance.Unlock(item.GetComponent<Modifier>().modifierToApply.ToString());
            }
            collectPop.pitch = Random.Range(0.5f, 1f);
            collectPop.Play();

            if (SpacesAvailable() <= 0)
            {
                TutorialManagerScript.instance.Unlock("Full Inventory");
            }
            return true;
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

    public bool PlaceItemInOpenSpace(InventoryItem item)
    {
        for (int i = 0; i < spaces.Count; i++)
        {
            if (!spaces[i].filled)
            {
                spaces[i].item = item;
                spaces[i].filled = true;
                spaces[i].item.transform.parent = spaces[i].transform;
                spaces[i].item.transform.localPosition = Vector3.zero;
                return true;
            }
        }

        //Put it in trash as a last resort

        Destroy(trash.item.gameObject);
        trash.item = selectedItem;
        trash.filled = true;
        trash.item.transform.parent = trash.transform;
        trash.item.transform.localPosition = Vector3.zero;
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

    public GameObject GetItemInSelectedSpace()
    {
        if (selectedSpace.item)
        {
            return selectedSpace.item.itemObject;
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
        if(!GameObject.Find("PlayerVCam").GetComponent<CameraController>().canUseCamera && !inventoryActive)
        {
            SetSpacesActive(true);
        }
    }

    void OpenCraftingMenu()
    {
        craftingMenu.SetActive(true);
        TutorialManagerScript.instance.Unlock("Crafting Table");
    }

    void CloseCraftingMenu()
    {
        craftingMenu.SetActive(false);
    }

    public ItemID[] ReturnAllInventoryIDs()
    {
        List<ItemID> items = new List<ItemID>();

        InventorySpace[] allSpaces = GameObject.FindObjectsOfType<InventorySpace>(true).OrderBy(gameObject => gameObject.name).ToArray();

        //allSpaces2.Sort();

        for (int i = 0; i < allSpaces.Length; i++)
        {
            if (allSpaces[i].filled)
                items.Add(DetermineItemID(allSpaces[i].item.itemObject));
            else
                items.Add(ItemID.Unidentified);
        }
        return items.ToArray();
    }

    public List<InventoryItemSave> ReturnAllInventoryIDsNew()
    {
        List<InventoryItemSave> savedItems = new List<InventoryItemSave>();
        List<ItemID> items = new List<ItemID>();

        List<InventorySpace> allSpaces = new List<InventorySpace>();
        allSpaces.AddRange(GameObject.FindObjectsOfType<InventorySpace>(true));
        allSpaces.Sort();

        for (int i = 0; i < allSpaces.Count; i++)
        {
            if (allSpaces[i].filled)
                items.Add(DetermineItemID(allSpaces[i].item.itemObject));
            else
                items.Add(ItemID.Unidentified);
        }
        return savedItems;
    }

    public void LoadAllItems(InventoryItemIndex index, ItemID[] items)
    {
        InventorySpace[] allSpaces = GameObject.FindObjectsOfType<InventorySpace>(true).OrderBy(gameObject => gameObject.name).ToArray();

        if(allSpaces.Length != items.Length)
        {
            Debug.LogError("Item id length != space count");
        }
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] != ItemID.Unidentified)
                AddItemToSpace(Instantiate(index.items[(int)items[i]]), allSpaces[i]);
        }
    }

    public ItemID DetermineItemID(GameObject item)
    {
        //
        if (item.tag == "Fertilizer")
            return ItemID.Fertilizer;

        if (item.tag == "Plant")
        {
            if (item.GetComponent<Plant>().species == PlanetSpecies.GasPlanet)
            {
                if(item.GetComponent<Plant>().stage == Plant.Stage.Rotten)
                {
                    return ItemID.RottenPlanet;
                }
                if (item.GetComponent<Plant>().type == PlanetType.VolcanicAsh)
                {
                    return ItemID.FirePlanet;
                }
                else if (item.GetComponent<Plant>().type == PlanetType.FrozenCore)
                {
                    return ItemID.IcePlanet;
                }
                else if (item.GetComponent<Plant>().type == PlanetType.MortalCoil)
                {
                    return ItemID.GhostPlanetPlant;
                }
                else if (item.GetComponent<Plant>().type == PlanetType.Fossilium)
                {
                    return ItemID.FossilPlanetPlant;
                }
                else if (item.GetComponent<Plant>().type == PlanetType.DewDrop)
                {
                    return ItemID.WaterPlanetPlant;
                }
                else if (item.GetComponent<Plant>().type == PlanetType.Sprout)
                {
                    return ItemID.GrassPlanetPlant;
                }
                else
                {
                    return ItemID.PlanetPlant;
                }
            }

            if (item.GetComponent<Plant>().species == PlanetSpecies.Star)
            {
                if (item.GetComponent<Plant>().stage == Plant.Stage.Rotten)
                {
                    return ItemID.RottenStar;
                }
                if (item.GetComponent<Plant>().type == PlanetType.VolcanicAsh)
                {
                    return ItemID.FireStar;
                }
                else if (item.GetComponent<Plant>().type == PlanetType.FrozenCore)
                {
                    return ItemID.IceStar;
                }
                else if (item.GetComponent<Plant>().type == PlanetType.MortalCoil)
                {
                    return ItemID.GhostStarPlant;
                }
                else if (item.GetComponent<Plant>().type == PlanetType.Fossilium)
                {
                    return ItemID.FossilStarPlant;
                }
                else if (item.GetComponent<Plant>().type == PlanetType.DewDrop)
                {
                    return ItemID.WaterStarPlant;
                }
                else if (item.GetComponent<Plant>().type == PlanetType.Sprout)
                {
                    return ItemID.GrassStarPlant;
                }
                else
                {
                    return ItemID.StarPlant;
                }
            }

            if (item.GetComponent<Plant>().species == PlanetSpecies.Asteroid)
            {
                if (item.GetComponent<Plant>().stage == Plant.Stage.Rotten)
                {
                    return ItemID.RottenAsteroid;
                }
                if (item.GetComponent<Plant>().type == PlanetType.VolcanicAsh)
                {
                    return ItemID.FireAsteroid;
                }
                else if (item.GetComponent<Plant>().type == PlanetType.FrozenCore)
                {
                    return ItemID.IceAsteroid;
                }
                else if (item.GetComponent<Plant>().type == PlanetType.MortalCoil)
                {
                    return ItemID.GhostAsteroidPlant;
                }
                else if (item.GetComponent<Plant>().type == PlanetType.Fossilium)
                {
                    return ItemID.FossilAsteroidPlant;
                }
                else if (item.GetComponent<Plant>().type == PlanetType.DewDrop)
                {
                    return ItemID.WaterAsteroidPlant;
                }
                else if (item.GetComponent<Plant>().type == PlanetType.Sprout)
                {
                    return ItemID.GrassAsteroidPlant;
                }
                else
                {
                    return ItemID.AsteroidPlant;
                }
            }

            if (item.GetComponent<Plant>().species == PlanetSpecies.RockPlanet)
            {
                if (item.GetComponent<Plant>().stage == Plant.Stage.Rotten)
                {
                    return ItemID.RottenRockyPlanet;
                }
                if (item.GetComponent<Plant>().type == PlanetType.VolcanicAsh)
                {
                    return ItemID.FireRockyPlanet;
                }
                else if (item.GetComponent<Plant>().type == PlanetType.FrozenCore)
                {
                    return ItemID.IceRockyPlanet;
                }
                else if (item.GetComponent<Plant>().type == PlanetType.MortalCoil)
                {
                    return ItemID.GhostRockyPlanet;
                }
                else if (item.GetComponent<Plant>().type == PlanetType.Fossilium)
                {
                    return ItemID.FossilRockyPlanet;
                }
                else if (item.GetComponent<Plant>().type == PlanetType.DewDrop)
                {
                    return ItemID.WaterRockyPlanet;
                }
                else if (item.GetComponent<Plant>().type == PlanetType.Sprout)
                {
                    return ItemID.GrassRockyPlanet;
                }
                else
                {
                    return ItemID.RockyPlanetPlant;
                }
            }

            if (item.GetComponent<Plant>().species == PlanetSpecies.Comet)
            {
                if (item.GetComponent<Plant>().stage == Plant.Stage.Rotten)
                {
                    return ItemID.RottenComet;
                }
                if (item.GetComponent<Plant>().type == PlanetType.VolcanicAsh)
                {
                    return ItemID.FireCometPlanet;
                }
                else if (item.GetComponent<Plant>().type == PlanetType.FrozenCore)
                {
                    return ItemID.IceCometPlanet;
                }
                else if (item.GetComponent<Plant>().type == PlanetType.MortalCoil)
                {
                    return ItemID.GhostCometPlanet;
                }
                else if (item.GetComponent<Plant>().type == PlanetType.Fossilium)
                {
                    return ItemID.FossilCometPlanet;
                }
                else if (item.GetComponent<Plant>().type == PlanetType.DewDrop)
                {
                    return ItemID.WaterCometPlanet;
                }
                else if (item.GetComponent<Plant>().type == PlanetType.Sprout)
                {
                    return ItemID.GrassCometPlanet;
                }
                else
                {
                    return ItemID.CometPlant;
                }
            }
        }

        if (item.tag == "Seed")
        {
            if (item.GetComponent<Seed>().species == PlanetSpecies.GasPlanet)
            {
                return ItemID.PlanetSeed;
            }
            else if (item.GetComponent<Seed>().species == PlanetSpecies.Star)
            {
                return ItemID.StarSeed;
            }
            else if (item.GetComponent<Seed>().species == PlanetSpecies.Asteroid)
            {
                return ItemID.AsteroidSeed;
            }
            else if (item.GetComponent<Seed>().species == PlanetSpecies.RockPlanet)
            {
                return ItemID.RockyPlanetSeed;
            }
            else if (item.GetComponent<Seed>().species == PlanetSpecies.Comet)
            {
                return ItemID.CometSeed;
            }
        }

        if (item.tag == "Modifier")
        {
            if (item.GetComponent<Modifier>().modifierToApply == PlanetType.VolcanicAsh)
            {
                return ItemID.FireModifier;
            }
            else if (item.GetComponent<Modifier>().modifierToApply == PlanetType.FrozenCore)
            {
                return ItemID.IceModifier;
            }
            else if (item.GetComponent<Modifier>().modifierToApply == PlanetType.MortalCoil)
            {
                return ItemID.GhostModifier;
            }
            else if (item.GetComponent<Modifier>().modifierToApply == PlanetType.Fossilium)
            {
                return ItemID.FossilModifier;
            }
            else if (item.GetComponent<Modifier>().modifierToApply == PlanetType.DewDrop)
            {
                return ItemID.WaterModifier;
            }
            else if (item.GetComponent<Modifier>().modifierToApply == PlanetType.Sprout)
            {
                return ItemID.GrassModifier;
            }
        }

        return ItemID.Unidentified;
    }

    void UpdateTooltipTextOpacity()
    {
        timeSinceNewSelection += Time.deltaTime;
        if (timeSinceNewSelection >= timeUntilCompletelyFadedOut)
        {
            itemNameTooltip.color = new Color(itemNameTooltip.color.r, itemNameTooltip.color.g, itemNameTooltip.color.b, 0);
        }
        else if(timeSinceNewSelection > timeUntilFadeoutStart)
        {
            itemNameTooltip.color = new Color(itemNameTooltip.color.r, itemNameTooltip.color.g, itemNameTooltip.color.b, 
                1 - (timeSinceNewSelection - timeUntilFadeoutStart) / (timeUntilCompletelyFadedOut - timeUntilFadeoutStart));
        }
        else
        {
            itemNameTooltip.color = new Color(itemNameTooltip.color.r, itemNameTooltip.color.g, itemNameTooltip.color.b, 1);
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
        UpdateTooltipTextOpacity();
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

