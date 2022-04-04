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

    public string ghostModName;
    public string ghostModInfo;

    public string fossilModName;
    public string fossilModInfo;

    public string waterModName;
    public string waterModInfo;

    public string grassModName;
    public string grassModInfo;

    public string rottenName;
    public string rottenInfo;

    public bool isPlant = false;

    void CheckForMods()
    {
        if (isPlant)
        {
            if(GetComponent<Plant>().stage == Plant.Stage.Rotten)
            {
                name = rottenName;
                otherInfo = rottenInfo;
                return;
            }
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

            if (GetComponent<Plant>().type == PlanetType.MortalCoil)
            {
                name = ghostModName;
                otherInfo = ghostModInfo;
            }

            if (GetComponent<Plant>().type == PlanetType.Fossilium)
            {
                name = fossilModName;
                otherInfo = fossilModInfo;
            }

            if (GetComponent<Plant>().type == PlanetType.DewDrop)
            {
                name = waterModName;
                otherInfo = waterModInfo;
            }

            if (GetComponent<Plant>().type == PlanetType.Sprout)
            {
                name = grassModName;
                otherInfo = grassModInfo;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckForMods();
    }
}
