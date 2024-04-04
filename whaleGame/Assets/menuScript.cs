using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class menuScript : MonoBehaviour
{
	public TextMeshProUGUI moneyText;
    public TextMeshProUGUI dockButtonText;
    public shipMovementScript shipMovementScript;
    public GameObject openMenuButtonGameObject;
    public GameObject islandMenu;

	private float money = 0;

    public float distance;
    public float value;
    public int whalesHit = 0;

    public void Start()
    {
        islandMenu.SetActive(false);
    }

    public void delivery()
    {

    }

    public void dockButton()
    {
        if(shipMovementScript.canDock)
        {
            shipMovementScript.dock();
            islandMenu.SetActive(true);
            openMenuButtonGameObject.SetActive(true);
            dockButtonText.text = "Leave Port";
        }
        else if(shipMovementScript.docked)
        {
            shipMovementScript.undock();
            openMenuButtonGameObject.SetActive(false);
            dockButtonText.text = "Enter Port";
        }
    }

    public void openMenuButton()
    {
        islandMenu.SetActive(true);
    }

    public void menuBackButton()
    {
        islandMenu.SetActive(false);
    }



    

}
