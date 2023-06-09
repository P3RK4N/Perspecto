using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using WebXR;
using UnityEngine.UI;
using TMPro;

public class ToggleAR : MonoBehaviour
{
   WebXRManager r_WebXRManager;
   Button r_Button;

   void Awake()
   {
      r_WebXRManager = FindObjectOfType<WebXRManager>();
      r_Button = GetComponent<Button>();

      r_Button.onClick.AddListener(() => onButton());
   }

   void onButton()
   {
      Debug.Log("XR Toggled!");
      r_WebXRManager.ToggleAR();
      // r_WebXRManager.ToggleVR();
   }
}
