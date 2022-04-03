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
        switch (currentChapter)
        {
            case 1:
                {
                    //set all bouncy plants to active
                    bounceblooms.SetActive(true);

                    TutorialManagerScript.instance.Unlock("Bouncebloom");
                    break;
                }
            case 2:
                {
                    GameObject.FindObjectOfType<CharacterMovement>().canGlide = true;
                    TutorialManagerScript.instance.Unlock("Melmee");

                    //glider
                    break;
                }
            case 3:
                {
                    //new silo
                    silo1.SetActive(true);
                    TutorialManagerScript.instance.Unlock("New Silo");

                    break;
                }
            case 4:
                {
                    //new planters
                    planterSet1.SetActive(true);
                    TutorialManagerScript.instance.Unlock("New Planters");
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
                    planterSet2.SetActive(true);
                    TutorialManagerScript.instance.Unlock("New Planters 2");
                    break;
                }
            case 7:
                {
                    GameObject.FindObjectOfType<AttackHitbox>().canBreakRocks = true;
                    TutorialManagerScript.instance.Unlock("Stallanovium Trowel");

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
