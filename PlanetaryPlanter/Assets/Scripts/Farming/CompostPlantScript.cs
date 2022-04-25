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

    public GameObject fill;
    public Transform noFill;
    public Transform filled;

    // Audio Manager Script is set up here
    private SoundManager soundManager;

    // Start is called before the first frame update
    void Start()
    {
        canCompost = false;
        hasComposted = false;
        inventory = FindObjectOfType<NewInventory>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        interactable = GetComponent<InteractableObject>();

        //Audio Manager Is Opend Up here
        soundManager = SoundManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        CheckDistanceFromPlayer();
        hasComposted = false;
        UpdateFillProgress();

        interactable.interactText = InteractText();
    }

    public void CompostPlant()
    {
            if (inventory.selectedSpace.filled)
            {
                if (inventory.GetItem(inventory.selectedSpace).gameObject.tag == "Plant")
                {
                    soundManager.PlaySound("Composting");
                    inventory.PopItem();
                    currentCompost++;
                    hasComposted = true;
                }
            }

            if (inventory.SpacesAvailable() > 0 && currentCompost >= minUntilFertilizer && !hasComposted)
            {
                currentCompost -= minUntilFertilizer;
                inventory.AddItem(fertilizer);
                soundManager.PlaySound("CollectItem");
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

    void UpdateFillProgress()
    {
        float progress = ((float)currentCompost / (float)minUntilFertilizer);
        fill.transform.position = Vector3.Lerp(noFill.position, filled.position, progress);
    }
}
