using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boatScript : MonoBehaviour
{
    public Rigidbody2D boatRigidbody;
    public logicTargetScript logicTargetScript;

    [SerializeField]
    private float acceleration;
    [SerializeField]
    private float maxSpeed;
    private float speed;
    [SerializeField]
    private float deceleration;

    [SerializeField]
    private float rotationalAcceleration;
    [SerializeField]
    private float maxRotationSpeed;
    private float rotationSpeed = 0;
    [SerializeField]
    private float rotationalDeceleration;

    [SerializeField]
    private bool testMode;

    private bool docked = true;

    // Start is called before the first frame update
    void Start()
    {
        if(testMode)
        {
            acceleration = 5f;
            maxSpeed = 50f;
            deceleration = 10f;
            rotationalAcceleration = 0.5f;
            rotationalDeceleration = 0.9f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(docked)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                docked = false;
            }
        }
        else
        {
            shipMovement();
        }
    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        transform.position = collision.gameObject.transform.parent.position;
        transform.rotation = collision.gameObject.transform.parent.rotation;
        boatRigidbody.velocity = Vector2.zero;
        rotationSpeed = 0;
        docked = true;
        logicTargetScript.attemptGetNewTarget();
    }

    private void shipMovement()
    {
        speed = boatRigidbody.velocity.magnitude;
        float velocityAngle = ((Vector2.SignedAngle(boatRigidbody.velocity, Vector2.right) * -1) + 360) % 360;
        float currentAngle = (transform.eulerAngles.z + 360) % 360;
        if (Mathf.Abs(velocityAngle - currentAngle) > 90 && Mathf.Abs(velocityAngle - currentAngle) < 270)
        {
            speed *= -1;
        }

        if (Input.GetKey(KeyCode.W))
        {
            speed += acceleration * Time.deltaTime;
            if (speed > maxSpeed)
            {
                speed = maxSpeed;
            }
        }
        if (Input.GetKey(KeyCode.S))
        {
            speed -= acceleration * Time.deltaTime;
            if (speed < -maxSpeed)
            {
                speed = -maxSpeed;
            }
        }
        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            if (speed > 0)
            {
                speed -= deceleration * Time.deltaTime;
            }
            else if (speed < 0)
            {
                speed += deceleration * Time.deltaTime;
            }
        }

        if (Input.GetKey(KeyCode.A))
        {
            rotationSpeed += rotationalAcceleration * Time.deltaTime;
            if (rotationSpeed > maxRotationSpeed)
            {
                rotationSpeed = maxRotationSpeed;
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            rotationSpeed -= rotationalAcceleration * Time.deltaTime;
            if (rotationSpeed < -maxRotationSpeed)
            {
                rotationSpeed = -maxRotationSpeed;
            }
        }
        if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            rotationSpeed *= rotationalDeceleration;
        }
        transform.Rotate(0f, 0f, rotationSpeed);

        boatRigidbody.velocity = new Vector2(speed * Mathf.Cos(currentAngle * Mathf.PI / 180), speed * Mathf.Sin(currentAngle * Mathf.PI / 180));
    }
}
