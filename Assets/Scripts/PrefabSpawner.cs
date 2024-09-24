using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    public GameObject prefab;

    // multiple GameObjects and their weights

    public GameObject spawnAreaCube;

    [Range(0.01f, 10.0f)]
    public float spawnTimeSeconds = 2.0f;

    public int spawnOnStart = 10;

    public Boolean spawnContinuously = true;

    public Boolean randomHorizontalRotation = false;

    public float height = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        if (spawnContinuously)
        {
            // spawn pickups every 2 seconds
            InvokeRepeating("SpawnPickup", spawnTimeSeconds, spawnTimeSeconds);
        }

        // spawn a number of pickups on start
        for (int i = 0; i < spawnOnStart; i++)
        {
            SpawnPickup();
        }
    }

    void SpawnPickup()
    {
        // spawn a pickup at a random position
        Vector3 position = new Vector3(UnityEngine.Random.Range(-spawnAreaCube.transform.localScale.x / 2, spawnAreaCube.transform.localScale.x / 2),
                                        height,
                                        UnityEngine.Random.Range(-spawnAreaCube.transform.localScale.z / 2, spawnAreaCube.transform.localScale.z / 2));

        if (randomHorizontalRotation)
        {
            // randomize rotation of spawned prefab
            prefab.transform.Rotate(new Vector3(0, UnityEngine.Random.Range(0, 360), 0));
        }

        // Pickup is a Prefab in the root folder
        Instantiate(prefab, position, Quaternion.identity);


    }
}
