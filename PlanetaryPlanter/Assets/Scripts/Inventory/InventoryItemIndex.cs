using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This class is used for containing all items in the game on a separate game object

[System.Serializable]
public enum ItemID
{
    Unidentified, //0
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
    Fertilizer, //15
    FossilModifier,
    GrassModifier,
    WaterModifier,
    GhostModifier,
    RockyPlanetPlant, //20
    FireRockyPlanet,
    IceRockyPlanet,
    GhostRockyPlanet,
    WaterRockyPlanet,
    GrassRockyPlanet,
    FossilRockyPlanet,
    CometPlant,
    FireCometPlanet,
    IceCometPlanet,
    GhostCometPlanet,
    WaterCometPlanet,
    GrassCometPlanet,
    FossilCometPlanet,//33
    GhostAsteroidPlant,
    WaterAsteroidPlant,
    FossilAsteroidPlant,
    GrassAsteroidPlant,
    GhostPlanetPlant,//38
    WaterPlanetPlant,
    FossilPlanetPlant,
    GrassPlanetPlant,
    GhostStarPlant, //42
    WaterStarPlant,
    FossilStarPlant,
    GrassStarPlant,
    RottenAsteroid,
    RottenPlanet,
    RottenStar,
    RottenRockyPlanet,
    RottenComet,
    RockyPlanetSeed,
    CometSeed //52
}

public class InventoryItemIndex : MonoBehaviour
{
    //ALL items
    public List<GameObject> items;
}
