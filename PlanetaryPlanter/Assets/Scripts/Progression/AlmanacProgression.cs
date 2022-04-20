using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AlmanacEntryType
{
    Flora,


}



public class AlmanacProgression : MonoBehaviour
{
    public List<Achievement> achievements;

    public static AlmanacProgression instance;

    // Audio Manager Script is set up here
    private SoundManager soundManager;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    public void Unlock(string title)
    {
        Achievement ac = FindAchievementByTitle(title);
        if (ac == null)
            return;

        if (!ac.unlocked)
        {
            ac.InteractableEventTriggered();

            soundManager.PlaySound("GainedStory");
        }
        else
        {
            Debug.Log(title + " unlocked already");
        }
    }

    public bool IsUnlocked(string title)
    {
        return FindAchievementByTitle(title).unlocked;
    }

    public Achievement FindAchievementByTitle(string title)
    {
        for (int i = 0; i < achievements.Count; i++)
        { 
            if (achievements[i].title == title)
            {
                return achievements[i];
            }
        }
        return null;
    }

    public string[] GetAllCompletedAchievements()
    {
        List<string> completedAchievements = new List<string>();
        foreach (Achievement t in achievements)
        {
            if (t.unlocked)
            {
                completedAchievements.Add(t.title);
            }
        }
        return completedAchievements.ToArray();
    }

    public void LoadCompletedAchievements(string[] completedAchievements)
    {
        foreach (string key in completedAchievements)
        {
            if (FindAchievementByTitle(key) != null)
            {
                FindAchievementByTitle(key).unlocked = true;
                Debug.Log("Unlocked " + key + " via Load Game");
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        achievements.AddRange(GetComponents<Achievement>());
        //Audio Manager Is Opend Up here
        soundManager = SoundManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
