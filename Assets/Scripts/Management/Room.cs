using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public Transform playerSpawnPoint;

    private void Start()
    {
        GenerateContents();
    }

    void GenerateContents()
    {
        // add enemy + loot logic here later
    }
}