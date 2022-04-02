using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class Tutorial : MonoBehaviour
{
    public bool isUnlocked;
    public string title;
    public string description;

    public GameObject tutorial;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;

    public class TutorialManagerScript : UnityEvent { }
    public TutorialManagerScript tutorialEvent = new TutorialManagerScript();
    public TutorialManagerScript onTutorialEvent { get { return tutorialEvent; } set { tutorialEvent = value; } }

    public void TutorialEventTriggered()
    {
        if(!isUnlocked)
        {
            isUnlocked = true;
            tutorial.SetActive(true);
            descriptionText.text = description;
            titleText.text = title;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
