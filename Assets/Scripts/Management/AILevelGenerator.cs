using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AILevelGenerator : MonoBehaviour
{
    public static AILevelGenerator Instance;

    public GameObject[] roomPrefabs;
    public GameObject startRoom;
    public GameObject endRoom;
    public Transform roomSpawnPoint;

    private int currentRoomIndex = -1;
    private List<GameObject> generatedRooms = new List<GameObject>();
    private GameObject currentRoom;
    private float difficulty = 1f;
    // private float playerPerformance = 2f; old attempt

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GenerateLevel();
        LoadNextRoom();
    }

    void ShuffleList(List<GameObject> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            GameObject temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    void GenerateLevel()
    {
        generatedRooms.Clear();

        int roomCount = Random.Range(5, 7);

        // de first room is start room
        generatedRooms.Add(startRoom);

        // makin a temporary pool, don’t modify the original array!
        List<GameObject> roomPool = new List<GameObject>(roomPrefabs);

        // Shuffle the pool (no order allowed!)
        ShuffleList(roomPool);

        // Add unique rooms (no repeats)
        for (int i = 0; i < roomCount - 1; i++)
        {
            generatedRooms.Add(roomPool[i]);
        }

        generatedRooms.Add(endRoom);
    }

    // void AdjustDifficulty() // difficulty scaling testing
    // {
    //     if (playerPerformance > 2f)
    //         difficulty += 0.2f;
    //     else if (playerPerformance < -2f)
    //         difficulty -= 0.2f;

    //     difficulty = Mathf.Clamp(difficulty, 0.5f, 2f);
    // }

    // public void OnRoomCompleted(float timeTaken, int damageTaken)
    // {
    //     float score = 0f;

    //     score += (10f - timeTaken);
    //     score -= damageTaken;

    //     playerPerformance = score;

    //     AdjustDifficulty();
    // }

    public void OnRoomCompleted(int hitsTaken)
    {
        if (hitsTaken == 0)
        {
            difficulty += 0.2f; // player did good = make harder
        }
        else if (hitsTaken == 1)
        {
            // no change
        }
        else
        {
            difficulty -= 0.2f; // player struggled = make easier
        }

        difficulty = Mathf.Clamp(difficulty, 0.5f, 2f);

        Debug.Log("Hits: " + hitsTaken + " | Difficulty: " + difficulty);
    }

    public void LoadNextRoom()
    {
        if (currentRoom != null)
            Destroy(currentRoom);

        currentRoomIndex++;

        if (currentRoomIndex >= generatedRooms.Count)
        {
            Debug.Log("Level Complete!");
            return;
        }

        currentRoom = Instantiate(generatedRooms[currentRoomIndex], roomSpawnPoint.position, Quaternion.identity);

        AIRoom roomScript = currentRoom.GetComponent<AIRoom>();

        //Debug.Log("Player: " + PlayerController.Instance);
        //Debug.Log("Room Script: " + roomScript);
        //Debug.Log("Spawn Point: " + roomScript.playerSpawnPoint); //This was to find a missing value assignment (it was a wrong script)
        // Move player
        PlayerController.Instance.transform.position = roomScript.playerSpawnPoint.position;

        // Apply AI difficulty + spawn enemies
        roomScript.SetDifficulty(difficulty);
        roomScript.GenerateContents();

        UIFade.Instance.FadeToClear();
    }
}
