using UnityEngine;

public class SpawnPrefab : MonoBehaviour
{
    public GameObject prefab; // Assign in Inspector
    public Transform spawnPoint; // Optional: Set a specific spawn point

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Change KeyCode to any desired key
        {
            Spawn();
        }
    }

    void Spawn()
    {
        if (prefab != null)
        {
            Vector3 position = spawnPoint != null ? spawnPoint.position : Vector3.zero; // Use spawnPoint or default to (0,0,0)
            Instantiate(prefab, position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Prefab is not assigned!");
        }
    }
}