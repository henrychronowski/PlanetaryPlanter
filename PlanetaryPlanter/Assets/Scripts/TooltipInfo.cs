using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipInfo : MonoBehaviour
{
    public string name;
    public string itemType;
    public string otherInfo;

    //The name and tooltip to change to if this item gets modified with fire/ice
    public string fireModName;
    public string fireModInfo;

    public string iceModName;
    public string iceModInfo;

    public bool isPlant = false;
    public ItemID itemID;

    void CheckForMods()
    {
        if (isPlant)
        {

            if (GetComponent<Plant>().type == PlanetType.VolcanicAsh)
            {
                name = fireModName;
                otherInfo = fireModInfo;
            }

            if (GetComponent<Plant>().type == PlanetType.FrozenCore)
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
