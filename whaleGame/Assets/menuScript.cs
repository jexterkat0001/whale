using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class menuScript : MonoBehaviour
{
    public Sprite[] islandImages;
    public Misc misc;

	public TextMeshProUGUI moneyText;
    private float money = 0;

    public TextMeshProUGUI dockButtonText;
    public shipMovementScript shipMovementScript;
    public GameObject menuButton;
    public GameObject islandMenu;

    public logicTargetScript logicTargetScript;
    public GameObject selectTargetTab;
    public GameObject targetTab;
    public GameObject ship;

    private int chosenIsland = -1;
    private GameObject[] islandChoices;
    private float[] distances;
    private float[] values;
    [SerializeField] private float distanceValueMultiplier;

    public int whalesHit = 0;

    public void Start()
    {
        distances = new float[3];
        values = new float[3];
        if (misc.testMode)
        {
            islandMenu.SetActive(false);
            menuButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Open Menu";
        }
    }

    public void delivery()
    {
        moneyText.text = money + "$";

        selectTargetScreen();

        selectTargetTab.SetActive(true);
        targetTab.SetActive(false);
    }

    private void selectTargetScreen()
    {
        islandChoices = logicTargetScript.getTargetChoices();
        for (int i = 0; i < 3; i++)
        {
            GameObject islandPanel = selectTargetTab.transform.GetChild(i).gameObject;
            GameObject islandChoice = islandChoices[i];

            islandPanel.transform.GetChild(0).GetComponent<Image>().sprite = islandImages[islandChoice.GetComponent<islandScript>().islandType];
            islandPanel.transform.GetChild(0).rotation = Quaternion.Euler(new Vector3(0f, 0f, islandChoice.transform.eulerAngles.z));

            distances[i] = misc.pythagorean(new Vector2(ship.transform.position.x, ship.transform.position.y), new Vector2(islandChoice.transform.position.x, islandChoice.transform.position.y))/10;
            values[i] = Mathf.Round(Random.Range(1f, 1f + (distances[i] * distanceValueMultiplier)) * 100) / 100;
            islandPanel.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = $"{islandChoice.name} \nDistance: {distances[i]} km\nValue Multiplier: x{values[i]}";
        }
    }

    public void islandButtonPressed()
    {
        if(Input.mousePosition.x < (Screen.width/2) - (375*Screen.width/3840)){chosenIsland = 0;}
        else if(Input.mousePosition.x < (Screen.width / 2) + (375 * Screen.width / 3840)){chosenIsland = 1;}
        else{chosenIsland = 2;}

        GameObject islandChoice = islandChoices[chosenIsland];
        targetTab.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = islandImages[islandChoice.GetComponent<islandScript>().islandType];
        targetTab.transform.GetChild(0).GetChild(0).rotation = Quaternion.Euler(new Vector3(0f, 0f, islandChoice.transform.eulerAngles.z));
        targetTab.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = $"{islandChoice.name} \nDistance: {distances[chosenIsland]} km\nValue Multiplier: x{values[chosenIsland]}";

        logicTargetScript.setTarget(new Vector2(islandChoices[chosenIsland].transform.position.x, islandChoices[chosenIsland].transform.position.y));

        selectTargetTab.SetActive(false);
        targetTab.SetActive(true);
    }

    public void dockButtonPressed()
    {
        if (shipMovementScript.canDock)
        {
            shipMovementScript.dock();
            islandMenu.SetActive(true);
            menuButton.SetActive(true);
            menuButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Close Menu";
            dockButtonText.text = "Leave Port";
            /*Debug.Log(new Vector2(ship.transform.position.x, ship.transform.position.y));
            Debug.Log(new Vector2(islandChoices[chosenIsland].transform.position.x, islandChoices[chosenIsland].transform.position.y));
            Debug.Log(chosenIsland);*/
            if(new Vector2(ship.transform.position.x, ship.transform.position.y) == new Vector2(islandChoices[chosenIsland].transform.position.x, islandChoices[chosenIsland].transform.position.y))
            {
                delivery();
            }
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
        if(chosenIsland == -1)
        {
            selectTargetScreen();
        }
        if (islandMenu.activeInHierarchy)
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
