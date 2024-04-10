using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour
{
    public GameObject ship;
    public Misc misc;

    [SerializeField]
    private float scrollAmount;
    [SerializeField]
    private float maxZoomIn;
    [SerializeField]
    public float maxZoomOut;

    // Start is called before the first frame update
    void Start()
    {
        if(misc.testMode)
        {
            maxZoomOut = 500f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(ship.transform.position.x, ship.transform.position.y, -10);

        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        Camera.main.orthographicSize *= Mathf.Pow(2,(-scrollInput * scrollAmount));
        if(Camera.main.orthographicSize < maxZoomIn)
        {
            Camera.main.orthographicSize = maxZoomIn;
        }
        if (Camera.main.orthographicSize > maxZoomOut)
        {
            Camera.main.orthographicSize = maxZoomOut;
        }

    }
}
