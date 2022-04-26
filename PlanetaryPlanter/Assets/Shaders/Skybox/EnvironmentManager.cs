using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    public static EnvironmentManager Instance { get; private set;}

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    [SerializeField] private SkyboxParams ColdParams;
    [SerializeField] private SkyboxParams TemperateParams;
    [SerializeField] private SkyboxParams HotParams;

    [SerializeField] private float TransitionTime = 1f;

    private bool IsLerping;
    private bool CurrentSky; // 0 = lhs, 1 = rhs. Starts as lhs always
    private bool TargetSky;
    private float LerpTime;
    private float invTransitionTime;
    private Material skybox;
    private bool toggle; // Toggle to prevent trigger being called twice on same instance
    private SkyboxParams[] Environments;

    public enum EnvironmentType
    {
        Cold = 0, Temperate, Hot
    }

    private EnvironmentType Current;
    private EnvironmentType Target;

    private void start()
    {
        Current = EnvironmentType.Cold;
        Environments = new SkyboxParams[] {ColdParams, TemperateParams, HotParams};
        SetParameters(Environments[((int)Current)]);
        skybox = RenderSettings.skybox;
        IsLerping = false;
        invTransitionTime = 1f/TransitionTime;
    }

    void Update()
    {
        if (IsLerping)
        {
            LerpTime += Time.deltaTime;
            float interpParam = LerpTime * invTransitionTime;

            if(interpParam < 1f)
			{
                SkyboxParams from, to;
                from = Environments[((int)Current)];
                to = Environments[((int)Target)];

                SetParameters(SkyboxParams.Lerp(from, to, interpParam));
                if (SystemInfo.supportsAsyncGPUReadback)
                {
                    DynamicGI.UpdateEnvironment();
                }
            }
            else
			{
                IsLerping = false;
                CurrentSky = TargetSky;
                if (SystemInfo.supportsAsyncGPUReadback)
                {
                    DynamicGI.UpdateEnvironment();
                }
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

    public void TransitionToHot()
    {
        Target = EnvironmentType.Hot;
        if(!IsLerping)
        {
            IsLerping = true;
            LerpTime = 0f;
        }
    }

    public void TransitionToTemperate()
    {
        Target = EnvironmentType.Temperate;
        if(!IsLerping)
        {
            IsLerping = true;
            LerpTime = 0f;
        }
    }

    public void TransitionToCold()
    {
        Target = EnvironmentType.Cold;
        if(!IsLerping)
        {
            IsLerping = true;
            LerpTime = 0f;
        }
    }
}
