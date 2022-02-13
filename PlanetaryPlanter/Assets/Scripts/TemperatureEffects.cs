using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemperatureEffects : MonoBehaviour
{
    //these public values will be multipliers ex. 1.25
        //so we can set these or intake temp values and set these accordingly
        //could perhaps make them arrays too?
    public float waterNeededChange;
    public float growthTimeChange;
    Biomes currentBiome;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetWaterChange()
    {
        return waterNeededChange;
    }

    public float GetGrowthChange()
    {
        return growthTimeChange;
    }

    public void SetCurrentBiome(Biomes biome)
    {
        currentBiome = biome;
    }
}
