using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public static LevelGenerator Instance;

    public GameObject[] roomPrefabs;
    public GameObject startRoom;
    public GameObject endRoom;
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

        int roomCount = 3;//Random.Range(2, 4);

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
