using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Biomes
{
    Hot,
    Cold,
    Temperate
}

public class TemperatureManager : MonoBehaviour
{
    public GameObject temperatureLocation; //location the temperature radiates from
    public float maxTemperature; //max temperature for zone
    public float minTemperature; //min temperature for zone
    public float biomeSize; //size of the biome for this temperature
    public float tempChangeTime; //time until temperature changes;

    float currentTemperature;
    float currentTempChangeTime;
    Collider[] objectsInBiome;

    // Start is called before the first frame update
    void Start()
    {
        CalculateTemperatureChange();
        currentTempChangeTime = tempChangeTime;
    }

    // Update is called once per frame
    void Update()
    {
        currentTempChangeTime -= Time.deltaTime;

        if (currentTempChangeTime <= 0.0f)
        {
            CalculateTemperatureChange();
            currentTempChangeTime = tempChangeTime;
        }

        CheckObjectsInRange();
    }

    void CheckObjectsInRange()
    {
        objectsInBiome = Physics.OverlapSphere(gameObject.transform.position, biomeSize);

        SetActiveBiomeForObjects();
    }

    void CalculateTemperatureChange()
    {
        currentTemperature = Random.Range(minTemperature, maxTemperature);
    }

    void SetActiveBiomeForObjects()
    {
        foreach(Collider collider in objectsInBiome)
        {
            if (currentTemperature >= 67.0f)
            {
                collider.gameObject.GetComponent<TemperatureEffects>().SetCurrentBiome(Biomes.Hot);

                if (collider.gameObject == GameObject.Find("Player"))
                {
                    collider.gameObject.GetComponent<DisplayPlayerBiome>().SetCurrentBiome(Biomes.Hot);
                }
            }
            else if (currentTemperature < 67.0f && currentTemperature > 33.0f)
            {
                collider.gameObject.GetComponent<TemperatureEffects>().SetCurrentBiome(Biomes.Temperate);

                if (collider.gameObject == GameObject.Find("Player"))
                {
                    collider.gameObject.GetComponent<DisplayPlayerBiome>().SetCurrentBiome(Biomes.Temperate);
                }
            }
            else if (currentTemperature <= 33.0f)
            {
                collider.gameObject.GetComponent<TemperatureEffects>().SetCurrentBiome(Biomes.Cold);

                if (collider.gameObject == GameObject.Find("Player"))
                {
                    collider.gameObject.GetComponent<DisplayPlayerBiome>().SetCurrentBiome(Biomes.Cold);
                }
            }
        }
    }
}