using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class logicTargetScript : MonoBehaviour
{
    public islandGeneratorScript islandGeneratorScript;
    public GameObject ship;
    public arrowScript arrowScript;
    public menuScript menuScript;
    public Misc misc;

    private List<Vector2> islandLocationList;
    private Vector2 target = Vector2.zero;
    private Vector2 previousTarget;
    private int poolSize;

    [ContextMenu("getTargetChoices")]
    public GameObject[] getTargetChoices()
    {
        Vector2 shipLocation = new Vector2(ship.transform.position.x, ship.transform.position.y);
        int shipUpgrade = ship.GetComponent<shipScript>().currentShipUpgrade;
        List<Vector2> possibleTargetList = getClosestPositions(islandGeneratorScript.islandRings[shipUpgrade], shipLocation);

        GameObject[] targetChoices = new GameObject[3];
        for (int i = 0; i < 3; i++)
        {
            int index = Random.Range(0, possibleTargetList.Count - 1);
            targetChoices[i] = islandGeneratorScript.getIslandGameObject(possibleTargetList[index]);
            possibleTargetList.RemoveAt(index);
        }

        return targetChoices;
    }

    public void setTarget(Vector2 targetChoice)
    {
        previousTarget = target;
        target = targetChoice;
        arrowScript.target = target;
    }

    private List<Vector2> getClosestPositions(List<Vector2> positionList, Vector2 centerPosition)
    {
        List<Vector3> distanceList = new List<Vector3>();
        for (int i = 0; i < positionList.Count; i++)
        {
            float distance = misc.pythagorean(positionList[i], centerPosition);
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
}