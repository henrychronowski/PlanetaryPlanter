using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipInfo : MonoBehaviour
{
    public string name;
    public string itemType;
    public string otherInfo;

    public string fireModName;
    public string fireModInfo;

    public string iceModName;
    public string iceModInfo;

    public bool isPlant = false;

    void CheckForMods()
    {
        if (isPlant)
        {

            if (GetComponent<PlantTool>().modifier == ModifierTypes.VolcanicAsh)
            {
                name = fireModName;
                otherInfo = fireModInfo;
            }

            if (GetComponent<PlantTool>().modifier == ModifierTypes.FrozenCore)
            {
                name = iceModName;
                otherInfo = iceModInfo;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckForMods();
    }
}
