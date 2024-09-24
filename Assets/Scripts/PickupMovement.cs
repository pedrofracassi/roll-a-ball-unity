using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupMovement : MonoBehaviour
{

    float speed = 3.0f;

    public Boolean rotate;

    public Boolean randomizeInitialRotation = true;

    public Boolean teleportToGroundOnStart = true;

    // initial coordinates
    public float initialX = 0.0f;
    public float initialY = 0.0f;
    public float initialZ = 0.0f;

    public Boolean rotateAllAxis;

    [Range(0.01f, 1.0f)]
    public float floatRange = 0.2f;

    public float minFloatHeight = 0.7f;

    void Start()
    {
        // save initial position
        initialX = transform.position.x;
        initialY = transform.position.y;
        initialZ = transform.position.z;

        if (randomizeInitialRotation)
        {
            // randomize rotation multiplier
            transform.Rotate(new Vector3(UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(0, 360)));
        }

        // randomize floating speed
        speed = UnityEngine.Random.Range(1.0f, 3.0f);
    }

    void Update()
    {
        if (rotate)
        {
            if (rotateAllAxis)
            {
                transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime * 3.0f);
            }
            else
            {
                transform.Rotate(new Vector3(0, 0, 45) * Time.deltaTime * 3.0f);
            }
        }


        // move up and down in a sine wave
        transform.position = new Vector3(transform.position.x, Mathf.Sin(Time.time * speed) * floatRange + initialY, transform.position.z);
    }
}
