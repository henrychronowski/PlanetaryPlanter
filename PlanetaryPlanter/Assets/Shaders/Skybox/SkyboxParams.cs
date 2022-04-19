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
    [Range(50f, 150f)]
    public float StarSize = 100f;
    public Color SunColor;
    [Range(0f, 0.5f)]
    public float SunSize = 0.1f;
    [Range(0f,1f)]
    public float SunBlend = 0.2f;
    public Color SunSpotColor;
    [Range(0f, 1f)]
    public float SunSpotOpacity = 0.5f;
    [Range(0f, 0.1f)]
    public float WindSpeed = 0.05f;
    public Texture2D CloudTexture;
    public float CloudHeight;
    public float CloudEdge;
    public Color CloudColor;
    [Range(-0.1f, 0.1f)]
    public float SkyRotationSpeed = 0.01f;
    [Range(0f, 1f)]
    public float HorizonOffset = 0f;
}
