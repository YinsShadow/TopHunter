using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIRoom : MonoBehaviour
{
    public Transform playerSpawnPoint;
    public Transform enemiesParent;

    [Header("Enemy Setup")]
    public GameObject[] enemyPrefabs;
    public Transform[] enemySpawnPoints;

    private float difficulty = 1f;

    // Called in AILevelGenerator
    public void SetDifficulty(float value)
    {
        difficulty = value;
    }

    public void GenerateContents()
    {
        List<Transform> availableSpawns = new List<Transform>(enemySpawnPoints);

        int enemyCount = Mathf.RoundToInt(Random.Range(2, availableSpawns.Count + 1) * difficulty);

        for (int i = 0; i < enemyCount && availableSpawns.Count > 0; i++)
        {
            int index = Random.Range(0, availableSpawns.Count);

            Transform spawnPoint = availableSpawns[index];
            availableSpawns.RemoveAt(index);

            GameObject enemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

            Instantiate(enemy, spawnPoint.position, Quaternion.identity, enemiesParent);
        }
    }
}