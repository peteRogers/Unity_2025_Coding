using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject objectToSpawn; // Prefab to spawn
    public float minSpawnInterval = 0.5f; // Minimum time between spawns
    public float maxSpawnInterval = 2.0f; // Maximum time between spawns

    [Header("Spawn Position Range")]
    public float minX = 0f; // Minimum X position
    public float maxX = 1f; // Maximum X position
    public float minZ = 0f; // Minimum Z position
    public float maxZ = 1f; // Maximum Z position

    [Header("Spawn Y Position")]
    public float spawnY = 0f; // Fixed Y position for spawning

    void Start()
    {
        // Start spawning objects continuously
        StartCoroutine(SpawnObjectsRandomly());
    }

    IEnumerator SpawnObjectsRandomly()
    {
        while (true)
        {
            // Randomize spawn interval
            float spawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);

            // Randomize position
            Vector3 randomPosition = new Vector3(
                Random.Range(minX, maxX),
                spawnY,
                Random.Range(minZ, maxZ)
            );

            // Randomize rotation
            Quaternion randomRotation = Random.rotation;

            // Spawn the object
            Instantiate(objectToSpawn, randomPosition, randomRotation);

            // Wait for the next spawn
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
