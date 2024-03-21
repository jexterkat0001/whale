using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class shipLogicScript : MonoBehaviour
{
    public Rigidbody2D boatRigidbody;
    public logicTargetScript logicTargetScript;
    public shipMovementScript shipMovementScript;
    public TextMeshProUGUI dockText;

    private Collider2D dockTrigger;
    private bool canDock = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(canDock)
        {
            dockText.text = "Press E to enter port";
            if(Input.GetKeyDown(KeyCode.E))
            {
                transform.position = dockTrigger.gameObject.transform.position;
                transform.rotation = dockTrigger.gameObject.transform.rotation; 
                boatRigidbody.velocity = Vector2.zero;
                shipMovementScript.rotationSpeed = 0;
                shipMovementScript.docked = true;
                logicTargetScript.attemptGetNewTarget();
                canDock = false;
            }
        }
        else if(shipMovementScript.docked)
        {
            dockText.text = "Press E to leave port";
            if(Input.GetKeyDown(KeyCode.E))
            {
                shipMovementScript.docked = false;
                canDock = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        dockTrigger = collision;
        if(Time.time > 1)
        {
            canDock = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        canDock = false;
        dockText.text = "";
    }
}
