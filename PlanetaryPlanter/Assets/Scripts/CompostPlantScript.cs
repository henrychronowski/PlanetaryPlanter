using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompostPlantScript : MonoBehaviour
{
    public NewInventory inventory;
    Transform player;

    [SerializeField]
    float distanceFromCompostBin;

    bool canCompost;

    // Start is called before the first frame update
    void Start()
    {
        canCompost = false;
        inventory = FindObjectOfType<NewInventory>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        CheckDistanceFromPlayer();
        CompostPlant();
    }

    public void CompostPlant()
    {
        if(Input.GetKeyDown(KeyCode.E) && canCompost && inventory.selectedSpace.gameObject.tag == "Plant")
        {
            inventory.PopItem();
        }
    }

    void CheckDistanceFromPlayer()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        if(distanceFromCompostBin > distance)
        {
            canCompost = true;
            Debug.Log("Can Compost");
        }
        else
        {
            canCompost = false;
            Debug.Log("Can't Compost");
        }
    }


}
