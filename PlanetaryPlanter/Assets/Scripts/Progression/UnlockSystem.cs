using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockSystem : MonoBehaviour
{
    public ObservatoryMaster observatoryMaster;
    public int currentChapter;
    public GameObject planterSet1;
    public GameObject planterSet2;
    public GameObject silo1;
    public GameObject silo2;
    public GameObject bounceblooms;
    public GameObject goldSquimbus;
    public ActManagerScript actManager;

    public bool seniorShowUnlocks;
    // Start is called before the first frame update
    void Start()
    {
        //actManager = gameObject.GetComponent<ActManagerScript>();
        actManager = FindObjectOfType<ActManagerScript>();
        observatoryMaster = GameObject.FindObjectOfType<ObservatoryMaster>();

    }

    void CheckForNewChapter()
    {
        if(currentChapter < observatoryMaster.currentChapterIndex)
        {
            currentChapter++;
            Unlock();
        }
    }

    public void UnlockChapters(int chaptersToUnlock)
    {
        while (currentChapter < chaptersToUnlock)
        {
            currentChapter++;
            Unlock(false);
            observatoryMaster.unlockedConstellations[currentChapter] = true;
        }
    }

    void Unlock(bool showTutorial = true) //0 = Chapter 1, 1 = Chapter 2, etc
    {
        if(seniorShowUnlocks)
        {
            SeniorShowUnlock(showTutorial);
            return;
        }

        switch (currentChapter)
        {
            case 1:
                {
                    //new silo
                    silo1.SetActive(true);
                    if (showTutorial) TutorialManagerScript.instance.Unlock("New Silo");
                    
                    break;
                }
            case 2:
                {
                    //new planters
                    planterSet1.SetActive(true);
                    if (showTutorial) TutorialManagerScript.instance.Unlock("New Planters");
                    actManager.Unlock("The Prologue");
                    break;
                }
            case 3:
                {
                    //set all bouncy plants to active
                    bounceblooms.SetActive(true);

                    if (showTutorial) TutorialManagerScript.instance.Unlock("Bouncebloom");
                    break;
                }
            case 4:
                {
                    planterSet2.SetActive(true);
                    if (showTutorial) TutorialManagerScript.instance.Unlock("New Planters 2");
                    
                    break;
                }
            case 5:
                {
                    silo2.SetActive(true);
                    if (showTutorial) TutorialManagerScript.instance.Unlock("New Silo Again");
                    if (showTutorial) actManager.Unlock("Act 1");
                    break;
                }
            case 6:
                {
                    GameObject.FindObjectOfType<AttackHitbox>().canBreakRocks = true;
                    if (showTutorial) TutorialManagerScript.instance.Unlock("Stallanovium Trowel");
                    
                    break;
                }
            case 7:
                {
                    GameObject.FindObjectOfType<CharacterMovement>().canGlide = true;
                    if (showTutorial) TutorialManagerScript.instance.Unlock("Melmee");
                    if (showTutorial) actManager.Unlock("Act 2");

                    //glider
                    break;
                }
            case 8:
                {
                    break;
                }
            case 9:
                {
                    
                    break;
                }
            case 10:
                {
                    goldSquimbus.SetActive(true);
                    if (showTutorial) actManager.Unlock("Act 3");
                    break;
                }
        }
    }

    void SeniorShowUnlock(bool showTutorial = true) //0 = Chapter 1, 1 = Chapter 2, etc
    {
        switch (currentChapter)
        {
            case 1:
                {
                    //set all bouncy plants to active
                    bounceblooms.SetActive(true);
                    if(showTutorial) TutorialManagerScript.instance.Unlock("Bounceblooms!");
                    
                    break;
                }
            case 2:
                {
                    GameObject.FindObjectOfType<AttackHitbox>().canBreakRocks = true;
                    if (showTutorial) TutorialManagerScript.instance.Unlock("Stallanovium Trowel");
                    if (showTutorial) ActManagerScript.instance.Unlock("The Prologue");
                    break;
                }
            case 3:
                {
                    //glider
                    GameObject.FindObjectOfType<CharacterMovement>().canGlide = true;
                    if (showTutorial) TutorialManagerScript.instance.Unlock("Melmee!");
                    if (showTutorial) TutorialManagerScript.instance.Unlock("Demo Over!");
                    goldSquimbus.SetActive(true);

                    break;
                }
            case 4:
                {
                    //new silo
                    silo1.SetActive(true);
                    if (showTutorial) TutorialManagerScript.instance.Unlock("New Silo");
                    
                    break;
                }
            case 5:
                {
                    //new planters
                    planterSet1.SetActive(true);
                    if (showTutorial) TutorialManagerScript.instance.Unlock("New Planters");
                    if (showTutorial) actManager.Unlock("Act 1");
                    break;
                }
            case 6:
                {
                    silo2.SetActive(true);
                    if (showTutorial) TutorialManagerScript.instance.Unlock("New Silo Again");
                    
                    break;
                }
            case 7:
                {
                    planterSet2.SetActive(true);
                    if (showTutorial) TutorialManagerScript.instance.Unlock("New Planters 2");
                    if (showTutorial) actManager.Unlock("Act 2");
                    break;
                }
            case 8:
                {
                    break;
                }
            case 9:
                {
                    
                    break;
                }
            case 10:
                {
                    goldSquimbus.SetActive(true);
                    if (showTutorial) TutorialManagerScript.instance.Unlock("Credits");
                    if (showTutorial) actManager.Unlock("Act 3");
                    break;
                }
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckForNewChapter();
    }
}
