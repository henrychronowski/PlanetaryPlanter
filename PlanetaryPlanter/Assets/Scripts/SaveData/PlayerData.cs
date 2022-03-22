using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float[] position;
    public int currentChapter;
    public bool[] spotsFilled;
    public string[] achievementsDone;
    public string[] tutorialsDone;
    public List<int> chapterCompletionRequirements;

    public ItemID[] itemIDs;
    public PlantsData plants;


    public PlayerData(CharacterMovement player, ObservatoryMaster observatoryMaster, AlmanacProgression alm, TutorialManagerScript tutorials, NewInventory inventory, PlantsData plantData)
    {
        position = new float[3] { player.transform.position.x, player.transform.position.y, player.transform.position.z };
        currentChapter = observatoryMaster.GetCurrentChapter();
        spotsFilled = observatoryMaster.GetFilledPlanetsOfCurrentChapter();
        achievementsDone = alm.GetAllCompletedAchievements();
        tutorialsDone = tutorials.GetAllCompletedTutorials();
        itemIDs = inventory.ReturnAllInventoryIDs();
        plants = plantData;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
