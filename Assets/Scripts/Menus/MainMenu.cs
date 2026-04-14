using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGameNormal()
    {
        Debug.Log("Go Normal!"); 

        SceneManager.LoadScene("GameNormal"); 
    }

    public void StartGameAI()
    {
        Debug.Log("Go AI!"); 

        SceneManager.LoadScene("GameAI"); 
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game"); 

        Application.Quit(); 
    }
}
