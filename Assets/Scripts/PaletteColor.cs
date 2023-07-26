using UnityEngine;

public class PaletteColor : MonoBehaviour
{
    public TerrainManager.LandType landType;
 
    public bool hovered; // Set to true by paint drawer each frame when hovered over; set to false at thnd of this object's update

    private Renderer renderer;
    public Color matBaseColor { get; private set; }

    public float LandHeight;

    private float opacity = 1f;
    
    private void Awake()
    {
        renderer = GetComponent<Renderer>();
        matBaseColor = renderer.material.color;
    }


    private void Update()
    {
        Color newColor;
        if (hovered)
        {
            newColor = Color.Lerp(matBaseColor, Color.white, 0.5f);
        } else
        {
            newColor = matBaseColor;
        }
        newColor.a = opacity;
        renderer.material.color = newColor;

        hovered = false;
    }

    float maxPaint = 1000f;
    public void UpdateOpacity(float paintLeft) {
        opacity = paintLeft / maxPaint;
        if(opacity == 0) 
            renderer.enabled = false;
        else 
            renderer.enabled = true;
    }
}