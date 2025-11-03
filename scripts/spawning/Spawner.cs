using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject prefabToSpawn;  // Reference to the prefab you want to spawn
    public int numberOfPrefabs = 10;   // Number of prefabs to spawn
    //public Transform parentObject;

    void Start()
    {
        SpawnPrefabs();
    }

    // Update is called once per frame
    void Update()
    {
    //     if (Input.GetKeyDown(KeyCode.Space))
    // {
    //     // Send the "OnMessageReceived" method to this object and all of its children.
    //     BroadcastMessage("OnMessageReceived", "Hello from parent!", SendMessageOptions.DontRequireReceiver);
    // }
    }

        private void SpawnPrefabs()
    {
        for (int x = 0; x < numberOfPrefabs; x++){
             for (int y = 0; y < numberOfPrefabs; y++){
                Vector3 spawnPos = new Vector3(x * 2, 0, y*2);
                
                GameObject spawnedObject = Instantiate(prefabToSpawn,spawnPos, Quaternion.identity);
         //       spawnedObject.transform.SetParent(parentObject);
             }
        }
    }
    
}


