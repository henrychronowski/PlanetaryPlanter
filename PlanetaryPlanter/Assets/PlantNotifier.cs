using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlantNotifier : MonoBehaviour
{
    [SerializeField] List<PlantSpot> spots;
    float blinkLength;
    float timeBetweenBlinks;
    float timeSinceLastBlink;
    int plantsReady;
    [SerializeField] Image alert;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] GameObject panel;
    void PlantsReady()
    {
        timeBetweenBlinks += Time.deltaTime;
        plantsReady = 0;
        foreach(PlantSpot spot in spots)
        {
            if(spot.GetPlantStatus())
                plantsReady++;
        }
    }

    public void SetText()
    {
        text.text = (plantsReady + " Plants Ready");

        if(plantsReady == 1)
        {
            text.text = (plantsReady + " Plant Ready");
            TutorialManagerScript.instance.Unlock("Plant Ready!");
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        spots = new List<PlantSpot>();
        spots.AddRange(FindObjectsOfType<PlantSpot>(true));
    }

    // Update is called once per frame
    void Update()
    {
        PlantsReady();
        SetText();
        if(plantsReady == 0)
        {
            panel.SetActive(false);
        }
        else
        {
            panel.SetActive(true);
        }
        //Check plant status
    }
}
