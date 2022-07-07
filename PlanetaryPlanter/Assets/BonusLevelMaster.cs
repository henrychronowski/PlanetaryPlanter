using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LevelDifficulty
{
    Easy,
    Normal,
    Hard
}

public struct BonusLevel
{
    public BonusLevel(GameObject level, float time, LevelDifficulty diff, GameObject reward, Transform portal, PortalFoundScript.Biome biome)
    {
        levelDesignPrefab = level;
        timer = time;
        difficulty = diff;
        completeReward = reward;
        portalEntrance = portal;
        portalBiome = biome;
    }
    public GameObject levelDesignPrefab;
    public float timer;
    public LevelDifficulty difficulty;
    public GameObject completeReward;
    public Transform portalEntrance;
    public PortalFoundScript.Biome portalBiome;
}

// Disables and re enables certain systems based on if the player is in a bonus level currently
public class BonusLevelMaster : MonoBehaviour
{
    public bool bonusLevelActive = false;
    [SerializeField] Transform bonusLevelPlayerSpawn;
    [SerializeField] Transform bonusLevelArea;
    [SerializeField] GameObject timerUIObject;
    [SerializeField] TMPro.TextMeshProUGUI timerText;
    [SerializeField] float currentTimeLeft;
    BonusLevel currentLevel;
    GameObject levelDesignObject;

    [SerializeField] Sprite failurePopupSprite;
    [SerializeField] string failurePopupMessage;
    [SerializeField] Sprite successPopupSprite;
    [SerializeField] string successPopupMessage;

    public void Activate(BonusLevel level)
    {
        if(!bonusLevelActive)
        {
            currentLevel = level;
            bonusLevelActive = true;
            SaveManager.instance.canSave = false;
            SunRotationScript.instance.timeStopped = true;
            levelDesignObject = Instantiate(currentLevel.levelDesignPrefab, bonusLevelArea);
            GameObject.FindObjectOfType<CharacterMovement>().Teleport(bonusLevelPlayerSpawn);
            currentTimeLeft = currentLevel.timer;
            timerUIObject.SetActive(true);
        }
    }

    public void Deactivate(bool completed = false)
    {
        if (bonusLevelActive)
        {
            bonusLevelActive = false;
            SaveManager.instance.canSave = true;
            SunRotationScript.instance.timeStopped = false;
            EnvironmentManager.Instance.TransitionToBiome(currentLevel.portalBiome);
            GameObject.FindObjectOfType<CharacterMovement>().Teleport(currentLevel.portalEntrance);
            timerUIObject.SetActive(false);

            if(completed)
            {
                NewInventory.instance.AddItem(currentLevel.completeReward);
                PopUpController.instance.NewPopUp(failurePopupMessage, failurePopupSprite);

            }
            else
            {
                PopUpController.instance.NewPopUp(failurePopupMessage, failurePopupSprite);
            }
            Destroy(levelDesignObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(bonusLevelActive)
        {
            SaveManager.instance.canSave = false;
            currentTimeLeft -= Time.deltaTime;
            timerText.text = ((int)currentTimeLeft).ToString(); //avoids decimal places entirely
            if(currentTimeLeft <= 0)
            {
                Deactivate();
            }
        }
        else
        {
            SaveManager.instance.canSave = true;
        }
    }
}
