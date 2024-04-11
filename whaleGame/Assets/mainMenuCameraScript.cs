using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainMenuCameraScript : MonoBehaviour
{
    public List<GameObject> islands;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(islands[Random.Range(0, islands.Count-1)], Vector3.up*1.5f, Quaternion.Euler(0f,0f,Random.Range(0,360f)), transform);
        transform.GetChild(0).localScale = new Vector3(1.3f, 1.3f, 1f);
    }

}
