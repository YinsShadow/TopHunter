using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour
{
    private bool isTriggered = false;

    // As I intended for the game 
    private Transform enemiesParent;

    private void Start()
    {
        // Find "Enemies" inside the same room
        enemiesParent = transform.parent.Find("Enemies");
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (isTriggered) return;

        if (other.GetComponent<PlayerController>() && AreAllEnemiesDead()) 
        {
            isTriggered = true;
            StartCoroutine(TransitionRoutine());
        }
    }

    private bool AreAllEnemiesDead()
    {
        return enemiesParent == null || enemiesParent.childCount == 0;
    }

    // Dev/Test mode
    // private void OnTriggerEnter2D(Collider2D other) 
    // {
    //     if (isTriggered) return;

    //     if (other.GetComponent<PlayerController>()) 
    //     {
    //         isTriggered = true;
    //         StartCoroutine(TransitionRoutine());
    //     }
    // }
    //
    private IEnumerator TransitionRoutine()
    {
        // Wait for fade to black to finish
        yield return StartCoroutine(UIFade.Instance.FadeToBlackCoroutine());

        // Spawn next room
        LevelGenerator.Instance.LoadNextRoom();

        // Wait a tiny bit to ensure room is ready
        yield return null;

        // Fade back in
        yield return StartCoroutine(UIFade.Instance.FadeToClearCoroutine());
    }
}