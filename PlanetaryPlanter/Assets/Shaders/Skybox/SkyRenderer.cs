using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

class NewSkyRenderer : SkyRenderer
{
	public static readonly int _Cubemap = Shader.PropertyToID("_Cubemap");
	public static readonly int _SkyParam = Shader.PropertyToID("_SkyParam");
	public static readonly int _PixelCoordToViewDirWS = Shader.PropertyToID("_PixelCoordToViewDirWS");

	Material m_NewSkyMaterial;
	MaterialPropertyBlock m_PropertyBlock = new MaterialPropertyBlock();

	private static int m_RenderCubemapID = 0;
	private static int m_RenderFullscreenSkyID = 1;

	public override void Build()
	{
		m_NewSkyMaterial = CoreUtils.CreateEngineMaterial(GetNewSkyShader());
	}

	Shader GetNewSkyShader()
	{
		return Shader.Find("Hidden/HDRP/Sky/SkyShader");//SkyShader");
	}

	public override void Cleanup()
	{
		CoreUtils.Destroy(m_NewSkyMaterial);
	}

	protected override bool Update(BuiltinSkyParameters builtinParams)
	{
		return base.Update(builtinParams);
	}

	public override void RenderSky(BuiltinSkyParameters builtinParams, bool renderForCubemap, bool renderSunDisk)
	{
		using (new ProfilingSample(builtinParams.commandBuffer, "Draw sky"))
		{
			var newSky = builtinParams.skySettings as NewSky;

			int passID = renderForCubemap ? m_RenderCubemapID : m_RenderFullscreenSkyID;
			
			float intensity = GetSkyIntensity(newSky, builtinParams.debugSettings);
			float phi = -Mathf.Deg2Rad * newSky.rotation.value;
			m_PropertyBlock.SetTexture(_Cubemap, newSky.hdriSky.value);
			m_PropertyBlock.SetVector(_SkyParam, new Vector4(intensity, 0.0f, Mathf.Cos(phi), Mathf.Sin(phi)));
			m_PropertyBlock.SetMatrix(_PixelCoordToViewDirWS, builtinParams.pixelCoordToViewDirMatrix);
			CoreUtils.DrawFullScreen(builtinParams.commandBuffer, m_NewSkyMaterial, m_PropertyBlock, passID);
		}
	}
}