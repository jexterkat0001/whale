using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class menuScript : MonoBehaviour
{
	public TextMeshProUGUI moneyText;
    public shipMovementScript shipMovementScript;

	private float money = 0;

    public float distance;
    public float value;
    public int whalesHit = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void dockButton()
    {
        if(shipMovementScript.canDock)
        {
            shipMovementScript.dock();
        }
        else if(shipMovementScript.docked)
        {
            shipMovementScript.undock();
        }
    }

    //public void delivery()
    //{

    //}
}
