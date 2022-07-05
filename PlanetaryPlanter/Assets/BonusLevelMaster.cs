using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Disables and re enables certain systems based on if the player is in a bonus level currently
public class BonusLevelMaster : MonoBehaviour
{
    public bool bonusLevelActive = false;
    
    public void Activate()
    {
        if(!bonusLevelActive)
        {
            bonusLevelActive = true;
            SaveManager.instance.canSave = false;
            SunRotationScript.instance.timeStopped = true;
        }
    }

    public void Deactivate()
    {
        if (bonusLevelActive)
        {
            bonusLevelActive = false;
            SaveManager.instance.canSave = true;
            SunRotationScript.instance.timeStopped = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(bonusLevelActive)
        {
            SaveManager.instance.canSave = false;
        }
        else
        {
            SaveManager.instance.canSave = true;
        }
    }
}
