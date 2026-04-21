using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour
{
    //public Transform enemiesParent;

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
            //AILevelGenerator.Instance.OnRoomCompleted(5f, 1); // old

            int hits = PlayerHealth.Instance.GetHitsTaken();

            AILevelGenerator.Instance.OnRoomCompleted(hits);

            // reset for next room
            PlayerHealth.Instance.ResetHits();

            isTriggered = true;
            StartCoroutine(TransitionRoutine());
        }
    }

    private bool AreAllEnemiesDead()
    {
        return enemiesParent == null || enemiesParent.childCount == 0;
    }

    // Dev/Test mode (Don't have to kill to move on)
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

    private void LoadNext()
    {
        if (AILevelGenerator.Instance != null)
        {
            AILevelGenerator.Instance.LoadNextRoom();
        }
        else if (LevelGenerator.Instance != null)
        {
            LevelGenerator.Instance.LoadNextRoom();
        }
    }
    private IEnumerator TransitionRoutine()
    {
        // Wait for fade to black to finish
        yield return StartCoroutine(UIFade.Instance.FadeToBlackCoroutine());

        // Spawn next room
        //LevelGenerator.Instance.LoadNextRoom(); //old
        LoadNext();

        // Wait a tiny bit to ensure room is ready
        yield return null;

        // Fade back in
        yield return StartCoroutine(UIFade.Instance.FadeToClearCoroutine());
    }
}