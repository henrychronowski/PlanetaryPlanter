using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public GameObject player; //to reference the player
    public Text biomeText;

    float currentTemperature;
    float currentTempChangeTime;
    Collider[] objectsInBiome;
    Biomes currentBiome;

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

            CheckObjectsInRange();
        }

        DisplayCurrentBiome();
    }

    public void DisplayCurrentBiome()
    {
        biomeText.text = "Current biome is: " + currentBiome;
    }

    void CheckObjectsInRange()
    {
        objectsInBiome = Physics.OverlapSphere(gameObject.transform.position, biomeSize);

        foreach (Collider collider in objectsInBiome)
        {
            Debug.Log(collider.gameObject.name);
        }

        SetActiveBiomeForObjects();
    }

    void CalculateTemperatureChange()
    {
        currentTemperature = Random.Range(minTemperature, maxTemperature);

        if (currentTemperature >= 67.0f)
        {
            currentBiome = Biomes.Hot;
        }

        if (currentTemperature < 67.0f && currentTemperature > 33.0f)
        {
            currentBiome = Biomes.Temperate;
        }

        if (currentTemperature <= 33.0f)
        {
            currentBiome = Biomes.Cold;
        }
    }

    void SetActiveBiomeForObjects()
    {
        for (int i = 0; i < objectsInBiome.Length; i++)
        {
            if (currentTemperature >= 67.0f)
            {
                objectsInBiome[i].gameObject.GetComponent<TemperatureEffects>().SetCurrentBiome(Biomes.Hot);

                if (objectsInBiome[i].gameObject == player)
                {
                    Debug.Log("Acknowledging Player");
                    objectsInBiome[i].gameObject.GetComponent<DisplayPlayerBiome>().SetCurrentBiome(Biomes.Hot);
                }
            }
            
            if (currentTemperature < 67.0f && currentTemperature > 33.0f)
            {
                objectsInBiome[i].gameObject.GetComponent<TemperatureEffects>().SetCurrentBiome(Biomes.Temperate);

                if (objectsInBiome[i].gameObject == player)
                {
                    Debug.Log("Acknowledging Player");
                    objectsInBiome[i].gameObject.GetComponent<DisplayPlayerBiome>().SetCurrentBiome(Biomes.Temperate);
                }
            }
            
            if (currentTemperature <= 33.0f)
            {
                objectsInBiome[i].gameObject.GetComponent<TemperatureEffects>().SetCurrentBiome(Biomes.Cold);

                if (objectsInBiome[i].gameObject == player)
                {
                    Debug.Log("Acknowledging Player");
                    objectsInBiome[i].gameObject.GetComponent<DisplayPlayerBiome>().SetCurrentBiome(Biomes.Cold);
                }
            }
        }
    }

    void SetSpecificBiome(GameObject gameObject)
    {
        if (currentTemperature >= 67.0f)
        {
            gameObject.GetComponent<TemperatureEffects>().SetCurrentBiome(Biomes.Hot);

            if (gameObject == player)
            {
                gameObject.GetComponent<DisplayPlayerBiome>().SetCurrentBiome(Biomes.Hot);
            }
        }

        if (currentTemperature < 67.0f && currentTemperature > 33.0f)
        {
            gameObject.GetComponent<TemperatureEffects>().SetCurrentBiome(Biomes.Temperate);

            if (gameObject == player)
            {
                gameObject.GetComponent<DisplayPlayerBiome>().SetCurrentBiome(Biomes.Temperate);
            }
        }

        if (currentTemperature <= 33.0f)
        {
            gameObject.GetComponent<TemperatureEffects>().SetCurrentBiome(Biomes.Cold);

            if (gameObject == player)
            {
                gameObject.GetComponent<DisplayPlayerBiome>().SetCurrentBiome(Biomes.Cold);
            }
        }
    }
}