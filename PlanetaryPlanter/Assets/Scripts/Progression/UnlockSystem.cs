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

    public bool seniorShowUnlocks;
    // Start is called before the first frame update
    void Start()
    {
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
            observatoryMaster.unlockedConstellations[currentChapter] = true;
            Unlock();
        }
    }

    void Unlock() //0 = Chapter 1, 1 = Chapter 2, etc
    {
        if(seniorShowUnlocks)
        {
            SeniorShowUnlock();
            return;
        }

        switch (currentChapter)
        {
            case 1:
                {
                    //new silo
                    silo1.SetActive(true);
                    TutorialManagerScript.instance.Unlock("New Silo");

                    break;
                }
            case 2:
                {
                    //new planters
                    planterSet1.SetActive(true);
                    TutorialManagerScript.instance.Unlock("New Planters");
                    break;
                }
            case 3:
                {
                    //set all bouncy plants to active
                    bounceblooms.SetActive(true);

                    TutorialManagerScript.instance.Unlock("Bouncebloom");
                    break;
                }
            case 4:
                {
                    planterSet2.SetActive(true);
                    TutorialManagerScript.instance.Unlock("New Planters 2");
                    break;
                }
            case 5:
                {
                    silo2.SetActive(true);
                    TutorialManagerScript.instance.Unlock("New Silo Again");
                    break;
                }
            case 6:
                {
                    GameObject.FindObjectOfType<AttackHitbox>().canBreakRocks = true;
                    TutorialManagerScript.instance.Unlock("Stallanovium Trowel");

                    break;
                }
            case 7:
                {
                    GameObject.FindObjectOfType<CharacterMovement>().canGlide = true;
                    TutorialManagerScript.instance.Unlock("Melmee");

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
        }
    }

    void SeniorShowUnlock() //0 = Chapter 1, 1 = Chapter 2, etc
    {
        switch (currentChapter)
        {
            case 1:
                {
                    //set all bouncy plants to active
                    bounceblooms.SetActive(true);
                    TutorialManagerScript.instance.Unlock("Bounceblooms!");
                    break;
                }
            case 2:
                {
                    GameObject.FindObjectOfType<AttackHitbox>().canBreakRocks = true;
                    TutorialManagerScript.instance.Unlock("Stallanovium Trowel");
                    break;
                }
            case 3:
                {
                    //glider
                    GameObject.FindObjectOfType<CharacterMovement>().canGlide = true;
                    TutorialManagerScript.instance.Unlock("Melmee!");
                    break;
                }
            case 4:
                {
                    //new silo
                    silo1.SetActive(true);
                    TutorialManagerScript.instance.Unlock("New Silo");
                    break;
                }
            case 5:
                {
                    //new planters
                    planterSet1.SetActive(true);
                    TutorialManagerScript.instance.Unlock("New Planters");
                    break;
                }
            case 6:
                {
                    silo2.SetActive(true);
                    TutorialManagerScript.instance.Unlock("New Silo Again");
                    break;
                }
            case 7:
                {
                    planterSet2.SetActive(true);
                    TutorialManagerScript.instance.Unlock("New Planters 2");
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
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckForNewChapter();
    }
}
