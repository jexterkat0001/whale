using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class menuScript : MonoBehaviour
{
    public Misc misc;

	public TextMeshProUGUI moneyText;
    public TextMeshProUGUI dockButtonText;
    public shipMovementScript shipMovementScript;
    public GameObject menuButton;
    public GameObject islandMenu;

	private float money = 0;

    public float distance;
    public float value;
    public int whalesHit = 0;

    public void Start()
    {
        if(misc.testMode)
        {
            islandMenu.SetActive(false);
            menuButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Open Menu";
        }
    }

    public void delivery()
    {
        moneyText.text = money + "$";
    }

    public void dockButtonPressed()
    {
        if(shipMovementScript.canDock)
        {
            shipMovementScript.dock();
            islandMenu.SetActive(true);
            menuButton.SetActive(true);
            dockButtonText.text = "Leave Port";
        }
        else if(shipMovementScript.docked)
        {
            shipMovementScript.undock();
            islandMenu.SetActive(false);
            menuButton.SetActive(false);
            dockButtonText.text = "Enter Port";

        }
    }

    public void menuButtonPressed()
    {
        if(islandMenu.activeInHierarchy)
        {
            islandMenu.SetActive(false);
            menuButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Open Menu";

        }
        else
        {
            islandMenu.SetActive(true);
            menuButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Close Menu";
        }
    }



    

}
