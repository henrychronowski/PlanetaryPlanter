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

    void Unlock() //0 = Chapter 1, 1 = Chapter 2, etc
    {
        switch (currentChapter)
        {
            case 1:
                {
                    //set all bouncy plants to active
                    GameObject[] bouncePads = GameObject.FindGameObjectsWithTag("BouncePad");
                    foreach(GameObject bouncePad in bouncePads)
                    {
                        bouncePad.SetActive(true);
                    }
                    TutorialManagerScript.instance.Unlock("BouncePad");
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
                    TutorialManagerScript.instance.Unlock("NewSilo1");

                    break;
                }
            case 4:
                {
                    //new planters
                    planterSet1.SetActive(true);
                    TutorialManagerScript.instance.Unlock("NewPlanters1");
                    break;
                }
            case 5:
                {
                    silo2.SetActive(true);
                    TutorialManagerScript.instance.Unlock("NewSilo2");
                    break;
                }
            case 6:
                {
                    planterSet2.SetActive(true);
                    TutorialManagerScript.instance.Unlock("NewPlanters2");
                    break;
                }
            case 7:
                {
                    GameObject.FindObjectOfType<AttackHitbox>().canBreakRocks = true;
                    TutorialManagerScript.instance.Unlock("ShovelUpgrade");

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
