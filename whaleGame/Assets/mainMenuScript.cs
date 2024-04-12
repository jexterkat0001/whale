using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenuScript : MonoBehaviour
{
    public GameObject mainButtons;
    public GameObject infoScreen;

    public void play()
    {
        SceneManager.LoadScene("game");
    }
    public void quit()
    {
        Application.Quit();
    }
    public void info()
    {
        mainButtons.SetActive(false);
        infoScreen.SetActive(true);
    }
    public void back()
    {
        mainButtons.SetActive(true);
        infoScreen.SetActive(false);
    }
}
