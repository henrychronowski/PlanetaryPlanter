using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SolarSystemCountScript : MonoBehaviour
{
    public int numSolarSystemsComplete;
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
        if(numSolarSystemsComplete == 77)
        {
            SceneManager.LoadScene(2);
        }
    }
}
