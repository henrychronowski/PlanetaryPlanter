using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemperatureEffects : MonoBehaviour
{
    //these public values will be multipliers ex. 1.25
        //so we can set these or intake temp values and set these accordingly
        //could perhaps make them arrays too?
    public float heatGrowthChange;
    public float coldGrowthChange;
    public float temperateGrowthChange;
    public PlanetType modifier;
    public PlanetSpecies species;
        
    Biomes currentBiome;
    float baseGrowth;

    // Start is called before the first frame update
    void Start()
    {
        //baseGrowth = gameObject.GetComponent<PlantTool>().growthRate;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ApplyGrowthChange()
    {
        if (currentBiome == Biomes.Hot)
        {
            //gameObject.GetComponent<PlantTool>().growthRate += heatGrowthRate;
        }

        if (currentBiome == Biomes.Cold)
        {
            //gameObject.GetComponent<PlantTool>().growthRate += coldGrowthRate;
        }

        if (currentBiome == Biomes.Temperate)
        {
            //gameObject.GetComponent<PlantTool>().growthRate += temperateGrowthRate;
        }
    }

    public void SetCurrentBiome(Biomes biome)
    {
        currentBiome = biome;
    }
}