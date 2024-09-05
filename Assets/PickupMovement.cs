using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupMovement : MonoBehaviour
{

    float speed = 3.0f;

    void Start()
    {
        // randomize rotation multiplier
        transform.Rotate(new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)));

        // randomize floating speed
        speed = Random.Range(1.0f, 3.0f);
    }

    void Update()
    {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime * 3.0f);

        // move up and down in a sine wave
        transform.position = new Vector3(transform.position.x, Mathf.Sin(Time.time * speed) * 0.2f + 0.7f, transform.position.z);
    }
}
