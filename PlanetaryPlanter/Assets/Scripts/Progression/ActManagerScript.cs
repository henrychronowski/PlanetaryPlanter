using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActManagerScript : MonoBehaviour
{
    public static ActManagerScript instance;
    public GameObject actCanvas;
    public List<ActScript> acts;

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
        acts.AddRange(GetComponents<ActScript>());
    }

    // Update is called once per frame
    void Update()
    {
        CheckActive();
    }

    void CheckActive()
    {
        if(actCanvas.activeInHierarchy)
        {
            NewInventory.instance.SetSpacesActive(true);
            Time.timeScale = 0;
        }
    }

    public void Unlock(string actName)
    {
        ActScript act = FindActName(actName);

        if(act == null)
        {
            return;
        }

        if(!act.unlocked)
        {
            act.TriggerActEnd();
        }
    }

    public ActScript FindActName(string actName)
    {
        for(int i = 0; i < acts.Count; i++)
        {
            if(acts[i].actName == actName)
            {
                return acts[i];
            }
        }
        return null;
    }

    public bool isUnlocked(string actName)
    {
        return FindActName(actName);
    }

    public string[] GetAllCompletedActs()
    {
        List<string> completedActs = new List<string>();
        foreach (ActScript x in acts)
        {
            if (x.unlocked)
            {
                completedActs.Add(x.actName);
            }
        }
        return completedActs.ToArray();
    }

    public void LoadCompletedTutorials(string[] completedActs)
    {
        foreach (string key in completedActs)
        {
            if (FindActName(key) != null)
            {
                FindActName(key).unlocked = true;
                Debug.Log("Unlocked " + key + " via Load Game");
            }
        }
    }
}
