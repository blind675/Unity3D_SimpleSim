using UnityEngine;
using System.Collections;
using AccidentalNoise;

public class World
{ 
    Tile[,] Tiles;
    public int Width { get; }
    public int Height { get; }

    // Noise generator module
    ImplicitFractal HeightMap;
    int TerrainOctaves = 6;
    double TerrainFrequency = 1.25;

    public World(int width = 40, int height = 40)
    {
        this.Width = width;
        this.Height = height;
        Tiles = new Tile[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Tiles[x, y] = new Tile(this, x, y);
            }
        }

        Debug.Log("World created with " + (width * height) + " tiles");

        HeightMap = new ImplicitFractal(FractalType.MULTI,
                                       BasisType.SIMPLEX,
                                       InterpolationType.QUINTIC,
                                       TerrainOctaves,
                                       TerrainFrequency,
                                       UnityEngine.Random.Range(0, int.MaxValue));

    }

    public void GenerateTilesHight() {
        // loop through each x,y point - get height value
        for (var x = 0; x < Width; x++)
        {
            for (var y = 0; y < Height; y++)
            {

                // Noise range
                float x1 = 0, x2 = 2;
                float y1 = 0, y2 = 2;
                float dx = x2 - x1;
                float dy = y2 - y1;

                // Sample noise at smaller intervals
                float s = x / (float)Width;
                float t = y / (float)Height;

                // Calculate our 4D coordinates
                float nx = x1 + Mathf.Cos(s * 2 * Mathf.PI) * dx / (2 * Mathf.PI);
                float ny = y1 + Mathf.Cos(t * 2 * Mathf.PI) * dy / (2 * Mathf.PI);
                float nz = x1 + Mathf.Sin(s * 2 * Mathf.PI) * dx / (2 * Mathf.PI);
                float nw = y1 + Mathf.Sin(t * 2 * Mathf.PI) * dy / (2 * Mathf.PI);

                float heightValue = (float)HeightMap.Get(nx, ny, nz, nw);

                Tiles[x, y].height = heightValue;
            }
        }
    }


    public void RandomizeTiles()
    {
        Debug.Log("World randomized tiles");

        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {

                if (Random.Range(0, 2) == 0)
                {
                    Tiles[x, y].type = Tile.TileType.Dirt;
                }
                else
                {
                    Tiles[x, y].type = Tile.TileType.Empty;
                }
            }
        }
    }


    public void ResetSetAllTiles()
    {
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                Tiles[x, y].type = Tile.TileType.Dirt;
            }
        }
    }


    public Tile GetTileAt(int x, int y)
    {
        if (x < 0)
        {
            x = Width + (x % Width);
        }
        if (y < 0) {
            y = Height + (y % Height);
        }

        return Tiles[x % Width, y % Height];
    }

}
