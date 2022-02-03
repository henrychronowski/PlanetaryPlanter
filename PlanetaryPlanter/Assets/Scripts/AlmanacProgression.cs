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
    // Start is called before the first frame update
    void Start()
    {
        achievements.AddRange(GetComponents<Achievement>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}