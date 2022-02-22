using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

//public abstract class SkyRenderer
//{
//	int m_LastFrameUpdate = -1;

//	/// <summary>
//	/// Called on startup. Create resources used by the renderer (shaders, mats, etc)
//	/// </summary>
//	public abstract void Build();

//	/// <summary>
//	/// Called on cleanup. Release resources used by the renderer
//	/// </summary>
//	public abstract void Cleanup();

//	/// <summary>
//	/// HDRP calls this each frame. Implement if the renderer needs to iterate independently of the user defined update frequency (See skysettings UpdateMode)
//	/// </summary>
//	/// <param name="builtinParams"></param>
//	/// <returns>True if the update determines that the sky lighting needs to be re-rendererd. False otherwise</returns>
//	protected virtual bool Update(BuiltinSkyParameters builtinParams) { return false; }

//	/// <summary>
//	/// Implements actual rendering of the sky. HDRP calls this when rendering the sky into a cubemap (for lighting) and also during main frame rendering.
//	/// </summary>
//	/// <param name="builtinParams">Engine parameters that you can use to render the sky.</param>
//	/// <param name="renderForCubemap">Pass in true if you want to render the sky into a cubemap for lighting. This is useful when the sky renderer needs a different implementation in this case.</param>
//	/// <param name="renderSunDisk">If the sky renderer supports the rendering of a sun disk, it must not render it if this is set to false.</param>
//	public abstract void RenderSky(BuiltinSkyParameters builtinParams, bool renderForCubemap, bool renderSunDisk);
//}

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
		return null;
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