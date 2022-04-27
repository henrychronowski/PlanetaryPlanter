using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[CreateAssetMenu(menuName = "Rendering/Universal Render Pipeline/Custom URP (Pipeline Asset)")]
public class CustomURP : UniversalRenderPipelineAsset {
    public Shader detailGrassBillboardShader;
    public override Shader terrainDetailGrassBillboardShader => detailGrassBillboardShader;
}