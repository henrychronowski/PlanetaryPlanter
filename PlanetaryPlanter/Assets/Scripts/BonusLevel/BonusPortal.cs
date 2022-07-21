using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusPortal : MonoBehaviour
{
    [SerializeField] GameObject levelDesignPrefab;
    [SerializeField] float timeValue;
    [SerializeField] string levelName;
    [SerializeField] public GameObject reward;
    [SerializeField] LevelDifficulty difficulty;
    BonusLevel level;
    BonusLevelMaster levelMaster;
    PortalFoundScript.Biome biome;
    public bool completed;
    public void Interact()
    {
        
        EnvironmentManager.Instance.TransitionToBonus();
        levelMaster.Activate(level);

    }

    // Start is called before the first frame update
    void Start()
    {
        levelMaster = GameObject.FindObjectOfType<BonusLevelMaster>();
        level = new BonusLevel(levelDesignPrefab, timeValue, difficulty, reward, transform, biome);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
