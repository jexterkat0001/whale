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
    [SerializeField]
    private float mouseInfluence;
    [SerializeField]
    private float mouseLerpAmount;

    private float currentMouseOffsetX = 0f;
    private float currentMouseOffsetY = 0f;

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
        float targetMouseOffsetX = (Mathf.Clamp(Input.mousePosition.x, 0f, Screen.width) - (Screen.width / 2)) * mouseInfluence * Mathf.Pow(Camera.main.orthographicSize, 0.1f);
        currentMouseOffsetX += (targetMouseOffsetX - currentMouseOffsetX) * mouseLerpAmount;
        float targetMouseOffsetY = (Mathf.Clamp(Input.mousePosition.y, 0f, Screen.height) - (Screen.height / 2)) * mouseInfluence * Mathf.Pow(Camera.main.orthographicSize, 0.1f);
        currentMouseOffsetY += (targetMouseOffsetY - currentMouseOffsetY) * mouseLerpAmount;
        transform.position = new Vector3(ship.transform.position.x + currentMouseOffsetX, ship.transform.position.y + currentMouseOffsetY, -10);

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
