using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public PaletteColor palleteColor;
    [SerializeField] public int amount;
    // Start is called before the first frame update
    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        // Set the new material
        renderer.material = Instantiate(palleteColor.GetComponent<Renderer>().material);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
