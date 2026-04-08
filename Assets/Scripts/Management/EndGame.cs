using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    const string Menu_Scene = "MainMenu";

    private bool isTriggered = false;


    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (isTriggered) return;

        if (other.GetComponent<PlayerController>()) 
        {
            isTriggered = true;
            StartCoroutine(TransitionRoutine());
        }
    }

    private IEnumerator TransitionRoutine()
    {
        // Wait for fade to black to finish
        yield return StartCoroutine(UIFade.Instance.FadeToBlackCoroutine());

        PlayerController player = FindObjectOfType<PlayerController>();
        if (player != null)
            Destroy(player.gameObject);
        Destroy(gameObject); 
        SceneManager.LoadScene(Menu_Scene); 
        Destroy(GameObject.Find("UICanvas"));
    }
    
}