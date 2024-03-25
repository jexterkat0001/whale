using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class meshGenerator : MonoBehaviour
{
    public ParticleSystem particleSystem;

    Mesh mesh;

    [SerializeField]
    private int width = 20;
    [SerializeField]
    private int height = 20;
    [SerializeField]
    private Color color;

    Vector3[] vertices;
    int[] triangles;

    // Start i  called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshRenderer>().material.color = color;
        var shape = particleSystem.shape;
        shape.mesh = mesh;

        CreateShape();
        UpdateMesh();
    }

    void Update(){GetComponent<MeshRenderer>().material.color = color;}

    void CreateShape()
    {
        vertices = new Vector3[(width+1) * (height+1)];
        for(int i = 0, y = 0; y <= height; y++)
        {
            for(int x = 0; x <= width; x++)
            {
                vertices[i] = new Vector3(x,y,0);
                i++;
            }
        }

        triangles = new int[width*height*6];
        int index = 0;
        for(int vertex = 0, y = 0; y < height; y++)
        {
            for(int x = 0; x < width; x++)
            {
                triangles[index] = vertex;
                triangles[index+1] = vertex + width + 1;
                triangles[index+2] = vertex + 1;
                triangles[index+3] = vertex + 1;
                triangles[index+4] = vertex + width + 1;
                triangles[index+5] = vertex + width + 2;
                index += 6;
                vertex++;
            }
            vertex++;
        }
        
        
    }

    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;
    }
    
}
