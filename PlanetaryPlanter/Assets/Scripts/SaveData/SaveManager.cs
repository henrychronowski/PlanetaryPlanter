using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        SaveSystem.SaveAllData(player, master, AlmanacProgression.instance, TutorialManagerScript.instance, NewInventory.instance);
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
    }

    public void LoadGame()
    {
        loadDataIntended = true;
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
    void Update()
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
            }
            else if (SceneManager.GetSceneByBuildIndex(activeSceneIndex).isLoaded && !loadDataIntended && !dataLoaded)
            {
                master = GameObject.FindObjectOfType<ObservatoryMaster>();
                player = GameObject.FindObjectOfType<CharacterMovement>();
                TutorialManagerScript.instance.Unlock("Welcome to Planetary Planter");

            }

        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            SaveData();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadData();
        }
    }
}
