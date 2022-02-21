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
    public float waterDecreaseRate; //rate at which water decreases
    public float startGrowthLevel; //level at which plant growth starts
    public float fullGrowthLevel; //level at which plant will be fully grown
    public float growthRate; //the rate at which the plant will grow, added to the growth level
    public float growthPerStage; //growth needed to advance stages
    public bool potted;

    public GameObject[] plantModels; //models for each stage of plant growth
    public GameObject waterNeededIcon; //icon to display when water level is low
    public GameObject waterParticles; //particles for pouring water on plants
    public GameObject fertilizer; //fertilizer that will come from dead plants
    public Sprite fullyGrownIcon; //icon for fully grown plants
    public AudioSource waterSound;


    public PlanetSpecies planetSpecies;
    public ModifierTypes modifier;
    public Stage currentStage;

    float currentWaterLevel;
    float currentGrowth;
    int pastHour;

    // Start is called before the first frame update
    void Start()
    {
        //setting some basic information for water levels and growth rates
        currentWaterLevel = startWaterLevel;
        currentGrowth = startGrowthLevel;
        growthPerStage = fullGrowthLevel / 4;

        //setting the time and ui to start
        pastHour = -1;
        UpdateUI();

        //originalScale = plantModels[(int)currentStage].transform.localScale;
        //timeSinceLastGrowth = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //checking if plant is potted and then doing growth if needed
        if (potted == true)
        {
            if (pastHour != SunRotationScript.instance.CurrentHour)
            {
                pastHour = SunRotationScript.instance.CurrentHour;
                Growth();
            }
        }
    }

    void Growth()
    {
        //checking to see if there is enough water for the plant to live
        if (currentWaterLevel >= deathWaterLevel)
        {
            UpdateUI();

            //updating growth and water levels
            currentGrowth += growthRate;
            currentWaterLevel -= waterDecreaseRate;

            if (currentGrowth % growthPerStage == 0 && currentStage != Stage.Rotten)
            {
                if (currentStage == Stage.Ripe)
                {
                    GetComponent<IconHolder>().icon = fullyGrownIcon;
                    AlmanacProgression.instance.Unlock(planetSpecies.ToString() + "CropGrown");

                    //if (currentWaterLevel <= deathWaterLevel)
                    //{
                    //    plantModels[(int)currentStage].SetActive(false);
                    //    currentStage++;
                    //    plantModels[(int)currentStage].SetActive(true);
                    //}
                }
                else
                {
                    plantModels[(int)currentStage].SetActive(false);
                    currentStage++;
                    plantModels[(int)currentStage].SetActive(true);
                }
            }
        }
        else
        {
            //kill the plant/make it rotten
            plantModels[(int)currentStage].SetActive(false);
            currentStage = Stage.Rotten;
            plantModels[(int)currentStage].SetActive(true);
        }
    }

    void UpdateUI()
    {
        if (currentWaterLevel <= warningWaterLevel)
        {
            waterNeededIcon.SetActive(true);
        }
        else
        {
            waterNeededIcon.SetActive(false);
        }
    }

    public void AddElapsedHours(int hoursToAdd)
    {
        for (int i = 0; i < hoursToAdd; i++)
        {
            Growth();
        }
    }

    public void AddWater(float waterToAdd)
    {
        AlmanacProgression.instance.Unlock("WaterPlant");
        currentWaterLevel += waterToAdd;

        waterSound.pitch = Random.Range(0.5f, 1f);
        waterSound.Play();

        Instantiate(waterParticles, transform);

        if (currentWaterLevel > maxWaterLevel)
        {
            currentWaterLevel = maxWaterLevel;
        }
    }
}