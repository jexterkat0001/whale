using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class logicTutorialScript : MonoBehaviour
{
    public GameObject tutorialScreen;
    public menuScript menuScript;
    public GameObject upgradeShipButton;
    public GameObject tutorialYesButton;
    public GameObject tutorialNoButton;
    public GameObject dockButton;
    public GameObject menuButton;
    public GameObject tutorialOkButton;

    public int stage = -1;
    public bool canSelectTarget;
    private string[] tutorialText =
    {
        "Welcome to whale game. In this game, you sail your ship to deliver cargo between various islands. Click \"Leave Port\" to leave port. The ship is controlled with WASD keys. Sail toward your target island, indicated by the arrow. Be careful not to hit the whales who you share the ocean with! They are endangered, hitting them will get you a money penalty. Once you reach the target, click \"Enter Port\"  to enter port.",

        "Great job on your first delivery! I hope you didn't hit any whales...       On your next delivery, you get to pick your destination! Click \"Select Target\" to open the selection tab. Farther islands are more likely to have higher values, but carry greater risk of hitting whales. Choose wisely.",

        "You should hopefully have enough money to buy a better ship. If not, that's ok. You will now be in debt. Click \"Upgrade Ship\" and buy the Big Ship. Larger ships are faster and can carry more cargo, but their speed makes them more likely to hit whales. Do speed wisely. You can scroll to zoom out farther with this ship. It also comes with the ability to launch a whale spotter by pressing E. How does this technology work? Nobody knows.",

        "This is the end of the tutorial. I heard that the next ship upgrade includes a technology that shows you which areas have high whale density. How Exciting! Anyway, keep going and remember that the whales' lives are important. Never let the corrupting hand of capitalism get to you. Ok?"
    };


    [ContextMenu("next")]
    public void nextTutorialStage(bool ahsgdhjasd = false)
    {
        if (stage == 3 || (stage == -1 && !ahsgdhjasd)) { return; }
        stage++;
        tutorialScreen.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = tutorialText[stage];
        if (stage == 0)
        {
            menuScript.selectTargetScreen(true);
            canSelectTarget = false;
        }
        if (stage == 1)
        {
            canSelectTarget = true;
        }
        if (stage == 2)
        {
            upgradeShipButton.SetActive(true);
        }
        if (stage == 3)
        {
            tutorialOkButton.SetActive(true);
        }
    }

    public void yes()
    {
        nextTutorialStage(true);
        upgradeShipButton.SetActive(false);
        tutorialYesButton.SetActive(false);
        tutorialNoButton.SetActive(false);
        dockButton.SetActive(true);
        menuButton.SetActive(true);
    }
    public void no()
    {
        canSelectTarget = true;
        menuScript.selectTargetScreen();
        upgradeShipButton.SetActive(true);
        tutorialYesButton.SetActive(false);
        tutorialNoButton.SetActive(false);
        dockButton.SetActive(true);
        menuButton.SetActive(true);
        tutorialScreen.SetActive(false);
    }
    public void ok()
    {
        tutorialScreen.SetActive(false);
    }
}
