using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platformer : MonoBehaviour
{

    public PaintDrawer paintDrawer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object colliding with the pickup has the tag "Pickup"
        if (other.CompareTag("Pickup"))
        {
            // Extract the value from the pickup
            Pickup pickup = other.GetComponent<Pickup>();
            paintDrawer.updatePaint(pickup.palleteColor, pickup.amount);
            // Delete the pickup object
            Destroy(other.gameObject);
        }
    }

}
