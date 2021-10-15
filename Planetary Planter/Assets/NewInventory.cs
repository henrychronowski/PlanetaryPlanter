using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewInventory : MonoBehaviour
{
    InventoryItem selectedItem;
    List<InventorySpace> spaces;
    public bool itemInCursor;
    void CheckInput()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(gameObject.activeInHierarchy)
                gameObject.SetActive(false);
            else
                gameObject.SetActive(false);
        }
    }

    public void Click(InventorySpace space)
    {
        if(!itemInCursor)
        {
            selectedItem = space.item;
            space.item = null;
            selectedItem.gameObject.transform.parent = transform;
        }
        else
        {
            if(space.item == null)
            {
                space.item = selectedItem;
                selectedItem = null;
                itemInCursor = false;
                space.item.transform.parent = space.transform;
                space.item.transform.position = Vector3.zero;
            }
        }
    }

    void UpdateHeldItemPos()
    {
        RectTransform itemTransform = selectedItem.gameObject.GetComponent<RectTransform>();
        selectedItem.gameObject.transform.position = new Vector3(Input.mousePosition.x + itemTransform.rect.width, Input.mousePosition.y + itemTransform.rect.height, Input.mousePosition.z);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHeldItemPos();
    }
}
