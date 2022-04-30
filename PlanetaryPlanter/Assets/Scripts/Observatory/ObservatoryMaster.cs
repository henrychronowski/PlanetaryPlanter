using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Holds the public function to enter the observatory and handles constellation selection UI
public class ObservatoryMaster : MonoBehaviour
{
    public GameObject playerCam;
    public bool inObservatoryView;
    public bool inSolarSystemView;
    public GameObject planetInfoPanel;
    public GameObject minimap;
    public GameObject welcomeTutorial;

    public AudioSource telescope;
    public AudioSource main;
    public List<Transform> observatoryPoints;
    public List<bool> unlockedConstellations;
    public List<ObservatoryPlanetSpot> currentChapterSpots;
    public float lerpTime;
    public int observatoryCamTransformIndex;
    public bool lerping;
    private Vector3 lerpTarget;
    private Vector3 lerpOrigin;
    private float timeSpentLerping;

    public Button leftButton;
    public Button rightButton;
    public Button exitButton;
    public Button exitSolarSystemButton;

    public GameObject basePlant;
    public bool initted;

    public int currentChapterIndex;
    public float zPos = -80f;

    public enum Direction
    {
        Left,
        Right
    }
    public void EnterObservatory()
    {
        AlmanacProgression.instance.Unlock("ObservatoryEnter");

        if (!inObservatoryView)
        {
            playerCam.GetComponent<Cinemachine.CinemachineFreeLook>().enabled = false;
            inObservatoryView = true;
            TutorialManagerScript.instance.Unlock("The Telescope");
            main.Stop();
            telescope.Play();
            foreach (Transform observatory in observatoryPoints)
            {
                observatory.gameObject.SetActive(true);
            }
            //main.mute = true;
        }
        else
        {
            playerCam.GetComponent<Cinemachine.CinemachineFreeLook>().enabled = true;
            inObservatoryView = false;
            telescope.Stop();
            main.Play();
            foreach (Transform observatory in observatoryPoints)
            {
                observatory.gameObject.SetActive(false);
            }
            //main.mute = false;
        }
    }

    public void ExitSolarSystemView()
    {
        inSolarSystemView = false;
        GameObject.FindGameObjectWithTag("SolarSystemCam").SetActive(false);
    }

    public void UpdateToDoUI()
    {
        for(int i = 0; i < unlockedConstellations.Count; i++)
        {
            if (unlockedConstellations[i])
            {
                currentChapterIndex = i;
            }
            else
                break;
        }
        observatoryPoints[currentChapterIndex].GetComponentInChildren<SolarSystemInfo>().UpdateRequirementsPanel();
    }

    public List<ObservatoryPlanetSpot> GetPlanetSpotsOfCurrentChapter()
    {
        List<ObservatoryPlanetSpot> spots = new List<ObservatoryPlanetSpot>();

        spots.AddRange(observatoryPoints[GetCurrentChapter()-1].GetComponentsInChildren<ObservatoryPlanetSpot>());

        return spots;
    }

    public List<ObservatoryPlanetSpot> GetPlanetSpotsOfChapter(int chapter)
    {
        if (chapter == 0)
            chapter = 1;

        List <ObservatoryPlanetSpot> spots = new List<ObservatoryPlanetSpot>();

        spots.AddRange(observatoryPoints[chapter - 1].GetComponentsInChildren<ObservatoryPlanetSpot>());

        return spots;
    }

    public int GetCurrentChapter()
    {
        int chapter = 0;
        foreach(bool isComplete in unlockedConstellations)
        {
            if(isComplete)
            {
                chapter++;
            }
            else
            {
                break;
            }
        }
        return chapter;
    }

    public bool[] GetFilledPlanetsOfCurrentChapter()
    {
        List<bool> result = new List<bool>();

        List<ObservatoryPlanetSpot> spots = GetPlanetSpotsOfCurrentChapter();

        for(int i = 0; i < spots.Count; i++)
        {
            result.Add(spots[i].filled);
        }

        return result.ToArray();
    }



