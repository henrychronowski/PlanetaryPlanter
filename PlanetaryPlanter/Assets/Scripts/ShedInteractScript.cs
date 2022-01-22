using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShedInteractScript : MonoBehaviour
{
    public bool canInteract;
    public GameObject shedMenu;
    public GameObject craftingMenu;

    [SerializeField]
    GameObject inventoryPanelObject;

    [SerializeField]
    float maxDistanceBeforeClosing;

    Transform player;

    public void Interact()
    {
        if (inventoryPanelObject.activeInHierarchy)
        {
            shedMenu.SetActive(false);
            craftingMenu.SetActive(false);
            NewInventory.instance.DisableForceActive();

        }
        else if(!NewInventory.instance.forceActive)
        {
            shedMenu.SetActive(true);
            NewInventory.instance.ForceActive();
        }

    }

    void CheckDistanceFromPlayer()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        if (distance > maxDistanceBeforeClosing && (shedMenu.activeInHierarchy || craftingMenu.activeInHierarchy))
        {
            craftingMenu.SetActive(false);
            shedMenu.SetActive(false);
            NewInventory.instance.SetSpacesActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        CheckDistanceFromPlayer();
    }
}
