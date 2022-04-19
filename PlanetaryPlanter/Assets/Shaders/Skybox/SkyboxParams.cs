using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Skybox Parameters", menuName = "ScriptableObjects/Skybox", order =1)]
public class SkyboxParams : ScriptableObject
{
    public string SkyName;

    public Color SkyColor;
    public Color HorizonColor;
    public float StarDensity = 4.0f;
    public float StarSize;
    public Color SunColor;
    public float SunSize;
    public float SunBlend;
    public Color SunSpotColor;
    public float SunSpotOpacity;
    public float WindSpeed;
    public Texture2D CloudTexture;
    public float CloudHeight;
    public float CloudEdge;
    public Color CloudColor;
    public float SkyRotationSpeed;
    public float HorizonOffset;
}
