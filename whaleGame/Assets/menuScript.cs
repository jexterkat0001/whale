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
    private float money = 100000;

    public TextMeshProUGUI dockButtonText;
    public shipScript shipScript;
    public GameObject menuButton;
    public GameObject islandMenu;
    public GameObject earningsTab;
    public GameObject shipUpgradeTab;
    public GameObject targetTabs;

    //Earnings
    [SerializeField] private int whaleHitPenalty;
    public int cargoCapacity = 100;
    public int whalesHit = 0;

    private int[] cargoCapacities = { 100, 200, 400, 1000 };
    private int[] upgradePrices = {250, 1500, 7500};
    private float[] maxZoomOuts = {1f,1f,1f,1f};

    //Select Target
    public logicTargetScript logicTargetScript;
    public TextMeshProUGUI selectTargetButtonText;
    public GameObject selectTargetTab;
    public GameObject targetTab;
    public GameObject ship;

    public GameObject targetIsland;
    private GameObject previousIsland;
    private int chosenIsland;
    private GameObject[] islandChoices;
    private float[] distances;
    private float[] values;
    [SerializeField] private float distanceValueMultiplier;

    public void Start()
    {
        distances = new float[3];
        values = new float[3];
        islandMenu.SetActive(false);
        menuButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Open Menu";
        selectTargetTab.SetActive(true);
        earningsButtonPressed();
    }

    public void delivery()
    {
        GameObject islandImage1 = earningsTab.transform.GetChild(0).GetChild(0).gameObject;
        GameObject island1 = previousIsland;
        islandImage1.GetComponent<Image>().sprite = islandImages[island1.GetComponent<islandScript>().islandType];
        islandImage1.transform.rotation = getIslandImageRotation(island1);

        GameObject islandImage2 = earningsTab.transform.GetChild(1).GetChild(0).gameObject;
        GameObject island2 = islandChoices[chosenIsland];
        islandImage2.GetComponent<Image>().sprite = islandImages[island2.GetComponent<islandScript>().islandType];
        islandImage2.transform.rotation = getIslandImageRotation(island2);

        earningsTab.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = $"{island1.name} to {island2.name}";

        float moneyEarned = Mathf.Clamp(cargoCapacity * distances[chosenIsland] * values[chosenIsland] - (whalesHit*whaleHitPenalty), 0f, 99999f);
        money += moneyEarned;
        moneyText.text = money + "$";
        earningsTab.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = $"${cargoCapacity}\nx{distances[chosenIsland]}\nx{values[chosenIsland]}\n\n\n\n${moneyEarned}";
        earningsTab.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = $"\n\n\n\n-${whalesHit * whaleHitPenalty}";
        earningsTab.transform.GetChild(6).GetComponent<TextMeshProUGUI>().text = $"New Balance: {money}$";
        whalesHit = 0;

        selectTargetScreen();

        selectTargetTab.SetActive(true);
        targetTab.SetActive(false);
        selectTargetButtonText.text = "Select Target";
    }

    public void upgradeShipButtonPressed()
    {
        if(money > upgradePrices[shipScript.currentShipUpgrade])
        {
            money -= upgradePrices[shipScript.currentShipUpgrade];
            shipScript.upgradeShip();
            cargoCapacity = cargoCapacities[shipScript.currentShipUpgrade];

            for(int i = 0; i < 3; i++)
            {
                shipUpgradeTab.transform.GetChild(i).gameObject.SetActive(i == shipScript.currentShipUpgrade);
            }
        }
    }

    private void selectTargetScreen()
    {
        islandChoices = logicTargetScript.getTargetChoices();
        for (int i = 0; i < 3; i++)
        {
            GameObject islandPanel = selectTargetTab.transform.GetChild(i).gameObject;
            GameObject islandChoice = islandChoices[i];

            islandPanel.transform.GetChild(0).GetComponent<Image>().sprite = islandImages[islandChoice.GetComponent<islandScript>().islandType];
            islandPanel.transform.GetChild(0).rotation = getIslandImageRotation(islandChoice);

            distances[i] = Mathf.Round(misc.pythagorean(new Vector2(ship.transform.position.x, ship.transform.position.y), new Vector2(islandChoice.transform.position.x, islandChoice.transform.position.y))) / 100;
            values[i] = Random.Range(1f, 1f + (distances[i] * distanceValueMultiplier));
            islandPanel.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = $"{islandChoice.name} \nDistance: {distances[i]} km\nValue: {Mathf.Round(values[i] * distances[i] * 100) / 100}";
        }
    }

    public void dockButtonPressed()
    {
        if (shipScript.canDock)
        {
            shipScript.dock();
            islandMenu.SetActive(true);
            menuButton.SetActive(true);
            menuButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Close Menu";
            dockButtonText.text = "Leave Port";
            
            if(new Vector2(ship.transform.position.x, ship.transform.position.y) == new Vector2(targetIsland.transform.position.x, targetIsland.transform.position.y))
            {
                earningsButtonPressed();
                delivery();
            }
        }
        else if(shipScript.docked)
        {
            shipScript.undock();
            islandMenu.SetActive(false);
            menuButton.SetActive(false);
            dockButtonText.text = "Enter Port";

        }
    }

    public void menuButtonPressed()
    {
        if (islandMenu.activeInHierarchy)
        {
            islandMenu.SetActive(false);
            menuButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Open Menu";
        }
        else
        {
            islandMenu.SetActive(true);
            menuButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Close Menu";
            selectTargetScreen();
        }
    }

    public void earningsButtonPressed()
    {
        earningsTab.SetActive(true);
        shipUpgradeTab.SetActive(false);
        targetTabs.SetActive(false);
    }
    public void shipUpgradeButtonPressed()
    {
        earningsTab.SetActive(false);
        shipUpgradeTab.SetActive(true);
        targetTabs.SetActive(false);
    }
    public void selectTargetButtonPressed()
    {
        earningsTab.SetActive(false);
        shipUpgradeTab.SetActive(false);
        targetTabs.SetActive(true);
    }

    public void islandButtonPressed()
    {
        previousIsland = targetIsland;
        if (Input.mousePosition.x < (Screen.width / 2) - (375 * Screen.width / 3840)) { chosenIsland = 0; }
        else if (Input.mousePosition.x < (Screen.width / 2) + (375 * Screen.width / 3840)) { chosenIsland = 1; }
        else { chosenIsland = 2; }
        targetIsland = islandChoices[chosenIsland];

        GameObject islandChoice = islandChoices[chosenIsland];
        targetTab.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = islandImages[islandChoice.GetComponent<islandScript>().islandType];
        targetTab.transform.GetChild(0).GetChild(0).rotation = getIslandImageRotation(islandChoice);
        targetTab.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = $"{islandChoice.name} \nDistance: {distances[chosenIsland]} km\nValue Multiplier: x{values[chosenIsland]}";

        logicTargetScript.setTarget(new Vector2(targetIsland.transform.position.x, targetIsland.transform.position.y));

        selectTargetTab.SetActive(false);
        targetTab.SetActive(true);
        selectTargetButtonText.text = "Target";
    }

    private Quaternion getIslandImageRotation(GameObject island)
    {
        islandScript islandScript = island.transform.GetComponent<islandScript>();
        return Quaternion.Euler(0f,0f,island.transform.eulerAngles.z + islandScript.islands[islandScript.islandType].transform.eulerAngles.z);
    }
}
