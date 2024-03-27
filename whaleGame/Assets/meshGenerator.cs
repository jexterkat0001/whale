using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meshGenerator : MonoBehaviour
{
    public GameObject whaleTilePrefab;

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

    // Start i  called before the first frame update
    void Start()
    {
        createWhaleSystem(0,0);
    }

    public void createWhaleSystem(float xOffset, float yOffset)
    {
        Instantiate(whaleTilePrefab, transform.position, transform.rotation, this.transform);
        Transform whaleTile = transform.GetChild(transform.childCount-1);

        Mesh outerMesh = createMesh(xOffset,yOffset,threshold1,threshold2);
        Transform whaleSystemOuter = whaleTile.transform.GetChild(0);
        whaleSystemOuter.GetComponent<MeshFilter>().mesh = outerMesh;
        whaleSystemOuter.GetComponent<MeshRenderer>().material.color = color1;
        var whaleSystemOuterShape = whaleSystemOuter.GetComponent<ParticleSystem>().shape;
        whaleSystemOuterShape.mesh = outerMesh;
        /*
        Mesh innerMesh = createMesh(xOffset,yOffset,threshold2,1f);
        Transform whaleSystemInner = whaleTile.transform.GetChild(1);
        whaleSystemInner.GetComponent<MeshFilter>().mesh = innerMesh;
        whaleSystemInner.GetComponent<MeshRenderer>().material.color = color2;
        var whaleSystemInnerShape = whaleSystemInner.GetComponent<ParticleSystem>().shape;
        whaleSystemInnerShape.mesh = innerMesh;*/
    }

    Mesh createMesh(float xOffset, float yOffset, float lowerThreshold, float upperThreshold)
    {
        Vector3[] vertices = new Vector3[(width+1) * (height+1)];
        List<int> triangles = new List<int>();

        for(int i = 0, y = 0; y <= height; y++)
        {
            for(int x = 0; x <= width; x++)
            {
                vertices[i] = new Vector3(x + xOffset,y + yOffset,0);
                i++;
            }
        }

        for(int i = 0, vertex = 0, y = 0; y < height; y++)
        {
            for(int x = 0; x < width; x++, i+=6)
            {
                float value = Mathf.PerlinNoise(vertices[vertex].x * noiseStretch * Mathf.PI + 1000000, vertices[vertex].y * noiseStretch * Mathf.PI + 1000000);
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
        return mesh;
    }
    
}
