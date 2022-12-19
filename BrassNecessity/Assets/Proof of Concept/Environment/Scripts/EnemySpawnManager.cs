using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public float totalEnemies = 20f;      // Total number of enemies in the level
    public float maxConcurrentEnemies = 2f;    // Total number of enemies that should be present in the level at any time
    //public EnemySpawner[] enemySpawners;     // Array listing all the EnemySpawn
    public GameObject[] spawnPoints;       // Each game object marks the location of a spawnPoint
    public GameObject[] enemyPrefabs;      


    private void Awake()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.Log("EnemySpawnManager does not have any SpawnPoints specified.");
        }



    }


}
