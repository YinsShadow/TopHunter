using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour
{
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

        // Spawn next room
        LevelGenerator.Instance.LoadNextRoom();

        // Wait a tiny bit to ensure room is ready
        yield return null;

        // Fade back in
        yield return StartCoroutine(UIFade.Instance.FadeToClearCoroutine());
    }
}