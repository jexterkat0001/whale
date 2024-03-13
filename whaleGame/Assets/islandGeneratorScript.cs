using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class islandGeneratorScript : MonoBehaviour
{
    public GameObject island;

    [SerializeField]
    private int startPoint;
    [SerializeField]
    private int endPoint;
    [SerializeField]
    private float spawnChance;
    [SerializeField]
    private float spawnChanceIncrease;
    [SerializeField]
    private float distance;



    List<Vector2> islandLocationList = new List<Vector2>();


    // Start is called before the first frame update
    void Start()
    {
        generateIslandLocations();

        for(int i = 0; i < islandLocationList.Count; i++)
        {
            spawnIsland(islandLocationList[i]);
        }
    }


    // Update is called once per frame
    void Update()
    {

    }


    private void generateIslandLocations()
    {
        for (int i = startPoint; i < endPoint; i++)
        {
            if (Random.Range(0f, 100f) < spawnChance + i * spawnChanceIncrease)
            {
                float angle = Random.Range(0f, 2 * Mathf.PI);

                Vector2 islandPosition = new Vector2(i * Mathf.Cos(angle), i * Mathf.Sin(angle));

                if(checkIslandOverlaps(islandPosition))
                {
                    islandLocationList.Add(islandPosition);
                }

            }
        }
    }

    private bool checkIslandOverlaps(Vector2 islandPosition)
    {
        float x = islandPosition.x;
        float y = islandPosition.y;

        for (int i = 0; i < islandLocationList.Count; i++)
        {
            if(pythagorean(x, islandLocationList[i].x, y, islandLocationList[i].y) < distance)
            {
                Debug.Log("A");
                return false;
            }
        }

        return true;
    }

    private float pythagorean(float x1, float x2, float y1, float y2)
    {
        return (Mathf.Sqrt(Mathf.Pow((x1 - x2), 2) + Mathf.Pow((y1 - y2), 2)));
    }

    private void spawnIsland(Vector2 location)
    {
        Instantiate(island, new Vector3(location.x, location.y, 0), Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
    }






}
