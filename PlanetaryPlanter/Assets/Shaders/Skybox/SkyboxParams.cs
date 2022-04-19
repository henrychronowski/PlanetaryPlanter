using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Skybox Parameters", menuName = "Skybox/Skybox Parameters", order =1)]
public class SkyboxParams : ScriptableObject
{
    public string SkyName;

    public Color SkyColor;
    public Color HorizonColor;
    //public float StarDensity = 4.0f;
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
    //public Texture2D CloudTexture;
    public float CloudHeight;
    public float CloudEdge;
    public Color CloudColor;
    //[Range(-0.1f, 0.1f)]
    //public float SkyRotationSpeed = 0.01f;
    [Range(0f, 1f)]
    public float HorizonOffset = 0f;

    static public SkyboxParams Lerp(SkyboxParams from, SkyboxParams to, float interp)
	{
        SkyboxParams result = SkyboxParams.CreateInstance<SkyboxParams>();

        result.SkyColor = Color.Lerp(from.SkyColor, to.SkyColor, interp);
        result.HorizonColor = Color.Lerp(from.HorizonColor, to.HorizonColor, interp);
        result.StarSize = Mathf.Lerp(from.StarSize, to.StarSize, interp);
        result.SunColor = Color.Lerp(from.SunColor, to.SunColor, interp);
        result.SunSize = Mathf.Lerp(from.SunSize, to.SunSize, interp);
        result.SunBlend = Mathf.Lerp(from.SunBlend, to.SunBlend, interp);
        result.SunSpotColor = Color.Lerp(from.SunSpotColor, to.SunSpotColor, interp);
        result.SunSpotOpacity = Mathf.Lerp(from.SunSpotOpacity, to.SunSpotOpacity, interp);
        result.WindSpeed = Mathf.Lerp(from.WindSpeed, to.WindSpeed, interp);
        result.CloudHeight = Mathf.Lerp(from.CloudHeight, to.CloudHeight, interp);
        result.CloudEdge = Mathf.Lerp(from.CloudEdge, to.CloudEdge, interp);
        result.CloudColor = Color.Lerp(from.CloudColor, to.CloudColor, interp);
        result.HorizonOffset = Mathf.Lerp(from.HorizonOffset, to.HorizonOffset, interp);
        
        return result;
	}
}
