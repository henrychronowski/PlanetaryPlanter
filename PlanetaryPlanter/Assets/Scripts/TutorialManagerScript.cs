using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManagerScript : MonoBehaviour
{
    public static TutorialManagerScript instance;
    public GameObject tutorialCanvas;
    public List<Tutorial> tutorials;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        tutorials.AddRange(GetComponents<Tutorial>());
    }

    // Update is called once per frame
    void Update()
    {
        CheckActive();
        Unlock("Welcome to Planetary Planter");
    }

    void CheckActive()
    {
        if(tutorialCanvas.activeInHierarchy)
        {
            NewInventory.instance.SetSpacesActive(true);
            Time.timeScale = 0;
            //Time.fixedDeltaTime = 0.00f;

        }
        else
        {
            //NewInventory.instance.SetSpacesActive(false);
            //Time.timeScale = 1;
            //Time.fixedDeltaTime = 0.02f;
        }
    }

    public void Unlock(string str)
    {
        Tutorial tutorial = FindTutorialEntry(str);

        if(tutorial == null)
        {
            return;
        }

        if(!tutorial.isUnlocked)
        {
            tutorial.TutorialEventTriggered();
        }
    }

    public Tutorial FindTutorialEntry(string str)
    {
        for(int i = 0; i < tutorials.Count; i++)
        {
            if(tutorials[i].title == str)
            {
                return tutorials[i];
            }
        }
        return null;
    }

    public bool isUnlocked(string str)
    {
        return FindTutorialEntry(str);
    }
}
