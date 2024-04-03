using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class logicTargetScript : MonoBehaviour
{
    public islandGeneratorScript islandGeneratorScript;
    public GameObject ship;
    public arrowScript arrowScript;
    public menuScript menuScript;

    private List<Vector2> islandLocationList;
    private Vector2 target;
    private Vector2 previousTarget;
    private int poolSize;


    // Start is called before the first frame update
    void Start()
    {

    }
    bool e = true;
    // Update is called once per frame
    void Update()
    {
        if (e)
        {
            e = false;
            getNewTarget();
            
        }
    }


    public void getNewTarget()
    {
        Vector2 shipLocation = new Vector2(ship.transform.position.x, ship.transform.position.y);
        int shipUpgrade = ship.GetComponent<shipUpgradeScript>().ship;
        List<Vector2> possibleTargetList = getClosestPositions(islandGeneratorScript.islandRings[shipUpgrade], shipLocation);
        target = possibleTargetList[Random.Range(0, poolSize)];
        arrowScript.target = target;

        menuScript.distance = pythagorean(shipLocation, target);
        menuScript.value = 1f;
    }


    private List<Vector2> getClosestPositions(List<Vector2> positionList, Vector2 centerPosition)
    {
        List<Vector3> distanceList = new List<Vector3>();
        for (int i = 0; i < positionList.Count; i++)
        {
            float distance = pythagorean(positionList[i], centerPosition);
            distanceList.Add(new Vector3(positionList[i].x, positionList[i].y, distance));
        }


        List<Vector3> sortedList = binarySort(distanceList);


        List<Vector2> finalList = new List<Vector2>();
        if(sortedList.Count < 10)
        {
            poolSize = sortedList.Count;
        }
        else
        {
            poolSize = 10;
        }
        for (int i = 0; i < poolSize; i++)
        {
            Vector2 possibleTarget = new Vector2(sortedList[i].x, sortedList[i].y);
            if (possibleTarget != target && possibleTarget != previousTarget)
            {
                finalList.Add(new Vector2(sortedList[i].x, sortedList[i].y));
            }
        }
        return finalList;
    }


    private List<Vector3> binarySort(List<Vector3> unsortedList)
    {
        List<Vector3> sortedList = new List<Vector3>();


        for (int i = 0; i < unsortedList.Count; i++)
        {
            int insertionIndex = binarySearch(sortedList, 0, sortedList.Count - 1, unsortedList[i]);
            sortedList.Insert(insertionIndex, unsortedList[i]);
        }


        return sortedList;
    }




    private int binarySearch(List<Vector3> list, int startIndex, int endIndex, Vector3 input)
    {
        int length = endIndex - startIndex + 1;
        if (length == 0)
        {
            return startIndex;
        }


        int checkIndex = startIndex + Mathf.CeilToInt(length / 2);

        if (input.z < list[checkIndex].z)
        {
            int newEndIndex = checkIndex - 1;
            int newLength = newEndIndex - startIndex + 1;
            return binarySearch(list, startIndex, newEndIndex, input);
        }
        else
        {
            int newStartIndex = checkIndex + 1;
            int newLength = endIndex - newStartIndex + 1;
            return binarySearch(list, newStartIndex, endIndex, input);
        }
    }


    private float pythagorean(Vector2 position1, Vector2 position2)
    {
        return (Mathf.Sqrt(Mathf.Pow((position1.x - position2.x), 2) + Mathf.Pow((position1.y - position2.y), 2)));
    }
}