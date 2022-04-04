using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlmanacEntryScript : MonoBehaviour
{
    public Text entryOutput;
    public Button button;
    public GameObject currentMenu;
    public GameObject entry;
    public GameObject achievementTracker;
    public string entryName;
    public string achievementName;

    public bool isActivated;

    // Start is called before the first frame update
    void Start()
    {
        isActivated = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckTrue();
    }

    void CheckTrue()
    {
        isActivated = achievementTracker.GetComponent<AlmanacProgression>().IsUnlocked(achievementName);
        if (!isActivated)
        {
            entryOutput.text = "???";
            //button.gameObject.SetActive(false);
            button.interactable = false;
        }
        else
        {
            entryOutput.text = entryName;
            //button.gameObject.SetActive(true);
            button.interactable = true;
        }
    }
}
