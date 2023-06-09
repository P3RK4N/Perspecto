using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField]
    float f_CamSpeed = 20.0f;


    public Transform r_CameraTF;
    Transform r_TF;

    float m_X = 0.0f;
    float m_Y = 0.0f;

    float m_X3 = 0.0f;
    float m_Y3 = 0.0f;

    void Awake()
    {
        r_CameraTF = GetComponentInChildren<Camera>().transform;
        r_TF = transform;

        // Cursor.visible = false;
        // Cursor.lockState = CursorLockMode.Locked;
    }

    public void moveCamera(Vector2 delta)
    {
        m_X = (m_X + delta.x * Time.deltaTime * f_CamSpeed) % 360.0f;
        m_Y = Mathf.Clamp(m_Y + delta.y * Time.deltaTime * f_CamSpeed, -85.0f, 85.0f);

        var fwd = r_TF.forward;
        fwd = Quaternion.Euler(0, m_X, 0) * (Quaternion.Euler(-m_Y, 0, 0) * fwd);
        r_CameraTF.rotation = Quaternion.LookRotation(fwd, Vector3.up);
    }

    public void moveCamera3(Vector2 delta)
    {
        m_X3 = (m_X3 + delta.x * Time.deltaTime * f_CamSpeed) % 360.0f;
        m_Y3 = Mathf.Clamp(m_Y3 + delta.y * Time.deltaTime * f_CamSpeed, -85.0f, 85.0f);

        var fwd = r_TF.forward;
        fwd = Quaternion.Euler(0, m_X3, 0) * (Quaternion.Euler(-m_Y3, 0, 0) * fwd);
        r_CameraTF.rotation = Quaternion.LookRotation(fwd, Vector3.up);
    }
}
