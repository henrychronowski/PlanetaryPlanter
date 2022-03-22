using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This class is used for containing all items in the game on a separate game object

[System.Serializable]
public enum ItemID
{
    Unidentified,
    AsteroidSeed,
    PlanetSeed,
    StarSeed,
    AsteroidPlant,
    FireAsteroid,
    IceAsteroid,
    PlanetPlant,
    FirePlanet,
    IcePlanet,
    StarPlant,
    FireStar,
    IceStar,
    FireModifier,
    IceModifier,
    Fertilizer
}

public class InventoryItemIndex : MonoBehaviour
{
    //ALL items
    public List<GameObject> items;
}
