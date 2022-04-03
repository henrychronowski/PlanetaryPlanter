using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompostPlantScript : MonoBehaviour
{
    public NewInventory inventory;
    Transform player;

    [SerializeField]
    float distanceFromCompostBin;

    bool canCompost;
    bool hasComposted;

    //Counts how many plants you've composted
    public int currentCompost;

    //The minimum amount of plants required to make fertilizer.
    public int minUntilFertilizer;

    public GameObject fertilizer;

    InteractableObject interactable;

    public string compostInteract;
    public string collectInteract;

    public List<Slider> sliders; 

    // Start is called before the first frame update
    void Start()
    {
        canCompost = false;
        hasComposted = false;
        inventory = FindObjectOfType<NewInventory>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        interactable = GetComponent<InteractableObject>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckDistanceFromPlayer();
        CompostPlant();
        hasComposted = false;
        UpdateSliders();

        interactable.interactText = InteractText();
    }

    public void CompostPlant()
    {
        if(Input.GetKeyDown(KeyCode.E) && canCompost)
        {
            if (inventory.selectedSpace.filled)
            {
                if (inventory.GetItem(inventory.selectedSpace).gameObject.tag == "Plant")
                {
                    inventory.PopItem();
                    currentCompost++;
                    hasComposted = true;
                }
            }

            if (inventory.SpacesAvailable() > 0 && currentCompost >= minUntilFertilizer && !hasComposted)
            {
                currentCompost -= minUntilFertilizer;
                inventory.AddItem(fertilizer);
            }
        }
    }

    void CheckDistanceFromPlayer()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        if(distanceFromCompostBin > distance)
        {
            canCompost = true;
            TutorialManagerScript.instance.Unlock("Compost Bin");
        }
        else
        {
            canCompost = false;
        }
    }

    string InteractText()
    {
        if(currentCompost >= minUntilFertilizer)
        {
            return collectInteract;
        }

        return compostInteract;
    }

    void UpdateSliders()
    {
        foreach(Slider slider in sliders)
        {
            slider.value = ((float)currentCompost / (float)minUntilFertilizer);
        }    
    }
}