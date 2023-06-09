using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using Unity.Collections;

public class Lines : MonoBehaviour
{
    public int resX = 10;
    public int resY = 10;
    public Material triangleMat;
    public Material lineMat;

    static readonly int maxNumIndices = 200;

    Mesh triangleMesh;
    Mesh lineMesh;

    Camera cam1;
    Camera cam3;
    Camera ar;
    Transform tf;

    CameraStats cs;

    GameObject r_Frustum;
    MeshFilter r_FrustumFilter;
    MeshRenderer r_FrustumRenderer;

    GameObject r_FrustumLine;
    MeshFilter r_FrustumLineFilter;
    MeshRenderer r_FrustumLineRenderer;

    readonly Color[] triangleColors =
    {
        Color.red,
        Color.red,
        Color.red,
        Color.red,
        Color.red,
        Color.red,
        Color.blue,
        Color.blue,
        Color.blue,
        Color.blue,
        Color.blue,
        Color.blue,
        Color.red,
        Color.red,
        Color.red,
        Color.red,
        Color.red,
        Color.red,
        Color.blue,
        Color.blue,
        Color.blue,
        Color.blue,
        Color.blue,
        Color.blue,
        Color.green,
        Color.green,
        Color.green,
        Color.green,
        Color.green,
        Color.green,
        Color.green,
        Color.green,
        Color.green,
        Color.green,
        Color.green,
        Color.green,
    };

    readonly int[] triangleIndices =
    {
        // Left
        0, 1, 2,
        2, 1, 3,

        // Bottom
        0, 4, 1,
        4, 5, 1,

        // Right
        7, 4, 6,
        7, 5, 4,

        // Top
        3, 6, 2,
        3, 7, 6,

        // Near
        2, 6, 0,
        6, 4, 0,

        // Far
        3, 1, 7,
        7, 1, 5
    }; // 36

    readonly int[] lineIndices =
    {
        0,1,
        2,3,
        4,5,
        6,7,

        0,2,
        0,4,
        2,6,
        4,6,

        1,3,
        1,5,
        3,7,
        5,7,
    };

    void Start()
    {
        cs = FindObjectOfType<CameraStats>();
        cam1 = GetComponent<Camera>();

        if(FindObjectOfType<Camera3>() != null)
        {
            cam3 = FindObjectOfType<Camera3>().GetComponent<Camera>();
        }

        tf = transform;

        triangleMesh = new Mesh();
        triangleMesh.bounds = new Bounds(Vector3.zero, new Vector3(1,1,1) * 1000);

        lineMesh = new Mesh();
        lineMesh.bounds = new Bounds(Vector3.zero, new Vector3(1,1,1) * 1000);

        updateSubData();

        int[] indices = new int[36];
        for(int i = 0; i < indices.Length; i++)
            indices[i] = i;

        triangleMesh.SetIndices(indices, MeshTopology.Triangles, 0);
        triangleMesh.SetColors(triangleColors);
        lineMesh.SetIndices(lineIndices, MeshTopology.Lines, 0);

        r_Frustum = new GameObject("Frustum");
        r_Frustum.layer = LayerMask.NameToLayer("Frustum");
        r_Frustum.transform.SetParent(transform, false);
        r_FrustumFilter = r_Frustum.AddComponent<MeshFilter>();
        r_FrustumFilter.mesh = triangleMesh;
        r_FrustumRenderer = r_Frustum.AddComponent<MeshRenderer>();
        r_FrustumRenderer.material = triangleMat;

        r_FrustumLine = new GameObject("FrustumLine");
        r_FrustumLine.layer = LayerMask.NameToLayer("Frustum");
        r_FrustumLine.transform.SetParent(transform, false);
        r_FrustumLineFilter = r_FrustumLine.AddComponent<MeshFilter>();
        r_FrustumLineFilter.mesh = lineMesh;
        r_FrustumLineRenderer = r_FrustumLine.AddComponent<MeshRenderer>();
        r_FrustumLineRenderer.material = lineMat;
    }

    void Update()
    {
        updateSubData();
    }

    public void OnPreCull()
    {
        if(!cam3) return;

        cam3.ResetCullingMatrix();
        Matrix4x4 m = cam3.cullingMatrix;
        m.SetRow(0, m.GetRow(0)*0.5f);
        m.SetRow(1, m.GetRow(1)*0.5f);
        cam3.cullingMatrix = m;
    }

    Vector3 m_RayDir = Vector3.zero;
    Vector3 m_RayPos = Vector3.zero;
    Vector3 m_ScreenPos = Vector3.zero;
    Vector3[] m_TriangleVertices = new Vector3[36];
    void updateSubData()
    {
        float near = Mathf.Max(float.Parse(cs.near.text), 0.001f);
        float far = Mathf.Min(float.Parse(cs.far.text), 1000.0f);

        Vector3[] vertices = new Vector3[resX*resY*2];

        float x = 1.0f/resX * cam1.pixelWidth;
        float y = 1.0f/resY * cam1.pixelHeight;

        int pos = 0;

        for(int i = 0; i < resX; i += resX-1)
        {
            m_ScreenPos.x = x*i;
            for(int j = 0; j < resY; j += resY-1)
            {
                m_ScreenPos.y = y*j;

                // m_RayPos = cam1.ScreenToWorldPoint(m_ScreenPos);
                m_RayPos = cam1.ScreenToWorldPoint(m_ScreenPos);
                m_RayPos -= cam1.transform.position;
                m_RayPos = Quaternion.Inverse(cam1.transform.rotation) * m_RayPos;

                // m_RayDir = cam1.ScreenPointToRay(m_ScreenPos).direction;
                m_RayDir = cam1.ScreenPointToRay(m_ScreenPos).direction;
                m_RayDir = Quaternion.Inverse(cam1.transform.rotation) * m_RayDir;
                m_RayDir.Normalize();

                // m_RayDir /= Mathf.Max(0.001f, Vector3.Dot(m_RayDir, tf.forward));
                m_RayDir /= Mathf.Max(0.001f, Vector3.Dot(m_RayDir, Vector3.forward));

                vertices[pos++] = m_RayPos + near * m_RayDir;
                vertices[pos++] = m_RayPos + far * m_RayDir;
            }
        }

        for(int i = 0; i < 36; i++)
        {
            m_TriangleVertices[i] = vertices[triangleIndices[i]];
        }

        triangleMesh.SetVertices(m_TriangleVertices, 0, 36);
        triangleMesh.RecalculateNormals();
        lineMesh.SetVertices(vertices, 0, 8);
    }
}
