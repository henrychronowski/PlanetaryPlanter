using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public bool inPot;
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

    [SerializeField]
    float currentWater; //energy needed for plant to grow

    [SerializeField]
    float maxWater;

    [SerializeField]
    public float growthNeededForEachStage;

    [SerializeField]
    float growthProgress;

    public int hoursElapsed;

    IEnumerator growthTime;

    [SerializeField]
    float timeSinceLastGrowth;
    [SerializeField]
    float timeBetweenGrowths;

    public Vector3 originalScale;

    public Sprite smallIcon;

    public Sprite grownIcon;

    public TextMeshProUGUI waterText;

    [SerializeField]
    int lastRecordedHour;

    [SerializeField]
    float sinFactor;

    public GameObject fertilizer;

    void Growth()
    {
        if(currentWater > 0)
        {
            UpdateUI();
            growthProgress++;
            currentWater--;
            if(growthProgress % growthNeededForEachStage == 0 && stage != Stage.Rotten)
            {
                Debug.Log("Next Stage");
                plantModels[(int)stage].SetActive(false);
                stage++;
                plantModels[(int)stage].SetActive(true);

                if (stage == Stage.Ripe)
                {
                    GetComponent<IconHolder>().icon = grownIcon;
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
        waterText.text = currentWater + "/" + maxWater;
    }

    public void AddWater(float waterToAdd)
    {
        currentWater += waterToAdd;
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
    }

    // Update is called once per frame
    void Update()
    {
        if(inPot)
        {
            //timeSinceLastGrowth += Time.deltaTime;
            //if(timeBetweenGrowths <= timeSinceLastGrowth)
            //{
            //    Growth();
            //    timeSinceLastGrowth = 0;
            //}
            
            if (lastRecordedHour != SunRotationScript.instance.CurrentHour) 
            {
                lastRecordedHour = SunRotationScript.instance.CurrentHour;
                Growth();
            }
            //plantModels[(int)stage].transform.localScale = new Vector3(originalScale.x + (Mathf.Sin(Time.time) * sinFactor), originalScale.y, originalScale.z);
        }
    }
}
