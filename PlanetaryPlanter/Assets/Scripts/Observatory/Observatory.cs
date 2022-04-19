using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class Observatory : MonoBehaviour
{
    public List<ObservatoryPlanetSpot> constellationSpots; //New implementation

    public bool completed;
    public GameObject solarSystemButton;
    public TextMeshProUGUI numComplete;

    public Sprite completedConstellationSprite;

    public Transform next;
    public List<Transform> connections;
    public string completionAchievementName;
    LineRenderer baseLine; //used as a base when new connections are made in Start()
    public List<LineRenderer> lines;
    public int filledSpots;

    // Audio Manager Script is set up here
    private SoundManager soundManager;

    public SolarSystemCountScript solarSystemCounter;


    public bool CheckForCompletion()
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

    public List<ObservatoryPlanetSpot> GetPlanetSpots()
    {
        constellationSpots.Clear();
        constellationSpots.AddRange(GetComponentsInChildren<ObservatoryPlanetSpot>());
        return constellationSpots;
    }

    void Complete()
    {
        SoundManager.instance.PlaySound("Craft");
        foreach (LineRenderer l in lines)
        {
            l.enabled = true;
        }
        
        completed = true;
        solarSystemButton.GetComponent<UnityEngine.UI.Image>().sprite = completedConstellationSprite;

        solarSystemCounter.numSolarSystemsComplete++;
        AlmanacProgression.instance.Unlock(completionAchievementName + solarSystemCounter.numSolarSystemsComplete.ToString());

    }

    // Start is called before the first frame update
    void Start()
    {
        constellationSpots.AddRange(GetComponentsInChildren<ObservatoryPlanetSpot>());
        lines = new List<LineRenderer>();
        lines.AddRange(GetComponentsInChildren<LineRenderer>());
        for (int i = 0; i < connections.Count; i++)
        {
            lines[i].SetPosition(0, solarSystemButton.transform.position);
            lines[i].SetPosition(1, connections[i].position);
            lines[i].enabled = false;
        }

        solarSystemCounter = FindObjectOfType<SolarSystemCountScript>();

        //Audio Manager Is Opend Up here
        soundManager = SoundManager.instance;
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
