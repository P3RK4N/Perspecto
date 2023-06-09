using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchCamera : MonoBehaviour, IDragHandler, IPointerClickHandler
{
    PlayerCamera r_PC;
    PlayerCamera r_Cam3;

    PlayerMovement pm;

    void Awake()
    {
        r_PC = FindObjectOfType<PlayerCamera>();
        pm = FindObjectOfType<PlayerMovement>();
    }

    public void OnPointerClick(PointerEventData ped)
    {
        // Cursor.visible = false;
        // Cursor.lockState = CursorLockMode.Locked;
    }

    public void OnPointerMove(PointerEventData ped)
    {
        // if(Application.isFocused)
        // {

        // }
    }

    public void OnDrag(PointerEventData ped)
    {
        if(pm.f_Moving) r_PC.moveCamera(ped.delta);
        else r_PC.moveCamera3(ped.delta);
    }
}
