using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class whaleSystemGeneratorScript : MonoBehaviour
{
    public GameObject whaleSystem;
    public Collider2D whaleDetector;
    public Collider2D whaleSpotter;

    private int width = 100;
    private int height = 100;
    [SerializeField]
    private float noiseStretch;
    [SerializeField]
    private float threshold1;
    [SerializeField]
    private float threshold2;
    [SerializeField]
    private Color color1;
    [SerializeField]
    private Color color2;

    public void generateWhaleSystem(Vector2 location)
    {
        Instantiate(whaleSystem, new Vector3(transform.position.x + location.x, transform.position.y + location.y, transform.position.z), transform.rotation, this.transform);
        Transform whaleTile = transform.GetChild(transform.childCount-1);


        Mesh[] outerMeshes = createMeshes(location.x, location.y, threshold1, threshold2);

        Mesh outerMesh = outerMeshes[0];
        Transform whaleSystemOuter = whaleTile.transform.GetChild(0);
        var whaleSystemOuterShape = whaleSystemOuter.GetComponent<ParticleSystem>().shape;
        whaleSystemOuterShape.mesh = outerMesh;
        var whaleSystemOuterTrigger = whaleSystemOuter.GetComponent<ParticleSystem>().trigger;
        whaleSystemOuterTrigger.AddCollider(whaleDetector);
        whaleSystemOuterTrigger.AddCollider(whaleSpotter);

        Mesh outerOverlayMesh = outerMeshes[1];
        whaleSystemOuter.GetComponent<MeshFilter>().mesh = outerOverlayMesh;
        whaleSystemOuter.GetComponent<MeshRenderer>().material.color = color1;


        Mesh[] innerMeshes = createMeshes(location.x, location.y, threshold2, 1f);

        Mesh innerMesh = innerMeshes[0];
        Transform whaleSystemInner = whaleTile.transform.GetChild(1);
        var whaleSystemInnerShape = whaleSystemInner.GetComponent<ParticleSystem>().shape;
        whaleSystemInnerShape.mesh = innerMesh;
        var whaleSystemInnerTrigger = whaleSystemInner.GetComponent<ParticleSystem>().trigger;
        whaleSystemInnerTrigger.AddCollider(whaleDetector);
        whaleSystemInnerTrigger.AddCollider(whaleSpotter);

        Mesh innerOverlayMesh = innerMeshes[1];
        whaleSystemInner.GetComponent<MeshFilter>().mesh = innerOverlayMesh;
        whaleSystemInner.GetComponent<MeshRenderer>().material.color = color2;
    }

    Mesh[] createMeshes(float xOffset, float yOffset, float lowerThreshold, float upperThreshold)
    {
        Vector3[] vertices = new Vector3[(width+1) * (height+1)];
        List<int> triangles = new List<int>();

        for(int i = 0, y = 0; y <= height; y++)
        {
            for(int x = 0; x <= width; x++)
            {
                vertices[i] = new Vector3(x,y,0);
                i++;
            }
        }

        for(int i = 0, vertex = 0, y = 0; y < height; y++)
        {
            for(int x = 0; x < width; x++, i+=6)
            {
                float value = Mathf.PerlinNoise((vertices[vertex].x+xOffset) * noiseStretch * Mathf.PI + 1000000, (vertices[vertex].y+yOffset) * noiseStretch * Mathf.PI + 1000000);
                if(value > lowerThreshold && value < upperThreshold)
                {
                    triangles.Add(vertex);
                    triangles.Add(vertex + width + 1);
                    triangles.Add(vertex + 1);
                    triangles.Add(vertex + 1);
                    triangles.Add(vertex + width + 1);
                    triangles.Add(vertex + width + 2);
                }
                vertex++;
            }
            vertex++;
        }

        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        int[] trianglesArray = new int[triangles.Count];
        for(int i = 0; i < triangles.Count; i++)
        {
            trianglesArray[i] = triangles[i];
        }
        mesh.triangles = trianglesArray;

        Mesh overlayMesh = new Mesh();
        Vector3[] overlayVertices = new Vector3[(width + 1) * (height + 1)];
        for (int i = 0; i < overlayVertices.Length; i++)
        {
            overlayVertices[i] = new Vector3(vertices[i].x, vertices[i].y, -0.5f);
        }
        overlayMesh.vertices = overlayVertices;
        overlayMesh.triangles = trianglesArray;

        return new Mesh[] { mesh, overlayMesh };
    }
}
