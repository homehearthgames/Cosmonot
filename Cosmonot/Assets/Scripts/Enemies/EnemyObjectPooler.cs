using System.Collections.Generic;
using UnityEngine;

public class EnemyObjectPooler : MonoBehaviour
{
    // Declare a list of enemy prefabs that can be set in the inspector
    public List<GameObject> enemyPrefabs;
    
    // Declare a list of integers to store the number of enemies to create for each prefab
    public List<int> enemyCounts;

    // Declare a dictionary to store the object pools for each enemy prefab
    public Dictionary<GameObject, Queue<GameObject>> enemyPoolDictionary;

    void Start()
    {
        // Initialize the dictionary
        enemyPoolDictionary = new Dictionary<GameObject, Queue<GameObject>>();

        // For each enemy prefab in the list...
        for (int i = 0; i < enemyPrefabs.Count; i++)
        {
            GameObject enemyPrefab = enemyPrefabs[i];
            int enemyCount = enemyCounts[i];

            // Create a new game object to serve as the object pool for this enemy prefab
            GameObject enemyPool = new GameObject(enemyPrefab.name + " Pool");
            enemyPool.transform.parent = transform; // Set the parent to the game object that this script is attached to

            // Create a new queue for the object pool
            Queue<GameObject> poolQueue = new Queue<GameObject>();

            // Add the specified number of inactive enemies to the object pool
            for (int j = 0; j < enemyCount; j++)
            {
                GameObject enemy = Instantiate(enemyPrefab, enemyPool.transform);
                enemy.SetActive(false);
                poolQueue.Enqueue(enemy);
            }

            // Add the object pool to the dictionary
            enemyPoolDictionary.Add(enemyPrefab, poolQueue);
        }
    }

    // Method to retrieve an enemy from the object pool
    public GameObject GetEnemyFromPool(GameObject enemyPrefab)
    {
        // Check if the object pool for the given enemy prefab exists in the dictionary
        if (enemyPoolDictionary.ContainsKey(enemyPrefab))
        {
            // If the object pool is not empty, retrieve the next enemy from the queue
            if (enemyPoolDictionary[enemyPrefab].Count > 0)
            {
                GameObject enemy = enemyPoolDictionary[enemyPrefab].Dequeue();
                enemy.SetActive(true);
                return enemy;
            }
        }

        // If the object pool is empty or does not exist, instantiate a new enemy and return it
        return Instantiate(enemyPrefab);
    }
}
