using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shipMovementScript : MonoBehaviour
{
    public Rigidbody2D boatRigidbody;
    public logicTargetScript logicTargetScript;
    public GameObject dockButton;
    public Misc misc;

    private Collider2D dockTrigger;

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

    public bool docked;
    public bool canDock;

    // Start is called before the first frame update
    void Start()
    {
        if(misc.testMode)
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
        if(!docked)
        {
            shipMovement();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        dockTrigger = collision;
        if (Time.time > 0.5f)
        {
            canDock = true;
            dockButton.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        canDock = false;
        dockButton.SetActive(false);
    }

    public void dock()
    {
        transform.position = dockTrigger.gameObject.transform.position;
        transform.rotation = dockTrigger.gameObject.transform.rotation;
        boatRigidbody.velocity = Vector2.zero;
        rotationSpeed = 0;
        docked = true;
        canDock = false;
    }

    public void undock()
    {
        docked = false;
        canDock = true;
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
                if(speed < 0)
                {
                    speed = 0;
                }
            }
            else if (speed < 0)
            {
                speed += deceleration * Time.deltaTime;
                if(speed > 0)
                {
                    speed = 0;
                }
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
        if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && rotationSpeed != 0f)
        {
            rotationSpeed *= rotationalDeceleration;
            if(Mathf.Abs(rotationSpeed) < 0.001)
            {
                rotationSpeed = 0f;
            }
        }
        transform.Rotate(0f, 0f, rotationSpeed);

        boatRigidbody.velocity = new Vector2(speed * Mathf.Cos(currentAngle * Mathf.PI / 180), speed * Mathf.Sin(currentAngle * Mathf.PI / 180));
    }
}
