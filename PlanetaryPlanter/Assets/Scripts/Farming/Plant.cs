using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public bool inPot;

    // Audio Manager Script is set up here
    private SoundManager soundManager;
    public enum Stage
    {
        Stage1,
        Stage2,
        Ripe,
        Rotten
    }
    
    

    public Stage stage;

    [SerializeField]
    List<GameObject> plantModels;

    public float currentWater; //energy needed for plant to grow

    [SerializeField]
    float maxWater;

    [SerializeField]
    float waterWarningValue;

    [SerializeField]
    public float growthNeededForEachStage;

    [SerializeField]
    public float growthProgress;

    public int hoursElapsed;

    IEnumerator growthTime;
    
    [SerializeField]
    float timeSinceLastGrowth;
    [SerializeField]
    float timeBetweenGrowths;

    public float growthRequiredToRot;

    public Vector3 originalScale;

    public Sprite smallIcon;

    public Sprite grownIcon;

    public TextMeshProUGUI waterText;

    public GameObject waterIcon;
    public GameObject waterParticles;

    public AudioSource waterSound;

    [SerializeField]
    int lastRecordedHour;

    [SerializeField]
    float sinFactor;

    public GameObject fertilizer;

    public PlanetSpecies species;

    public PlanetType type;
    
    
    void Growth()
    {
        if(currentWater > 0)
        {
            UpdateUI();
            growthProgress++;
            currentWater--;
            if(growthProgress % growthNeededForEachStage == 0 && stage != Stage.Rotten)
            {
                if (stage == Stage.Ripe)
                {
                    GetComponent<IconHolder>().icon = grownIcon;
                    AlmanacProgression.instance.Unlock(species.ToString() + "CropGrown");
                    if(growthProgress >= growthRequiredToRot)
                    {
                        Debug.Log("Next Stage");
                        plantModels[(int)stage].SetActive(false);
                        stage++;
                        plantModels[(int)stage].SetActive(true);
                    }
                    
                }
                else
                {
                    Debug.Log("Next Stage");
                    plantModels[(int)stage].SetActive(false);
                    stage++;
                    plantModels[(int)stage].SetActive(true);
                }

            }
        }
        
    }
    
    public void AddElapsedHours(int hoursToAdd)
    {
        for(int i = 0; i < hoursToAdd; i++)
        {
            Growth();
        }
    }

    void UpdateUI()
    {
        if(currentWater <= waterWarningValue)
        {
            waterIcon.SetActive(true);
        }
        else
        {
            waterIcon.SetActive(false);
        }
    }

    public void AddWater(float waterToAdd)
    {
        AlmanacProgression.instance.Unlock("WaterPlant");
        currentWater += waterToAdd;
        waterSound.pitch = Random.Range(0.5f, 1f);
        soundManager.PlaySound("WaterPlant");
        Instantiate(waterParticles, transform);
        if(currentWater > maxWater)
        {
            currentWater = maxWater;
        }
        UpdateUI();
    }
    

    // Start is called before the first frame update
    void Start()
    {
        lastRecordedHour = -1;
        originalScale = plantModels[(int)stage].transform.localScale;
        timeSinceLastGrowth = 0;
        UpdateUI();

        //Audio Manager Is Opend Up here
        soundManager = SoundManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if(inPot)
        {
            if (lastRecordedHour != SunRotationScript.instance.CurrentHour) 
            {
                lastRecordedHour = SunRotationScript.instance.CurrentHour;
                Growth();
            }
        }
    }
}
