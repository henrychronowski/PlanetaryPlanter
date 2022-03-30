using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class craftingTableFirst : MonoBehaviour
{

    bool tutorialUnlocked;
    public string tutorial = "Crafting Table";
    public KeyCode openCraft = KeyCode.C;

    public void unlock()
    {
        if (!tutorialUnlocked)
        {
            TutorialManagerScript.instance.Unlock(tutorial);
            tutorialUnlocked = true;

        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(openCraft))
        {
            unlock();
        }
    }

}
