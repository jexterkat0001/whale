using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boatScript : MonoBehaviour
{
    public Rigidbody2D boatRigidbody;

    [SerializeField]
    private float acceleration;
    [SerializeField]
    private float maxSpeed;
    private float speed;

    [SerializeField]
    private float rotationalAcceleration;
    [SerializeField]
    private float maxRotationSpeed;
    private float rotationSpeed = 0;

    [SerializeField]
    private float deceleration;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        speed = boatRigidbody.velocity.magnitude;
        float velocityAngle = ((Vector2.SignedAngle(boatRigidbody.velocity, Vector2.right)*-1)+360)%360;
        float currentAngle = (transform.eulerAngles.z + 360)%360;
        if (Mathf.Abs(velocityAngle-currentAngle) > 90 && Mathf.Abs(velocityAngle - currentAngle) < 270)
        {
            speed *= -1;
        }
        

        if (Input.GetKey(KeyCode.W))
        {
            speed += acceleration * Time.deltaTime;
            if(speed > maxSpeed)
            {
                speed = maxSpeed;
            }
        }
        if(Input.GetKey(KeyCode.S))
        {
            speed -= acceleration * Time.deltaTime;
            if(speed < -maxSpeed)
            {
                speed = -maxSpeed;
            }
        }
        if(!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            speed *= deceleration;
        }


        if (Input.GetKey(KeyCode.A))
        {
            rotationSpeed += rotationalAcceleration * Time.deltaTime;
            if(rotationSpeed > maxRotationSpeed)
            {
                rotationSpeed = maxRotationSpeed;
            }
        }
        if(Input.GetKey(KeyCode.D))
        {
            rotationSpeed -= rotationalAcceleration * Time.deltaTime;
            if(rotationSpeed < -maxRotationSpeed)
            {
                rotationSpeed = -maxRotationSpeed;
            }
        }
        if(!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            rotationSpeed *= deceleration;
        }
        transform.Rotate(0f,0f,rotationSpeed);

        boatRigidbody.velocity = new Vector2(speed * Mathf.Cos(currentAngle*Mathf.PI/180), speed * Mathf.Sin(currentAngle*Mathf.PI/180));
        
    
    }
}
