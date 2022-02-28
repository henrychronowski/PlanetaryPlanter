using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class Observatory : MonoBehaviour
{

    public Transform bottomLeftCorner;
    public int width;
    public int height;
    public float xDistanceBetweenPlanets;
    public float yDistanceBetweenPlanets;

    public GameObject emptySlot;
    public ObservatoryPlanetSpot[,] planetSpotsArray; //Part of the old implementation
    public bool createPlanetsOnStart;

    public List<ObservatoryPlanetSpot> constellationSpots; //New implementation

    public bool completed;
    public GameObject solarSystemButton;
    public TextMeshProUGUI numComplete;

    public Sprite completedConstellationSprite;

    public Transform next;
    public string completionAchievementName;
    LineRenderer line;
    public int filledSpots;


    void CreateEmptySlots()
    {
        for(int i = 0; i < width; i++)
        {
            for(int j = height-1; j >= 0; j--)
            {
                GameObject temp = Instantiate(emptySlot, 
                    new Vector3(bottomLeftCorner.position.x + (xDistanceBetweenPlanets * i), bottomLeftCorner.position.y + (yDistanceBetweenPlanets * j), bottomLeftCorner.position.z),
                    Quaternion.identity,
                    bottomLeftCorner);
                temp.name = "PlanetSpot " + i.ToString() + j.ToString(); 
                planetSpotsArray[i,j] = temp.GetComponent<ObservatoryPlanetSpot>();
            }
        }
    }

    bool CheckForCompletion()
    {
        foreach(ObservatoryPlanetSpot spot in constellationSpots)
        {
            if(!spot.filled)
            {
                return false;
            }
        }
        Complete();
        
        return true;

    }

    public int GetFilledSpots()
    {
        int filled = 0;
        foreach (ObservatoryPlanetSpot spot in constellationSpots)
        {
            if (spot.filled)
            {
                filled++;
            }
        }
        numComplete.text = filled + "/" + constellationSpots.Count;
        return filled;
    }

    void Complete()
    {
        line.enabled = true;
        completed = true;
        solarSystemButton.GetComponent<UnityEngine.UI.Image>().sprite = completedConstellationSprite;
        
        AlmanacProgression.instance.Unlock(completionAchievementName);

    }

    // Start is called before the first frame update
    void Start()
    {
        planetSpotsArray = new ObservatoryPlanetSpot[width, height];
        constellationSpots.AddRange(GetComponentsInChildren<ObservatoryPlanetSpot>());
        line = GetComponent<LineRenderer>();
        line.SetPosition(0, solarSystemButton.transform.position);
        line.SetPosition(1, next.position);
        line.enabled = false;
        //line.colorGradient.
    }

    // Update is called once per frame
    void Update()
    {
        if(!completed)
        {
            GetFilledSpots();
            CheckForCompletion();
        }
        else
        {
        }
    }

}
