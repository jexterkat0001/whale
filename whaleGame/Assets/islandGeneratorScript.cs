using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class islandGeneratorScript : MonoBehaviour
{
    public GameObject islandPrefab;
    public Misc misc;

    [SerializeField]
    private int[] thresholds;
    [SerializeField]
    private float spawnChance;
    [SerializeField]
    private float spawnChanceIncrease;
    [SerializeField]
    private float deletionDistance;

    public List<GameObject> islandList = new List<GameObject>();
    public List<Vector2>[] islandRings = { new List<Vector2>(), new List<Vector2>(), new List<Vector2>(), new List<Vector2>() };

    // Start is called before the first frame update
    void Start()
    {
        islandList.Add(transform.GetChild(0).gameObject);
        islandRings[0].Add(Vector2.zero);
        transform.GetChild(0).GetComponent<islandScript>().load();
        spawnIslands();
        Debug.Log(islandRings[0].Count);
    }

    public GameObject getIslandGameObject(Vector2 position)
    {
        for (int i = 0; i < islandList.Count; i++)
        {
            if (position == new Vector2(islandList[i].transform.position.x, islandList[i].transform.position.y))
            {
                return islandList[i];
            }
        }
        return null;
    }

    private void spawnIslands()
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
                        spawnIsland(islandPosition);
                        islandRings[ring].Add(islandPosition);
                    }
                }
            }
        }
    }

    private bool checkIslandOverlaps(Vector2 islandPosition)
    {
        for (int i = 0; i < islandList.Count; i++)
        {
            Vector3 islandIPosition = islandList[i].transform.position;
            if (misc.pythagorean(islandPosition, new Vector2(islandIPosition.x, islandIPosition.y)) < deletionDistance)
            {
                return false;
            }
        }
        return true;
    }

    private void spawnIsland(Vector2 location)
    {
        islandList.Add(Instantiate(islandPrefab, new Vector3(location.x, location.y, -1), Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)), this.transform) as GameObject);
    }
}
