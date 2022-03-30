//using System;
//using UnityEngine;
//using UnityEngine.Rendering;
//using UnityEngine.Rendering.HighDefinition;

//[VolumeComponentMenu("Sky/New Sky")]
//[SkyUniqueID(NEW_SKY_UNIQUE_ID)]
//public class NewSky : SkySettings
//{
//	const int NEW_SKY_UNIQUE_ID = 20382390;

//	[Tooltip("Specify the cubemap HDRP uses to render the sky.")]
//	public CubemapParameter hdriSky = new CubemapParameter(null);

//	public override Type GetSkyRendererType()
//	{
//		return typeof(NewSkyRenderer);
//	}

//	public override int GetHashCode()
//	{
//		int hash = base.GetHashCode();
//		unchecked
//		{
//			hash = hdriSky.value != null ? hash * 23 + hdriSky.GetHashCode() : hash;
//		}
//		return hash;
//	}

//	public override int GetHashCode(Camera camera)
//	{
//		// Implement if it depends on the camera settings (like position)
//		return base.GetHashCode(camera);
//	}
//}