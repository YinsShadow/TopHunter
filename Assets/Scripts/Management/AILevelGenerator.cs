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
    private float playerPerformance = 0f;

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

        int roomCount = 5;//Random.Range(2, 4);

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

    void AdjustDifficulty()
    {
        if (playerPerformance > 2f)
            difficulty += 0.2f;
        else if (playerPerformance < -2f)
            difficulty -= 0.2f;

        difficulty = Mathf.Clamp(difficulty, 0.5f, 2f);
    }

    public void OnRoomCompleted(float timeTaken, int damageTaken)
    {
        float score = 0f;

        score += (10f - timeTaken);
        score -= damageTaken;

        playerPerformance = score;

        AdjustDifficulty();
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

        // Move player
        PlayerController.Instance.transform.position = roomScript.playerSpawnPoint.position;

        // Apply AI difficulty + spawn enemies
        roomScript.SetDifficulty(difficulty);
        roomScript.GenerateContents();

        UIFade.Instance.FadeToClear();
    }
}
