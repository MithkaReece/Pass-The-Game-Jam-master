using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupMovement : MonoBehaviour
{
    float hoverHeight = 0.5f;
    float hoverSpeed = 2f;

    Vector3 initialPosition;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the vertical offset using a sine wave
        float yOffset = Mathf.Sin(Time.time * hoverSpeed) * hoverHeight;

        // Apply the vertical offset to the object's position
        transform.position = initialPosition + new Vector3(0f, yOffset, 0f);

    }
}
