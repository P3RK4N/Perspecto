using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraUI : MonoBehaviour, IPointerClickHandler
{
    Camera cam1;
    Camera cam3;
    UnityEngine.UI.RawImage img;
    RenderTexture rt;
    GameObject bb;

    void Awake()
    {
        cam1 = FindObjectOfType<Camera1>().GetComponent<Camera>();
        cam3 = FindObjectOfType<Camera3>().GetComponent<Camera>();

        bb = FindObjectOfType<Billboard>().gameObject;
        img = GetComponent<UnityEngine.UI.RawImage>();
        rt = new RenderTexture((int)(cam1.pixelWidth * 0.15f), (int)(cam1.pixelHeight * 0.15f), 24);

        img.texture = rt;
        cam3.targetTexture = rt;
    }

    public void OnPointerClick(PointerEventData ped)
    {
        if(cam1.targetTexture != rt)
        {
            cam3.targetTexture = null;
            cam1.targetTexture = rt;
            FindObjectOfType<PlayerCamera>().r_CameraTF = cam3.transform;
            FindObjectOfType<PlayerMovement>().f_Moving = false;
            cam3.GetComponent<CameraFly>().flying = true;
            // bb.SetActive(false);
            cam1.GetComponent<AudioListener>().enabled = false;
            cam3.GetComponent<AudioListener>().enabled = true;
        }
        else
        {
            cam1.targetTexture = null;
            cam3.targetTexture = rt;
            FindObjectOfType<PlayerCamera>().r_CameraTF = cam1.transform;
            FindObjectOfType<PlayerMovement>().f_Moving = true;
            cam3.GetComponent<CameraFly>().flying = false;
            // bb.SetActive(true);
            cam1.GetComponent<AudioListener>().enabled = true;
            cam3.GetComponent<AudioListener>().enabled = false;
        }
    }
}
