#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class Raycaster : MonoBehaviour
{
    [SerializeField]
    int f_ResolutionX;
    [SerializeField]
    int f_ResolutionY;

    Transform r_TF;
    Camera r_CAM;
    Transform r_CAMTF;

    void Awake()
    {
        r_TF = transform;
        r_CAM = GetComponent<Camera>();
        r_CAMTF = r_CAM.transform;
    }

    void Reset()
    {
        r_TF = transform;
        r_CAM = GetComponent<Camera>();
        r_CAMTF = r_CAM.transform;
    }

    void OnDrawGizmos()
    {
        throwRays();
    }

    Vector3 m_RayDir = Vector3.zero;
    Vector3 m_RayPos = Vector3.zero;
    Vector3 m_ScreenPos = Vector3.zero;
    void throwRays()
    {
        float x = 1.0f/f_ResolutionX * r_CAM.pixelWidth;
        float y = 1.0f/f_ResolutionY * r_CAM.pixelHeight;

        for(int i = 0; i < f_ResolutionX; i++)
        {
            m_ScreenPos.x = x * i;

            for(int j = 0; j < f_ResolutionY; j++)
            {
                m_ScreenPos.y = y * j;
                
                m_RayPos = r_CAM.ScreenToWorldPoint(m_ScreenPos);
                m_RayDir = r_CAM.ScreenPointToRay(m_ScreenPos).direction;
                Gizmos.DrawLine(m_RayPos+m_RayDir/Vector3.Dot(m_RayDir,r_CAMTF.forward) * r_CAM.nearClipPlane, m_RayPos+m_RayDir*5.0f);
            }
        }
    }
}

#endif
