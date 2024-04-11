using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class menuScript : MonoBehaviour
{
    public Sprite[] islandImages;
    public Misc misc;
    public logicTutorialScript logicTutorialScript;

	public TextMeshProUGUI moneyText;
    private int money = 0;

    public TextMeshProUGUI dockButtonText;
    public shipScript shipScript;
    public GameObject menuButton;
    public GameObject islandMenu;
    public GameObject earningsTab;
    public GameObject shipUpgradeTab;
    public GameObject targetTabs;

    public GameObject distanceText;

    //Earnings
    public GameObject earningsButton;
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

    private bool reachedTarget = true;
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
        selectTargetButtonPressed();
        StartCoroutine(setDistanceText());
    }

    IEnumerator setDistanceText()
    {
        for (; ; )
        {
            if (reachedTarget)
            {
                distanceText.SetActive(false);
            }
            else
            {
                distanceText.SetActive(true);
                distanceText.GetComponent<TextMeshProUGUI>().text = $"{Mathf.Round(misc.pythagorean(new Vector2(ship.transform.position.x, ship.transform.position.y), new Vector2(targetIsland.transform.position.x, targetIsland.transform.position.y))) / 100} km from target";
            }
            yield return new WaitForSeconds(0.1f);
        }
    }


    public void delivery()
    {
        logicTutorialScript.nextTutorialStage();

        earningsButton.SetActive(true);

        GameObject islandImage1 = earningsTab.transform.GetChild(0).GetChild(0).gameObject;
        GameObject island1 = previousIsland;
        islandImage1.GetComponent<Image>().sprite = islandImages[island1.GetComponent<islandScript>().islandType];
        islandImage1.transform.rotation = getIslandImageRotation(island1);

        GameObject islandImage2 = earningsTab.transform.GetChild(1).GetChild(0).gameObject;
        GameObject island2 = targetIsland;
        islandImage2.GetComponent<Image>().sprite = islandImages[island2.GetComponent<islandScript>().islandType];
        islandImage2.transform.rotation = getIslandImageRotation(island2);

        earningsTab.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = $"{island1.name} to {island2.name}";

        int moneyEarned = Mathf.RoundToInt(Mathf.Clamp(cargoCapacity * distances[chosenIsland] * values[chosenIsland] - (whalesHit*whaleHitPenalty), 0f, 99999f));
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
        reachedTarget = true;
    }

    public void upgradeShipButtonPressed()
    {
        if (money > upgradePrices[shipScript.currentShipUpgrade] || (shipScript.currentShipUpgrade == 0 && logicTutorialScript.stage > -1))
        {
            money -= upgradePrices[shipScript.currentShipUpgrade];
            moneyText.text = money + "$";
            shipScript.upgradeShip();
            cargoCapacity = cargoCapacities[shipScript.currentShipUpgrade];

            for(int i = 0; i < 3; i++)
            {
                shipUpgradeTab.transform.GetChild(i).gameObject.SetActive(i == shipScript.currentShipUpgrade);
            }

            selectTargetScreen();
        }
    }

    public void selectTargetScreen(bool getClosestIslands = false)
    {
        islandChoices = logicTargetScript.getTargetChoices(getClosestIslands);
        for (int i = 0; i < 3; i++)
        {
            GameObject islandPanel = selectTargetTab.transform.GetChild(i).gameObject;
            GameObject islandChoice = islandChoices[i];

            islandPanel.transform.GetChild(0).GetComponent<Image>().sprite = islandImages[islandChoice.GetComponent<islandScript>().islandType];
            islandPanel.transform.GetChild(0).rotation = getIslandImageRotation(islandChoice);

            distances[i] = Mathf.Round(misc.pythagorean(new Vector2(ship.transform.position.x, ship.transform.position.y), new Vector2(islandChoice.transform.position.x, islandChoice.transform.position.y))) / 100;
            values[i] = Mathf.Round(Random.Range(1f, 1f + (distances[i] * distanceValueMultiplier)) * 100) / 100;
            islandPanel.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = $"{islandChoice.name} \nDistance: {distances[i]} km\nValue: {Mathf.Round(values[i] * distances[i] * 100) / 100}";
        }
        if(!logicTutorialScript.canSelectTarget)
        {
            islandButtonPressed();
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

            if (new Vector2(ship.transform.position.x, ship.transform.position.y) == new Vector2(targetIsland.transform.position.x, targetIsland.transform.position.y) && previousIsland != null && !reachedTarget)
            {
                earningsButtonPressed();
                delivery();
            }
        }
        else if (shipScript.docked)
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
        reachedTarget = false;

        GameObject islandChoice = islandChoices[chosenIsland];
        targetTab.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = islandImages[islandChoice.GetComponent<islandScript>().islandType];
        targetTab.transform.GetChild(0).GetChild(0).rotation = getIslandImageRotation(islandChoice);
        targetTab.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = $"{islandChoice.name} \nDistance: {distances[chosenIsland]} km\nValue: {Mathf.Round(values[chosenIsland] * distances[chosenIsland] * 100) / 100}";

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