    public void LoadFilledSpots(bool[] filledSpots, int currentChapter)
    {
        
        currentChapterSpots = GetPlanetSpotsOfChapter(currentChapter);
        FillChapterSpots(currentChapter - 1); //fill all previous chapters
        
        


        if(filledSpots.Length != currentChapterSpots.Count)
        {
            Debug.Log("Given array != current chapter count");
            return;
        }

        for(int i = 0; i < filledSpots.Length; i++)
        {
            if (!filledSpots[i])
                continue; //check the next spot

            GameObject temp = Instantiate(basePlant);
            temp.GetComponent<Plant>().type = currentChapterSpots[i].type;
            temp.GetComponent<Plant>().species = currentChapterSpots[i].species;

            currentChapterSpots[i].PlaceObject(temp, false);
        }
        for(int i = 0; i < currentChapter; i++)
        {
            unlockedConstellations[i] = true;
        }
    }

    public void FillChapterSpots(int chaptersToFill)
    {
        for (int i = 0; i < chaptersToFill; i++)
        {
            //fill up each chapter's spots
            List<ObservatoryPlanetSpot> spots = GetPlanetSpotsOfChapter(i + 1);
            foreach (ObservatoryPlanetSpot spot in spots)
            {
                GameObject temp = Instantiate(basePlant);
                spot.transform.parent.parent.GetComponent<Observatory>().LoadComplete();

                temp.GetComponent<Plant>().type = spot.type;
                temp.GetComponent<Plant>().species = spot.species;

                spot.PlaceObject(temp, false);          
            }
        }
    }

    public void UnlockConstellation(int index)
    {
        unlockedConstellations[index] = true;
    }

    public void ChangeTargetObservatory(int dir)
    {
        if(dir == 1)
        {
            StartLerp(observatoryPoints[observatoryCamTransformIndex].localPosition, observatoryPoints[observatoryCamTransformIndex + 1].localPosition);
            observatoryCamTransformIndex++;
        }   
        else
        {
            
            StartLerp(observatoryPoints[observatoryCamTransformIndex].localPosition, observatoryPoints[observatoryCamTransformIndex - 1].localPosition);
            observatoryCamTransformIndex--;
        }
    }

    void ChangeConstellationButtonStatus()
    {
        if(observatoryCamTransformIndex - 1 < 0 || lerping)
        {

            leftButton.interactable = false;

        }
        else
        {
            leftButton.interactable = true;

        }

        if(observatoryCamTransformIndex + 1 >= observatoryPoints.Count || lerping)
        {
            rightButton.interactable = false;

        }
        else
        {
            if(unlockedConstellations[observatoryCamTransformIndex + 1])
                rightButton.interactable = true;
            else
                rightButton.interactable = false;

        }
    }

    void StartLerp(Vector3 origin, Vector3 target)
    {
        lerpOrigin = origin;
        lerpTarget = target;
        lerping = true;
    }

    void LerpUpdate()
    {
        if(lerping)
        {
            float t = Mathf.SmoothStep(0, 1, timeSpentLerping/lerpTime);
            Vector3 lerpedPos = Vector3.Lerp(lerpOrigin, lerpTarget, t);
            transform.localPosition = new Vector3(lerpedPos.x, lerpedPos.y, zPos);
            timeSpentLerping += Time.deltaTime;
            if(timeSpentLerping>= lerpTime)
            {
                timeSpentLerping = 0;
                transform.localPosition = new Vector3(lerpTarget.x, lerpTarget.y, zPos);
                lerping = false;
            }
        }

    }

    void SetButtonVisibility()
    {
        planetInfoPanel.gameObject.SetActive(inSolarSystemView);
        leftButton.gameObject.SetActive(!inSolarSystemView);
        rightButton.gameObject.SetActive(!inSolarSystemView);
        minimap.gameObject.SetActive(!inObservatoryView);
        exitSolarSystemButton.gameObject.SetActive(inSolarSystemView);
        exitButton.gameObject.SetActive(!inSolarSystemView);

        try
        {
            welcomeTutorial.gameObject.SetActive(!inObservatoryView);

        }
        catch (Exception e)
        {

        }
    }

    // Start is called before the first frame update
    void Start()
    {

        foreach (Transform observatory in observatoryPoints)
        {
            observatory.gameObject.SetActive(false);
        }
        initted = true;
        minimap = GameObject.FindGameObjectWithTag("Minimap");
        welcomeTutorial = GameObject.Find("WelcomeTutorial");
        playerCam = GameObject.Find("PlayerVCam");
    }

    // Update is called once per frame
    void Update()
    {
        LerpUpdate();
        ChangeConstellationButtonStatus();
        UpdateToDoUI();
        SetButtonVisibility();
    }
}
