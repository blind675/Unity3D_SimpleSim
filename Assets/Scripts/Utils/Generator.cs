using UnityEngine;
using AccidentalNoise;

class MapData
{

    public float[,] Data;
    public float Min { get; set; }
    public float Max { get; set; }

    public MapData(int width, int height)
    {
        Data = new float[width, height];
        Min = float.MaxValue;
        Max = float.MinValue;
    }
}

public class Generator
{
    static private int terrainOctaves = 6;
    static private double terrainFrequency = 1.25;

    public static Tile[,] GenerateWorld(int width, int height)
    {
        // Initialize the HeightMap Generator
        ImplicitFractal heightMap = new ImplicitFractal(FractalType.MULTI,
                                       BasisType.SIMPLEX,
                                       InterpolationType.QUINTIC,
                                       terrainOctaves,
                                       terrainFrequency,
                                       Random.Range(0, int.MaxValue));

        // Build the height map
        MapData heightData = new MapData(width, height);

        // loop through each x,y point - get height value
        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                // Noise range
                float x1 = 0, x2 = 2;
                float y1 = 0, y2 = 2;
                float dx = x2 - x1;
                float dy = y2 - y1;

                // Sample noise at smaller intervals
                float s = x / (float)width;
                float t = y / (float)height;

                // Calculate our 4D coordinates
                float nx = x1 + Mathf.Cos(s * 2 * Mathf.PI) * dx / (2 * Mathf.PI);
                float ny = y1 + Mathf.Cos(t * 2 * Mathf.PI) * dy / (2 * Mathf.PI);
                float nz = x1 + Mathf.Sin(s * 2 * Mathf.PI) * dx / (2 * Mathf.PI);
                float nw = y1 + Mathf.Sin(t * 2 * Mathf.PI) * dy / (2 * Mathf.PI);

                float heightValue = (float)heightMap.Get(nx, ny, nz, nw);

                // keep track of the max and min values found
                if (heightValue > heightData.Max) heightData.Max = heightValue;
                if (heightValue < heightData.Min) heightData.Min = heightValue;

                heightData.Data[x, y] = heightValue;
            }
        }

        // Build our final objects based on our data
        Tile[,] tiles = new Tile[width, height];

        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                Tile t = new Tile(x, y);

                float value = heightData.Data[x, y];

                //normalize our value between 0 and 1
                value = (value - heightData.Min) / (heightData.Max - heightData.Min);

                t.Height = value;

                tiles[x, y] = t;
            }
        }

        return tiles;
    }

}