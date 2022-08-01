using UnityEngine;

public class World
{
    Tile[,] tiles;
    public int Width { get; }
    public int Height { get; }

    public World(int width = 100, int height = 100)
    {
        this.Width = width;
        this.Height = height;
        tiles = Generator.GenerateWorld(width, height);

        Debug.Log("World created with " + (width * height) + " tiles");

    }

    public void SetTilesForHeight()
    {
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                Tile tile = GetTileAt(x, y);

                if (tile.Height > 0.95)
                {
                    tile.type = Tile.TileType.Snow;
                }
                else if (tile.Height > 0.87)
                {
                    tile.type = Tile.TileType.Rock;
                }
                else
                {
                    tile.type = Tile.TileType.Dirt;
                }
                //else if (tile.Height > 0.50)
                //{
                //    tile.type = Tile.TileType.Grass;
                //}
                //else if (tile.Height > 0.20)
                //{
                //    tile.type = Tile.TileType.Dirt;
                //}
                //else if (tile.Height > 0.10)
                //{
                //    tile.type = Tile.TileType.Water;
                //}
                //else
                //{
                //    tile.type = Tile.TileType.DeepWater;
                //}

                Tile.Orientation maxSlopOrientation = new Tile.Orientation();
                float maxSloapValue = float.NegativeInfinity;

                for (int xOffset = -1; xOffset < 2; xOffset++)
                {
                    for (int yOffset = -1; yOffset < 2; yOffset++)
                    {
                        Tile adjacentTile = GetTileAt(x + xOffset, y + yOffset);
                        float sloap = tile.Height - adjacentTile.Height;

                        if (sloap > 0 && sloap > maxSloapValue)
                        {
                            maxSlopOrientation = new Tile.Orientation(xOffset, yOffset);
                            maxSloapValue = sloap;
                        }
                    }
                }

                tile.SloapValue = maxSloapValue;
                tile.SloapOrientation = maxSlopOrientation;
            }
        }
    }

    public void GenerateSprings()
    {
        int probability = 5;

        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                Tile tile = GetTileAt(x, y);

                if (tile.Height > 0.8 && tile.Height < 0.83)
                {
                    if (Random.Range(0, 99) < probability)
                    {
                        tile.type = Tile.TileType.Water;
                        probability = 5;
                    }
                    else {
                        probability++;
                    }
                }
            }
        }
    }

    public Tile GetTileAt(int x, int y)
    {
        if (x < 0)
        {
            x = Width + (x % Width);
        }
        if (y < 0)
        {
            y = Height + (y % Height);
        }

        return tiles[x % Width, y % Height];
    }

}
