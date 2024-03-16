using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class islandScript : MonoBehaviour
{
    public List<GameObject> islands;

    private int islandNumber;

    // Start is called before the first frame update
    void Start()
    {
        islandNumber = Random.Range(0, 9);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ContextMenu("load")]
    public void load()
    {
        Instantiate(islands[islandNumber], transform, false);
    }

    [ContextMenu("unload")]
    public void unload()
    {
        Transform islandTransform = gameObject.GetComponentsInChildren<Transform>()[2];
        Destroy(islandTransform.gameObject);
    }


}
