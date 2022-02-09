using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantTool : MonoBehaviour
{
    public enum Stage
    {
        Planted,
        Developing,
        Ripe,
        Rotten
    }

    public float deathWaterLevel; //water level at which plant will die
    public float warningWaterLevel; //water level when the warning icon will appear
    public float startWaterLevel; //water level plant will start at
    public float maxWaterLevel; //water level the plant can reach at maximum
    public float startGrowthLevel; //level at which plant growth starts
    public float fullGrowthLevel; //level at which plant will be fully grown

    public GameObject[] plantModels; //models for each stage of plant growth
    public GameObject waterNeeded;

    public PlanetSpecies planetSpecies;
    public ModifierTypes modifier;
    public Stage currentStage;

    float currentWaterLevel;
    float currentGrowth;

    // Start is called before the first frame update
    void Start()
    {
        currentWaterLevel = startWaterLevel;
        currentGrowth = startGrowthLevel;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}