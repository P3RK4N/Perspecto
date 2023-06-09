using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.Events;

public class CameraStats : MonoBehaviour
{
    public TMP_InputField far; 
    public TMP_InputField near; 
    public TMP_InputField top; 
    public TMP_InputField bottom; 
    public TMP_InputField left; 
    public TMP_InputField right; 
    public TMP_InputField fov; 
    public TMP_Dropdown projection;

    public Transform ToCameraMat;
    public Transform ToClipMat;

    Camera cam;

    public int projType = 0;

    private void Awake() {
        cam = FindObjectOfType<Camera1>(true).GetComponent<Camera>();

        fov.text = cam.fieldOfView + "";
        near.text = cam.nearClipPlane + "";
        far.text = cam.farClipPlane + "";

        updateFromFOV();
    }

    public static Tuple<float, float, float, float> VerticalFovToFrustrumPlanes(float vfov, float aspectRatio, float near, float far)
    {
        float tanVfov = (float)Math.Tan(Math.PI / 180 * vfov / 2);
        float height = 2 * near * tanVfov;
        float width = height * aspectRatio;
        float left = -width / 2;
        float right = width / 2;
        float bottom = -height / 2;
        float top = height / 2;
        return Tuple.Create(left, right, top, bottom);
    }

    public static float FrustumPlanesToVerticalFov(float left, float right, float top, float bottom, float near, float far)
    {
        float height = top - bottom;
        float fovY = 2 * (float)Math.Atan(height / (2 * near));
        return (float)(fovY * 180 / Math.PI);
    }

    public void updateFromPlanes()
    {
        System.Func<string,float> p = (string num) => float.Parse(num);

        float newfov = FrustumPlanesToVerticalFov(p(left.text), p(right.text), p(top.text), p(bottom.text), p(near.text), p(far.text));

        fov.text = newfov + "";

        updateCam();
    }

    void FixedUpdate()
    {
        Matrix4x4 worldToCameraMatrix = cam.transform.worldToLocalMatrix;
        
        for(int i = 0; i < 4; i++)
            for(int j = 0; j < 4; j++)
            {
                int ind = (i << 2) + j;
                ToCameraMat.Find((ind + 1) + "").GetComponent<TMP_Text>().text = Math.Round(worldToCameraMatrix[ind], 2) + "";
            }
    }

    public void updateFromFOV()
    {
        System.Func<string,float> p = (string num) => float.Parse(num);

        Tuple<float, float, float, float> panes = VerticalFovToFrustrumPlanes(p(fov.text), cam.aspect, p(near.text), p(far.text));

        left.text = panes.Item1 + "";
        right.text = panes.Item2 + "";
        top.text = panes.Item3 + "";
        bottom.text = panes.Item4 + "";

        updateCam(p(fov.text), p(near.text), p(far.text), panes.Item1, panes.Item2, panes.Item3, panes.Item4);
    }

    public void updateCam()
    {
        System.Func<string,float> p = (string num) => float.Parse(num);
        updateCam(p(fov.text), p(near.text), p(far.text), p(left.text), p(right.text), p(top.text), p(bottom.text));
    }

    void updateCam(float fov, float near, float far, float left, float right, float top, float bottom)
    {
        Debug.Log(projType);
        if(projType == 0) // Perspective / Frustum
        {
            // cam.projectionMatrix = Matrix4x4.Perspective(fov, cam.pixelWidth / cam.pixelHeight, near, far);
            cam.projectionMatrix = Matrix4x4.Frustum(left, right, bottom, top, near, far);
            cam.orthographic = false;
        }
        else if(projType == 1) // Ortho
        {
            cam.projectionMatrix = Matrix4x4.Ortho(left, right, bottom, top, near, far);
            cam.orthographic = true;
        }


        for(int i = 0; i < 4; i++)
            for(int j = 0; j < 4; j++)
            {
                int ind = (i << 2) + j;
                ToClipMat.Find((ind + 1) + "").GetComponent<TMP_Text>().text = Math.Round(cam.projectionMatrix[ind],2) + "";
            }
    }

}
