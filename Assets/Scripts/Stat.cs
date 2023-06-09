using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class Stat : MonoBehaviour, IDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    public static Color toColor(uint hexVal)
    {
        return new Color
        (
            ((hexVal >> 24) & 0xFF) / 255f,
            ((hexVal >> 16) & 0xFF) / 255f,
            ((hexVal >>  8) & 0xFF) / 255f,
            ((hexVal >>  0) & 0xFF) / 255f 
        );
    }

    public float normalSpeed = 0.5f;
    public float slowSpeed = 0.01f;

    public TMP_InputField input;
    public TMP_Dropdown dropdown;
    Image img;
    TMP_Text title;

    CameraStats cs;

    bool hovered = false;

    private void Awake()
    {
        input = GetComponentInChildren<TMP_InputField>();
        dropdown = GetComponentInChildren<TMP_Dropdown>();
        img = transform.Find("Image").GetComponent<Image>();
        title = transform.Find("Title").GetComponent<TMP_Text>();
        cs = GetComponentInParent<CameraStats>();

        img.color = toColor(0x8300E9FF);
        title.text = name;
        if(input) input.onSubmit.AddListener(onTextChanged);
        if(dropdown) dropdown.onValueChanged.AddListener(onProjectionChanged);
    }

    //0 -> perspective
    //1 -> ortho
    void onProjectionChanged(int proj)
    {
        cs.projType = proj;
        cs.updateCam();
        cs.updateFromPlanes();
    }

    void onTextChanged(string s)
    {
        if(name == "Fov")
        {
            cs.updateFromFOV();
        }
        else
        {
            cs.updateFromPlanes();
        }
    }

    public void OnPointerEnter(PointerEventData ped)
    {
        if(!input) return;

        hovered = true;
        img.color = toColor(0x8300E982);
    }

    public void OnPointerExit(PointerEventData ped)
    {
        if(!input) return;

        hovered = false;
        img.color = toColor(0x8300E9FF);
    }

    public void OnDrag(PointerEventData ped)
    {
        if(!input) return;

        float val = float.Parse(input.text);
        val += ped.delta.x * (Input.GetKey(KeyCode.LeftAlt) ? slowSpeed : normalSpeed);
        input.text = val + "";

        if(name == "Fov")
        {
            cs.updateFromFOV();
        }
        else
        {
            cs.updateFromPlanes();
        }
    }
}
