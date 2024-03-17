using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowScript : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite[] sprites;

    public Vector2 target = Vector2.zero;

    [SerializeField]
    private float markerScaleConstant;
    [SerializeField]
    private float markerRotationSpeed;

    [SerializeField]
    private float arrowScaleConstant;
    [SerializeField]
    private float arrowPositionConstant;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(targetIsInFrame())
        {
            spriteRenderer.sprite = sprites[1];
            transform.position = new Vector3(target.x, target.y, -2);
            transform.localScale = Vector3.one * markerScaleConstant * Camera.main.orthographicSize;
            transform.Rotate(0f, 0f, markerRotationSpeed);
        }
        else
        {
            spriteRenderer.sprite = sprites[0];
            Vector2 arrowVector = new Vector2(target.x - transform.parent.position.x, target.y - transform.parent.position.y);
            arrowVector.Normalize();
            arrowVector *= arrowPositionConstant * Camera.main.orthographicSize;

            transform.localScale = Vector3.one * arrowScaleConstant * Camera.main.orthographicSize;
            transform.localPosition = new Vector3(arrowVector.x, arrowVector.y, 8);

            float arrowAngle = ((Vector2.SignedAngle(arrowVector, Vector2.right) * -1) + 360) % 360 - 90;
            transform.rotation = Quaternion.Euler(0, 0, arrowAngle);
        }
    }


    private bool targetIsInFrame()
    {
        float cameraWidthY = Camera.main.orthographicSize;
        float cameraWidthX = Camera.main.orthographicSize * 16 / 9;

        if(Mathf.Abs(target.x - transform.parent.position.x) < cameraWidthX &&
           Mathf.Abs(target.y - transform.parent.position.y) < cameraWidthY)
        {
            return true;
        }
        return false;
    }
}
