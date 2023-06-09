using UnityEngine;

public class DrawLines : MonoBehaviour
{
    public Material lineMaterial;
    public Vector3[] linePoints;

    Mesh lineMesh;

    private void Start()
    {
        // Create a new mesh for the lines
        lineMesh = new Mesh();

        // Set up the vertices and indices for the lines
        Vector3[] vertices = new Vector3[linePoints.Length];
        int[] indices = new int[linePoints.Length];
        for (int i = 0; i < linePoints.Length; i++)
        {
            vertices[i] = linePoints[i];
            indices[i] = i;
        }

        // Set the vertices and indices for the mesh
        lineMesh.vertices = vertices;
        lineMesh.SetIndices(indices, MeshTopology.Lines, 0);

    }

    void Update()
    {
        // Draw the lines
        Graphics.DrawMesh(lineMesh, transform.position, transform.rotation, lineMaterial, 0);
    }
}