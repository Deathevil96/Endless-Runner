using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public GameObject platformPrefab; // Prefab for your platform
    public GameObject obstaclePrefab; // Prefab for obstacles
    public Transform playerCamera; // Reference to the player's camera
    public float platformSpawnDistance = 50f; // Distance ahead of the player to spawn platforms
    public float platformDestroyDistance = 30f; // Distance behind the player to destroy platforms
    public List<GameObject> obstaclePrefabs; // List of obstacle prefabs

    private List<GameObject> activePlatforms = new List<GameObject>();

    void Start()
    {
        SpawnInitialPlatforms();
    }

    void Update()
    {
        // Check if the first platform is out of the destroy distance range
        if (activePlatforms.Count > 0 && activePlatforms[0].transform.position.z < playerCamera.position.z - platformDestroyDistance)
        {
            DestroyPlatform(activePlatforms[0]);
            activePlatforms.RemoveAt(0);
        }

        // Check if the last platform is out of the spawn distance range
        if (activePlatforms.Count > 0 && activePlatforms[activePlatforms.Count - 1].transform.position.z < playerCamera.position.z + platformSpawnDistance)
        {
            SpawnPlatform();
        }
    }

    void SpawnInitialPlatforms()
    {
        for (int i = 0; i < 10; i++) // Spawn some initial platforms
        {
            SpawnPlatform();
        }
    }

    void SpawnPlatform()
    {
        Vector3 spawnPosition = Vector3.zero;

        if (activePlatforms.Count > 0)
        {
            Vector3 lastPlatformPosition = activePlatforms[activePlatforms.Count - 1].transform.position;
            spawnPosition = lastPlatformPosition + Vector3.forward * Random.Range(5f, 10f); // Adjust the spawn range as needed
        }

        GameObject newPlatform = Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
        GameObject obstaclePrefabToSpawn = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Count)];
        activePlatforms.Add(newPlatform);

        // Spawn obstacles randomly with the platforms
        if (Random.Range(0f, 1f) < 0.5f) // Adjust the probability as needed
        {
            SpawnObstacle(newPlatform, obstaclePrefabToSpawn);
        }
    }

    void SpawnObstacle(GameObject platform, GameObject obstaclePrefab)
    {
        float obstacleXPosition = Random.Range(-4.4f, 4.4f); // Obstacle position between -4.4 and 4.4
        Vector3 obstaclePosition = new Vector3(obstacleXPosition, 0.5f, platform.transform.position.z);
        GameObject newObstacle = Instantiate(obstaclePrefab, obstaclePosition, Quaternion.identity);
        newObstacle.transform.SetParent(platform.transform); // Set the platform as the parent of the obstacle
    }

    void DestroyPlatform(GameObject platform)
    {
        // Destroy all child obstacles first
        foreach (Transform child in platform.transform)
        {
            Destroy(child.gameObject);
        }

        // Then destroy the platform
        Destroy(platform);
    }
}