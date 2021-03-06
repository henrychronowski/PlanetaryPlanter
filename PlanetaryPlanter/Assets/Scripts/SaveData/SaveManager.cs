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
            if (planters[i].plantObject == null)
            {
                speciesArray[i] = PlanetSpecies.Unidentified;
                growthValue[i] = -1;
                waterValue[i] = -1;
            }
            else
            {
                speciesArray[i] = planters[i].placedPlant.species;
                growthValue[i] = planters[i].placedPlant.growthProgress;
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
    public float timeSinceLoad;
    public float loadGracePeriod; //disables some systems for the alotted time, avoids some loading weirdness
    public PlayerData playerData;
    public CharacterMovement player;
    public ObservatoryMaster master;
    public PortalMapScript portalMap;
    public int activeSceneIndex;
    public bool dataLoaded;
    public bool loadingStarted;
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
        portalMap = GameObject.FindObjectOfType<PortalMapScript>();
        SaveSystem.SaveAllData(player, master, AlmanacProgression.instance, TutorialManagerScript.instance, NewInventory.instance, new PlantsData(plantSpots), portalMap);
    }

    public void LoadData()
    {
        if(!loadingStarted)
            playerData = SaveSystem.LoadAllData();

        loadingStarted = true;
        //set all variables properly
        master = GameObject.FindObjectOfType<ObservatoryMaster>();
        player = GameObject.FindObjectOfType<CharacterMovement>();
        portalMap = GameObject.FindObjectOfType<PortalMapScript>();
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

        for(int i = 0; i < portalMap.portals.Count; i++)
        {
            portalMap.portals[i].portalFound = playerData.portalsFound[i];
        }
    }

    public bool GameDataFound()
    {
        GameObject.Find("LoadGame").SetActive(SaveSystem.CheckForData());
        return false;
    }

    public void LoadGame()
    {
        loadDataIntended = true;
        dataLoaded = false;
        loadingStarted = false;
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
        if(SceneManager.GetActiveScene().buildIndex == 0 || SceneManager.GetActiveScene().buildIndex == 2)
        {
            Cursor.lockState = CursorLockMode.None;
        }
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
                    playerData = SaveSystem.LoadAllData();
                if(GameObject.FindObjectOfType<UnlockSystem>().currentChapter == playerData.currentChapter)
                {
                    LoadData();
                    dataLoaded = true;
                    loadDataIntended = false;
                }
                else
                {
                    GameObject.FindObjectOfType<UnlockSystem>().UnlockChapters(playerData.currentChapter);

                }
            }
            else if (SceneManager.GetSceneByBuildIndex(activeSceneIndex).isLoaded && !loadDataIntended && !dataLoaded)
            {
                master = GameObject.FindObjectOfType<ObservatoryMaster>();
                player = GameObject.FindObjectOfType<CharacterMovement>();
                TutorialManagerScript.instance.Unlock("Welcome to Planetary Planter");

            }

        }
        if(dataLoaded)
            timeSinceLoad += Time.deltaTime;

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
