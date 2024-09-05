using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    public GameObject prefab;

    // Start is called before the first frame update
    void Start()
    {
        // spawn pickups every 2 seconds
        InvokeRepeating("SpawnPickup", 0.1f, 0.1f);
    }

    void SpawnPickup()
    {
        // spawn a pickup at a random position
        Vector3 position = new Vector3(Random.Range(-50.0f, 50.0f), 0.5f, Random.Range(-50.0f, 50.0f));

        // Pickup is a Prefab in the root folder
        Instantiate(prefab, position, Quaternion.identity);

        // debug log
        Debug.Log("Spawned a pickup at " + position);
    }
}
