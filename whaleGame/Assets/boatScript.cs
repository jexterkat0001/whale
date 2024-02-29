using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boatScript : MonoBehaviour
{
    public Rigidbody2D boatRigidbody;

    [SerializeField]
    private float accelerationStrength;
    [SerializeField]
    private float accelerationMax;
    private float acceleration = 0;

    [SerializeField]
    private float rotationSpeedStrength;
    [SerializeField]
    private float rotationSpeedMax;
    private float rotationSpeed = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            acceleration = acceleration + accelerationStrength;
        }
        if(Input.GetKeyDown(KeyCode.S))
        {
            acceleration = acceleration - accelerationStrength;
        }

        float currentVelocity = boatRigidbody.velocity.magnitude;

        if(Input.GetKeyDown(KeyCode.A))
        {
            rotationSpeed = rotationSpeed + rotationSpeedStrength;
        }
        if(Input.GetKeyDown(KeyCode.D))
        {
            rotationSpeed = rotationSpeed - rotationSpeedStrength;
        }
        //Debug.Log(transform.eulerAngles.z);
        transform.Rotate(0f,0f,rotationSpeed);
        float currentAngle = transform.eulerAngles.z-90;

        //Debug.Log(Mathf.Cos(90*Mathf.PI/180));
        boatRigidbody.velocity = new Vector2(Mathf.Cos(currentAngle*Mathf.PI/180),Mathf.Sin(currentAngle*Mathf.PI/180));
        
    
    }
}
