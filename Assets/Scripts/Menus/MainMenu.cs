using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Game"); // game scene name to load
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game"); // works for in editor mode

        Application.Quit(); // works when build done
    }
}
