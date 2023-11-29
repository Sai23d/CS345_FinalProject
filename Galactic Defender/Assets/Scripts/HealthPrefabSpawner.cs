using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPrefabSpawner : MonoBehaviour
{
    public GameObject healthPrefab; // Assign this from the editor
    public float spawnInterval = 5f; // Time in seconds between spawns
    public Vector2 spawnAreaMin; // The minimum x and y coordinates for spawn
    public Vector2 spawnAreaMax; // The maximum x and y coordinates for spawn

    private float timer;

    private void Start()
    {
        timer = spawnInterval; // Start the timer
    }

    private void Update()
    {
        timer -= Time.deltaTime; // Decrement the timer

        if (timer <= 0)
        {
            SpawnHealth();
            timer = spawnInterval; // Reset the timer
        }
    }

    void SpawnHealth()
    {
        // Generate a random x position within the spawn area
        float randomX = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
        Vector2 spawnPosition = new Vector2(randomX, spawnAreaMax.y);

        // Instantiate the health prefab at the random position above the screen
        Instantiate(healthPrefab, spawnPosition, Quaternion.identity);
    }
}
