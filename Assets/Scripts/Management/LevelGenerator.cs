using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public static LevelGenerator Instance;

    public GameObject[] roomPrefabs;
    public Transform roomSpawnPoint;

    private int currentRoomIndex = -1;
    private List<GameObject> generatedRooms = new List<GameObject>();
    private GameObject currentRoom;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GenerateLevel();
        LoadNextRoom();
    }

    void GenerateLevel()
    {
        int roomCount = Random.Range(3, 6);

        for (int i = 0; i < roomCount; i++)
        {
            GameObject room = roomPrefabs[Random.Range(0, roomPrefabs.Length)];
            generatedRooms.Add(room);
        }
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

        Room roomScript = currentRoom.GetComponent<Room>();

        // Move player
        PlayerController.Instance.transform.position = roomScript.playerSpawnPoint.position;

        UIFade.Instance.FadeToClear();
    }

    
}
