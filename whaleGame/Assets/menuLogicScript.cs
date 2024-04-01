using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuLogicScript : MonoBehaviour
{
    public void play()
    {
        SceneManager.LoadScene("game");
    }
}
