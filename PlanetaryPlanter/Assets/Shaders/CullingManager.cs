using UnityEngine;
using UnityEngine.Rendering;

public class CullingManager : MonoBehaviour
{
    #region MonoBehavior

    private void OnEnable ()
    {
        if(!Application.isPlaying)
        {
            return;
        }

        RenderPipelineManager.beginCameraRendering += OnBeginCameraRendering;
        RenderPipelineManager.endCameraRendering += OnEndCameraRendering;
    }

    private void OnDisable()
    {
        RenderPipelineManager.beginCameraRendering -= OnBeginCameraRendering;
        RenderPipelineManager.endCameraRendering -= OnEndCameraRendering;
    }

    #endregion

    #region Methods
    private static void OnBeginCameraRendering (ScriptableRenderContext ctx, Camera cam)
    {
        cam.cullingMatrix = Matrix4x4.Ortho(-399, 399, -250, 250, 0.001f, 599) * cam.worldToCameraMatrix;
    }

    private static void OnEndCameraRendering(ScriptableRenderContext ctx, Camera cam)
    {
        cam.ResetCullingMatrix();
    }

    #endregion
}