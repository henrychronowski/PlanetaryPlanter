using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SolarSystemCountScript : MonoBehaviour
{
    public int numSolarSystemsComplete;
    public int numSolarSystemsTotal = 77;
    // Start is called before the first frame update
    void Start()
    {
        numSolarSystemsComplete = 0;
        AudioSource[] audio = GameObject.FindObjectsOfType<AudioSource>();
        for (int i = 0; i < audio.Length; i++)
        {
            Debug.Log(audio[i].name);
        }
    }

    void Update()
    {
        CheckEndStatus();
    }

    void CheckEndStatus()
    {
        if(numSolarSystemsComplete == numSolarSystemsTotal)
        {
            TutorialManagerScript.instance.Unlock("Credits");
        }
        if (numSolarSystemsComplete == 75)
        {
            TutorialManagerScript.instance.Unlock("All Lore Unlocked!");
        }
    }
}
