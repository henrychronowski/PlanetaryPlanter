using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leavingHomePopup : MonoBehaviour
{
    bool tutorialunlocked = false;
    public string popupMessage;
    public Sprite popupSprite;

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "homeleaving")
        {
            if (tutorialunlocked == false)
            {
                //TutorialManagerScript.instance.Unlock("A Sense of Direction.");
                tutorialunlocked = true;
                PopUpController.instance.NewPopUp(popupMessage, popupSprite);
            }
        }
    }
}
