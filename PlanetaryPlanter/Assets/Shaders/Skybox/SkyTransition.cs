using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyTransition : MonoBehaviour
{
    [SerializeField]
    private float TransitionTime = 1f;
    [SerializeField][Tooltip("Always starts on LHS")]
    private SkyboxParams lhs;
    [SerializeField][Tooltip("Always starts on LHS")]
    private SkyboxParams rhs;

    private bool IsLerping;
    private bool CurrentSky; // 0 = lhs, 1 = rhs. Starts as lhs always
    private bool TargetSky;
    private float LerpTime;
    private float invTransitionTime;
    private Material skybox;
    
    void Start()
    {
        IsLerping = false;
        CurrentSky = false;
        TargetSky = CurrentSky;
        invTransitionTime = 1f / TransitionTime;
        skybox = RenderSettings.skybox;
    }

    void Update()
    {
        if (IsLerping)
        {
            Debug.Log("Is Lerping");
            LerpTime += Time.deltaTime;
            float interpParam = LerpTime * invTransitionTime;

            if(interpParam < 1f)
			{
                SkyboxParams from, to;
                from = (CurrentSky) ? rhs : lhs;
                to = (TargetSky) ? rhs : lhs;

                SetParameters(SkyboxParams.Lerp(from, to, interpParam));
            }
            else
			{
                IsLerping = false;
			}
        }
    }

    private void SetParameters(SkyboxParams input)
	{
        //RenderSettings.skybox.SetFloat("StarDensity", tmpTest);
        skybox.SetColor("SkyColor", input.SkyColor);
        skybox.SetColor("HorizonColor", input.HorizonColor);
        skybox.SetFloat("StarSize", input.StarSize);
        skybox.SetColor("SunColor", input.SunColor);
        skybox.SetFloat("SunSize", input.SunSize);
        skybox.SetFloat("SunBlend", input.SunBlend);
        skybox.SetColor("SunSpotColor", input.SunSpotColor);
        skybox.SetFloat("SunSpotOpacity", input.SunSpotOpacity);
        skybox.SetFloat("WindSpeed", input.WindSpeed);
        skybox.SetFloat("CloudHeight", input.CloudHeight);
        skybox.SetFloat("CloudEdge", input.CloudEdge);
        skybox.SetColor("CloudColor", input.CloudColor);
        skybox.SetFloat("HorizonOffset", input.HorizonOffset);
    }

    private void OnTriggerEnter(Collider other)
    {
        IsLerping = true;
        TargetSky = !CurrentSky;
        LerpTime = 0f;
    }
}
