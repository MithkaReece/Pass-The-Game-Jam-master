using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[ExecuteAlways]
public class TerrainManager : MonoBehaviour
{
    [SerializeField] private Terrain terrain;

    [SerializeField] private float[] _landTypeHeights;

    public enum LandType
    {
        Empty,
        Grass,
        Water,
        Mountain // gray
    }

    private int resolution;

    // since this is an ExecuteAlways script this will reset the map both when starting play mode and leaving play mode/starting edit mode
    private void Awake()
    {
        resolution = terrain.terrainData.heightmapResolution;

        // Set all heights to height of empty
        float[,] initialHeights = new float[resolution, resolution];
        for (int x = 0; x < resolution; x++)
        {
            for (int y = 0; y < resolution; y++)
            {
                initialHeights[x, y] = _landTypeHeights[0];
            }
        }
        terrain.terrainData.SetHeights(0, 0, initialHeights);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
            SaveTerrain("terrain_heights.data");
        if (Input.GetKeyDown(KeyCode.O))
            LoadTerrainHeightsFromFile("terrain_heights.data");
    }

    /// <summary>
    /// Sets the height of a region on the terrain, converting the given texture-space coordinated into terrain space coordinates, 
    /// and setting height based on the land type provided.
    /// </summary>
    /// <param name="xCoord">X texture coordinate on canvas, from 0 to 1</param>
    /// <param name="yCoord">Y texture coordinate on canvas, from 0 to 1</param>
    /// <param name="paintSize">brush size relative to canvas - Size of the brush divided by the total width of the canvas</param>
    /// <param name="landType"></param>
    public void SetLandTypeRegion(float xCoord, float yCoord, float paintSize, LandType landType)
    {
        float targetHeight = _landTypeHeights[(int)landType];

        // Calculate lower corner of brush
        // keep above 0, center heightmap change area on coordinate
        int x = Mathf.Max(0, (int)((xCoord - paintSize/2f) * resolution)); 
        int y = Mathf.Max(0, (int)((yCoord - paintSize/2f) * resolution));
        int size = (int)(paintSize * resolution);

        // Calculate upper corner of brush
        // Keep upper bound coordinates below size of heightmap
        int xMax = Mathf.Min(x + size, resolution);
        int yMax = Mathf.Min(y + size, resolution);

        Debug.Log(xCoord+", "+yCoord+". Clamped: "+x + ", " + y + ", " + xMax + ", " + yMax);

        //Fill in square region between two corners (centered at draw position)
        float[,] heights = new float[xMax-x, yMax-y];
        for (int hx = 0; hx < xMax - x; hx++) 
        {
            for (int hy = 0; hy < yMax - y; hy++)
            {
                heights[hx, hy] = targetHeight;
            }
        }

        terrain.terrainData.SetHeights(x, y, heights);
    }

    public void SaveTerrain(string filename) {
        float[,] heights = terrain.terrainData.GetHeights(0, 0, terrain.terrainData.heightmapResolution, terrain.terrainData.heightmapResolution);
        byte[] data = Serialize(heights);
        File.WriteAllBytes(filename, data);
        Debug.Log("Saved");
    }

    byte[] Serialize(float[,] heights)
    {
        BinaryFormatter bf = new BinaryFormatter();
        using (MemoryStream ms = new MemoryStream())
        {
            bf.Serialize(ms, heights);
            return ms.ToArray();
        }
    }

    void LoadTerrainHeightsFromFile(string filename)
    {
        if (File.Exists(filename))
        {
            // Read the binary data from the file
            byte[] data = File.ReadAllBytes(filename);

            // Deserialize the binary data back to the heights array
            float[,] heights = Deserialize(data);

            // Set the terrain heights from the loaded data
            terrain.terrainData.SetHeights(0, 0, heights);
        }
        else
        {
            Debug.LogError("Terrain height data file not found.");
        }
    }

    // Helper method to deserialize the heights array from the binary data
    float[,] Deserialize(byte[] data)
    {
        BinaryFormatter bf = new BinaryFormatter();
        using (MemoryStream ms = new MemoryStream(data))
        {
            return (float[,])bf.Deserialize(ms);
        }
    }
}
