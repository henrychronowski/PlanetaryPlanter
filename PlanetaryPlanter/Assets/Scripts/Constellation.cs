using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constellation : MonoBehaviour
{
    List<Observatory> observatoryList;
    public int chapter;
    public bool completed;
    public string completionAchievement;
    
    // Start is called before the first frame update
    void Start()
    {
        observatoryList = new List<Observatory>();
        observatoryList.AddRange(GetComponentsInChildren<Observatory>());
    }

    public bool CheckCompletion()
    {
        foreach(Observatory ob in observatoryList)
        {
            if(!ob.completed)
            {
                return false;
            }

        }
        AlmanacProgression.instance.Unlock("Constellation" + chapter.ToString());
        Debug.Log("Constellation" + chapter.ToString() + " Complete");
        GameObject.FindObjectOfType<ObservatoryMaster>().UnlockConstellation(chapter); // unlocks the next chapter
        TutorialManagerScript.instance.Unlock(completionAchievement);
        AlmanacProgression.instance.Unlock("Constellation" + chapter + "Done");
        completed = true;
        return true;
    }

    // Update is called once per frame
    void Update()
    {
        if(!completed)
            CheckCompletion();
    }
}

