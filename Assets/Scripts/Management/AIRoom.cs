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

    private float difficulty = 0.8f;

    // Called in AILevelGenerator
    public void SetDifficulty(float value)
    {
        difficulty = value;
    }

    public void GenerateContents()
    {
        List<Transform> availableSpawns = new List<Transform>(enemySpawnPoints);

        int enemyCount;

        if (difficulty <= 0.8f)
        {
            enemyCount = Random.Range(1, 3); // easy
        }
        else if (difficulty <= 1.3f)
        {
            enemyCount = Random.Range(2, 4); // normal
        }
        else
        {
            enemyCount = Random.Range(4, 6); // hard ( capped due to spawn zone limits )
        }

        enemyCount = Mathf.Clamp(enemyCount, 1, 5); //max and min spawns per room

        for (int i = 0; i < enemyCount && availableSpawns.Count > 0; i++)
        {
            int index = Random.Range(0, availableSpawns.Count);

            Transform spawnPoint = availableSpawns[index];
            availableSpawns.RemoveAt(index);

            int maxIndex;

            if (difficulty <= 0.8f)
            {
                maxIndex = 0; // easy enemy only
            }
            else if (difficulty <= 1.3f)
            {
                maxIndex = 1; // easy enemy + normal enemy
            }
            else
            {
                maxIndex = 2; // easy enemy + normal enemy + hard enemy
            }

            GameObject enemy = enemyPrefabs[Random.Range(0, maxIndex + 1)];

            //GameObject enemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)]; //random enemy spawn

            Instantiate(enemy, spawnPoint.position, Quaternion.identity, enemiesParent);
        }
    }
}