using UnityEngine;
 using UnityEngine.Rendering;
 
 public class DisableCamCull : MonoBehaviour
 {
     private void Start()
     {
         RenderPipelineManager.beginCameraRendering += RenderPipelineManager_beginCameraRendering;
     }
 
     private void RenderPipelineManager_beginCameraRendering(ScriptableRenderContext context, Camera camera)
     {
         camera.cullingMatrix = Matrix4x4.Ortho(-99999, 99999, -99999, 99999, 0.001f, 99999) *
                             Matrix4x4.Translate(Vector3.forward * -99999 / 2f) *
                             camera.worldToCameraMatrix;
     }
 }
 