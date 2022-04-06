using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManagerScript : MonoBehaviour
{
    public static TutorialManagerScript instance;
    public GameObject tutorialCanvas;
    public GameObject comicCanvas;
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
        //Unlock("Welcome to Planetary Planter");
    }

    // Update is called once per frame
    void Update()
    {
        CheckActive();
    }

    void CheckActive()
    {
        if(tutorialCanvas.activeInHierarchy || comicCanvas.activeInHierarchy)
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

    public string[] GetAllCompletedTutorials()
    {
        List<string> completedTutorials = new List<string>();
        foreach(Tutorial t in tutorials)
        {
            if(t.isUnlocked)
            {
                completedTutorials.Add(t.title);
            }
        }
        return completedTutorials.ToArray();
    }

    public void LoadCompletedTutorials(string[] completedTutorials)
    {
        foreach (string key in completedTutorials)
        {
            if (FindTutorialEntry(key) != null)
            {
                FindTutorialEntry(key).isUnlocked = true;
                Debug.Log("Unlocked " + key);
            }
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
