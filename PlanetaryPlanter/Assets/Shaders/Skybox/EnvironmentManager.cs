using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private float LerpTime;
    private float invTransitionTime;
    private Material skybox;
    private SkyboxParams[] Environments;

    public enum EnvironmentType
    {
        Cold = 0, Temperate, Hot
    }

    private EnvironmentType Current;
    private EnvironmentType Target;

    private void Start()
    {
        skybox = RenderSettings.skybox;
        if(skybox == null) //This is to allow additive scene loading to still get the skybox
        {
            skybox = Camera.main.gameObject.GetComponent<Skybox>().material;
        }
        Current = EnvironmentType.Temperate;
        Environments = new SkyboxParams[] {ColdParams, TemperateParams, HotParams};
        SetParameters(Environments[((int)Current)]);
        if (SystemInfo.supportsAsyncGPUReadback)
                {
                    DynamicGI.UpdateEnvironment();
                }
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
                Current = Target;
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
