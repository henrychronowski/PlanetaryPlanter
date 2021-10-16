using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public bool inPot;
    public enum Stage
    {
        Stage1,
        Stage2,
        Stage3,
        Final
    }

    [SerializeField]
    Stage stage;

    [SerializeField]
    List<GameObject> plantModels;

    [SerializeField]
    float currentWater; //energy needed for plant to grow

    [SerializeField]
    float maxWater;

    [SerializeField]
    float growthNeededForEachStage;

    [SerializeField]
    float growthProgress;

    IEnumerator growthTime;

    [SerializeField]
    float timeSinceLastGrowth;
    [SerializeField]
    float timeBetweenGrowths;

    public Vector3 originalScale;

    public Sprite smallIcon;

    public Sprite grownIcon;


    [SerializeField]
    float sinFactor;

    void Growth()
    {
        if(currentWater > 0)
        {
            growthProgress++;
            currentWater--;
            if(growthProgress % growthNeededForEachStage == 0 && stage != Stage.Final)
            {
                Debug.Log("Next Stage");
                plantModels[(int)stage].SetActive(false);
                stage++;
                plantModels[(int)stage].SetActive(true);

                if (stage == Stage.Final)
                {
                    GetComponent<IconHolder>().icon = grownIcon;
                }
            }
        }
    }

    void AddWater(float waterToAdd)
    {
        currentWater += waterToAdd;
        if(currentWater > maxWater)
        {
            currentWater = maxWater;
        }
    }
    

    // Start is called before the first frame update
    void Start()
    {
        originalScale = plantModels[(int)stage].transform.localScale;
        timeSinceLastGrowth = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(inPot)
        {

        timeSinceLastGrowth += Time.deltaTime;
        if(timeBetweenGrowths <= timeSinceLastGrowth)
        {
            Growth();
            timeSinceLastGrowth = 0;
        }
        //plantModels[(int)stage].transform.localScale = new Vector3(originalScale.x + (Mathf.Sin(Time.time) * sinFactor), originalScale.y, originalScale.z);
        }
    }
}
