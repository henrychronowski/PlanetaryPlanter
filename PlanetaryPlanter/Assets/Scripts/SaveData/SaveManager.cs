using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public struct PlantsData
{
    public PlantsData(List<PlantSpot> planters)
    {
        planterIds = new int[planters.Count];
        speciesArray = new PlanetSpecies[planters.Count];
        growthValue = new float[planters.Count];
        waterValue = new float[planters.Count];

        for (int i = 0; i < planters.Count; i++)
        {
            planterIds[i] = i;
            if (planters[i].placedPlant == null)
            {
                speciesArray[i] = PlanetSpecies.Unidentified;
                growthValue[i] = -1;
                waterValue[i] = -1;
            }
            else
            {
                speciesArray[i] = planters[i].placedPlant.species;
                growthValue[i] = planters[i].placedPlant.growthProgress;
                Debug.Log("Save growth as " + growthValue[i]);
                waterValue[i] = planters[i].placedPlant.currentWater;
            }

        }
        
    }

    public int[] planterIds;
    public PlanetSpecies[] speciesArray;
    public float[] growthValue;
    public float[] waterValue;
}

[System.Serializable]
public struct InventoryItemSave
{
    public InventoryItemSave(string name, ItemID itemId)
    {
        spaceName = name;
        id = itemId;
    }
    public string spaceName;
    public ItemID id;
}
public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;

    public float timeBetweenSaves;
    public float timeSinceLastSave;
    public PlayerData playerData;
    public CharacterMovement player;
    public ObservatoryMaster master;
    public int activeSceneIndex;
    public bool dataLoaded;
    public bool loadDataIntended;

    

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void SaveData()
    {
        List<PlantSpot> plantSpots = new List<PlantSpot>();
        plantSpots.AddRange(GameObject.FindObjectsOfType<PlantSpot>(true).OrderBy(gameObject => gameObject.name).ToArray());
        master = GameObject.FindObjectOfType<ObservatoryMaster>();
        player = GameObject.FindObjectOfType<CharacterMovement>();
        SaveSystem.SaveAllData(player, master, AlmanacProgression.instance, TutorialManagerScript.instance, NewInventory.instance, new PlantsData(plantSpots));
    }

    public void LoadData()
    {
        playerData = SaveSystem.LoadAllData();
        //set all variables properly
        master = GameObject.FindObjectOfType<ObservatoryMaster>();
        player = GameObject.FindObjectOfType<CharacterMovement>();
        if (master == null || player == null || AlmanacProgression.instance == null || TutorialManagerScript.instance == null)
        {
            Debug.Log("Loading");
            return;
        }
        master.LoadFilledSpots(playerData.spotsFilled, playerData.currentChapter);
        player.Teleport(new Vector3(playerData.position[0], playerData.position[1], playerData.position[2]));
        AlmanacProgression.instance.LoadCompletedAchievements(playerData.achievementsDone);
        TutorialManagerScript.instance.LoadCompletedTutorials(playerData.tutorialsDone);
        NewInventory.instance.LoadAllItems(GameObject.FindObjectOfType<InventoryItemIndex>(), playerData.itemIDs);

        //Spawn all the new plants
        PlantSpot[] planters = GameObject.FindObjectsOfType<PlantSpot>(true).OrderBy(gameObject => gameObject.name).ToArray();
        for(int i = 0; i < playerData.plants.planterIds.Length; i++)
        {
            planters[i].PlacePlantOfType(playerData.plants.speciesArray[i], playerData.plants.growthValue[i]);
            //planters[i].placedPlant.currentWater = playerData.plants.waterValue[i];
        }
    }

    public void LoadGame()
    {
        loadDataIntended = true;
        dataLoaded = false;
        SceneManager.LoadScene(activeSceneIndex);
    }

    public void NewGame()
    {
        SceneManager.LoadScene(activeSceneIndex);

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (SceneManager.GetActiveScene().buildIndex == activeSceneIndex)
        {
            timeSinceLastSave += Time.deltaTime;
            if(timeSinceLastSave > timeBetweenSaves)
            {
                SaveData();
                timeSinceLastSave = 0;
            }
            
            //Only runs once when pressing load game or new game
            if(SceneManager.GetSceneByBuildIndex(activeSceneIndex).isLoaded && loadDataIntended && !dataLoaded)
            {
                LoadData();
                dataLoaded = true;
                loadDataIntended = false;
            }
            else if (SceneManager.GetSceneByBuildIndex(activeSceneIndex).isLoaded && !loadDataIntended && !dataLoaded)
            {
                master = GameObject.FindObjectOfType<ObservatoryMaster>();
                player = GameObject.FindObjectOfType<CharacterMovement>();
                TutorialManagerScript.instance.Unlock("Welcome to Planetary Planter");

            }

        }

        //if (Input.GetKeyDown(KeyCode.K))
        //{
        //    SaveData();
        //}

        //if (Input.GetKeyDown(KeyCode.L))
        //{
        //    LoadData();
        //}
    }
}
