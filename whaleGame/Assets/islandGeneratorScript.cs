using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class islandGeneratorScript : MonoBehaviour
{
    public GameObject island;


    [SerializeField]
    private int[] thresholds;
    [SerializeField]
    private float spawnChance;
    [SerializeField]
    private float spawnChanceIncrease;
    [SerializeField]
    private float deletionDistance;


    public List<Vector2> islandLocationList = new List<Vector2>();
    public List<Vector2>[] islandRings = {new List<Vector2>(), new List<Vector2>(), new List<Vector2>(), new List<Vector2>()};


    // Start is called before the first frame update
    void Start()
    {
        generateIslandLocations();

        for (int i = 0; i < islandLocationList.Count; i++)
        {
            spawnIsland(islandLocationList[i]);
        }


        Debug.Log(islandRings[0].Count);
        Debug.Log(islandRings[1].Count);
        Debug.Log(islandRings[2].Count);
        Debug.Log(islandRings[3].Count);
    }




    // Update is called once per frame
    void Update()
    {


    }


    private void generateIslandLocations()
    {
        for (int ring = 0; ring < 4; ring++)
        {
            for (int i = thresholds[ring]; i < thresholds[ring + 1]; i++)
            {
                if (Random.Range(0f, 100f) < spawnChance + i * spawnChanceIncrease)
                {
                    float angle = Random.Range(0f, 2 * Mathf.PI);


                    Vector2 islandPosition = new Vector2(i * Mathf.Cos(angle), i * Mathf.Sin(angle));


                    if (checkIslandOverlaps(islandPosition))
                    {
                        islandLocationList.Add(islandPosition);
                        islandRings[ring].Add(islandPosition);
                    }
                }
            }
        }
    }


    private bool checkIslandOverlaps(Vector2 islandPosition)
    {
        for (int i = 0; i < islandLocationList.Count; i++)
        {
            if (pythagorean(islandPosition, islandLocationList[i]) < deletionDistance)
            {
                return false;
            }
        }


        return true;
    }


    private float pythagorean(Vector2 position1, Vector2 position2)
    {
        return (Mathf.Sqrt(Mathf.Pow((position1.x - position2.x), 2) + Mathf.Pow((position1.y - position2.y), 2)));
    }


    private void spawnIsland(Vector2 location)
    {
        Instantiate(island, new Vector3(location.x, location.y, -1), Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)), this.transform);
    }
}
