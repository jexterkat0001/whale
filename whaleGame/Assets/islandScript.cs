using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class islandScript : MonoBehaviour
{
    public List<GameObject> islands;

    public int islandType;

    // Start is called before the first frame update
    void Start()
    {
        islandType = Random.Range(0, islands.Count-1);
        name = GameObject.FindWithTag("logic").GetComponent<Misc>().getName();
    }

    [ContextMenu("load")]
    public void load()
    {
        if (transform.childCount == 1)
        {
            Instantiate(islands[islandType], transform, false);
        }
        else
        {
            transform.GetChild(1).gameObject.SetActive(true);
        }
    }

    [ContextMenu("unload")]
    public void unload()
    {
        transform.GetChild(1).gameObject.SetActive(false);
    }

}
