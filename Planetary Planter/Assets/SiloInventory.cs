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

    public void Interact()
    {
        if(inventoryPanelObject.activeInHierarchy)
        {
            inventoryPanelObject.SetActive(false);
        }
        else
        {
            inventoryPanelObject.SetActive(true);
        }

    }

    void CheckDistanceFromPlayer()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        if (distance > maxDistanceBeforeClosing && inventoryPanelObject.activeInHierarchy)
        {
            inventoryPanelObject.SetActive(false);
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
    }
    private void Update()
    {
        CheckDistanceFromPlayer();
        UpdatePosition();
    }

}