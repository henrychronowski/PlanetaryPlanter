using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SiloInventory : MonoBehaviour
{
    [SerializeField]
    GameObject inventoryPanelObject;

    [SerializeField]
    float maxDistanceBeforeClosing;

    Transform player;

    // Audio Manager Script is set up here
    private SoundManager soundManager;

    public void Interact()
    {
        AlmanacProgression.instance.Unlock("SiloOpen");
        TutorialManagerScript.instance.Unlock("The Silo");
        
        if (inventoryPanelObject.activeInHierarchy)
        {
            inventoryPanelObject.SetActive(false);
            //NewInventory.instance.SetSpacesActive(false);
            NewInventory.instance.DisableForceActive();

        }
        else
        {
            soundManager.PlaySound("EnterShed");
            inventoryPanelObject.SetActive(true);
            NewInventory.instance.ForceActive();
        }

    }

    void CheckDistanceFromPlayer()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        if (distance > maxDistanceBeforeClosing && inventoryPanelObject.activeInHierarchy)
        {
            inventoryPanelObject.SetActive(false);
            NewInventory.instance.SetSpacesActive(false);
        }
    }

    void UpdatePosition()
    {
        Vector3 newPoint = Camera.main.WorldToScreenPoint(transform.position);
        newPoint.x = newPoint.x + inventoryPanelObject.GetComponent<RectTransform>().rect.width / 2;
        newPoint.y = newPoint.y + inventoryPanelObject.GetComponent<RectTransform>().rect.height / 2;

        inventoryPanelObject.transform.position = newPoint;
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //Audio Manager Is Opend Up here
        soundManager = SoundManager.instance;
    }
    private void Update()
    {
        CheckDistanceFromPlayer();
        UpdatePosition();
    }

}

