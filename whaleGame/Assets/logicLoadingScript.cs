using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class logicLoadingScript : MonoBehaviour
{
    public GameObject ship;
    public Misc misc;

    public islandGeneratorScript islandGeneratorScript;
    private List<GameObject> islandList;
    private GameObject nearestIsland;

    public whaleSystemGeneratorScript whaleSystemGeneratorScript;
    private Vector2 cornerPosition;

    [SerializeField] private float loadUpdateWaitTime;

    // Start is called before the first frame update
    void Start()
    {
        islandList = islandGeneratorScript.islandList;
        nearestIsland = islandGeneratorScript.gameObject.transform.GetChild(0).gameObject;

        StartCoroutine(loadUpdate());
    }

    IEnumerator loadUpdate()
    {
        for (;;)
        {
            updateIslands();
            updateWhaleSystems();
            yield return new WaitForSeconds(loadUpdateWaitTime);
        }
    }

    private void updateIslands()
    {
        float previousLowestDistance = misc.pythagorean(nearestIsland, ship);
        for (int i = 0; i < islandList.Count; i++)
        {
            GameObject newNearestIsland = nearestIsland;
            if (misc.pythagorean(islandList[i], ship) < previousLowestDistance)
            {
                newNearestIsland = islandList[i];
            }

            if (newNearestIsland != nearestIsland)
            {
                nearestIsland.GetComponent<islandScript>().unload();
                nearestIsland = newNearestIsland;
                newNearestIsland.GetComponent<islandScript>().load();
            }
        }
    }

    private void updateWhaleSystems()
    {
        Vector2 newCornerPosition = new Vector2(
            ship.transform.position.x - ((ship.transform.position.x + 10050) % 100) - 50,
            ship.transform.position.y - ((ship.transform.position.y + 10050) % 100) - 50
        );

        if (newCornerPosition != cornerPosition)
        {
            cornerPosition = newCornerPosition;
            Transform whaleSystemGenerator = whaleSystemGeneratorScript.transform;

            List<Vector2> requiredWhaleSystemPositions = new List<Vector2>()
            {
                cornerPosition,
                new Vector2 (cornerPosition.x+100, cornerPosition.y),
                new Vector2 (cornerPosition.x, cornerPosition.y+100),
                new Vector2 (cornerPosition.x+100, cornerPosition.y+100)
            };

            for(int i = whaleSystemGenerator.childCount - 1; i >= 0; i--)
            {
                Vector2 whaleSystemPosition = new Vector2(whaleSystemGenerator.GetChild(i).position.x, whaleSystemGenerator.GetChild(i).position.y);
                bool destroyWhaleSystem = true;
                for (int j = 0; j < requiredWhaleSystemPositions.Count; j++)
                {
                    if(whaleSystemPosition == requiredWhaleSystemPositions[j])
                    {
                        destroyWhaleSystem = false;
                        requiredWhaleSystemPositions.RemoveAt(j);
                        break;
                    }
                }

                if(destroyWhaleSystem)
                {
                    Destroy(whaleSystemGenerator.GetChild(i).gameObject);
                }
            }

            for(int i = 0; i < requiredWhaleSystemPositions.Count; i++)
            {
                whaleSystemGeneratorScript.generateWhaleSystem(requiredWhaleSystemPositions[i]);
            }
        }
    }
}

