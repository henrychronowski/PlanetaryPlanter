using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class CollectSeedScript : MonoBehaviour
{
    public bool canCollect = false;
    public bool infinite = false; //testing
    public GameObject seed;
    public GameObject seedModel;
    public int hoursBeforeRespawn;
    public int hourLastCollected;
    SunRotationScript time;
    public string canCollectTip;
    public int hoursSinceLastCollected;

    //public GameObject fertilizer;

    //public bool collected = false;

    // Audio Manager Script is set up here
    private SoundManager soundManager;

    // Start is called before the first frame update
    void Start()
    {
        //NewInventory.instance.AddItem(fertilizer);
        time = GameObject.FindObjectOfType<SunRotationScript>();
        hourLastCollected = -1; //has not been collected yet

        //Audio Manager Is Opend Up here
        soundManager = SoundManager.instance;

    }


    // Update is called once per frame
    void Update()
    {
        //CollectSeed();
        CanCollect();
        if (canCollect)
        {
            seedModel.SetActive(true);
        }
        else
        {
            seedModel.SetActive(false);
        }
    }

    void CanCollect()
    {
        if (hourLastCollected == -1)
        {
            canCollect = true;
            return;
        }
        hoursSinceLastCollected = time.GetTotalElapsedTime() - hourLastCollected;
        if (time.GetTotalElapsedTime() - hourLastCollected >= hoursBeforeRespawn)
        {
            canCollect = true;
            GetComponent<InteractableObject>().interactText = canCollectTip;
        }
        else
        {
            canCollect = false;
            GetComponent<InteractableObject>().interactText = "";
        }
    }

    public void CollectSeed()
    {
        if(canCollect || infinite)
        {
            if(NewInventory.instance.SpacesAvailable() > 0)
            {
                hourLastCollected = time.GetTotalElapsedTime();
                GameObject temp = Instantiate(seed, transform.parent, false);
                temp.transform.position = new Vector3(10000, 100000);
                NewInventory.instance.AddItem(temp);
                soundManager.PlaySound("CollectItem");
                if(seed.tag == "Seed")
                {
                    TutorialManagerScript.instance.Unlock("Planting Seeds");
                    return;
                }
                else if(seed.tag == "Modifier")
                {
                    TutorialManagerScript.instance.Unlock("Modifiers");
                    return;
                }
            }
        }
    }
}
