using System;
using UtilMethods;


public class Tile
{
    public enum TileType { Test, Empty, Dirt, Sand, Grass, Rock, Water, DeepWater, Snow };
    public enum OrientationDirection { Flat, N, NE, E, SE, S, SW, W, NW };

    public struct Orientation
    {
        int xOffset;
        int yOffset;
        public OrientationDirection Direction { get; }

        public Orientation(int xOffset, int yOffset)
        {
            this.xOffset = xOffset;
            this.yOffset = yOffset;

            if (xOffset == 0 && yOffset == 1)
            {
                Direction = OrientationDirection.N;
            }
            else if (xOffset == 1 && yOffset == 1)
            {
                Direction = OrientationDirection.NE;
            }
            else if (xOffset == 1 && yOffset == 0)
            {
                Direction = OrientationDirection.E;
            }
            else if (xOffset == 1 && yOffset == -1)
            {
                Direction = OrientationDirection.SE;
            }
            else if (xOffset == 0 && yOffset == -1)
            {
                Direction = OrientationDirection.S;
            }
            else if (xOffset == -1 && yOffset == -1)
            {
                Direction = OrientationDirection.SW;
            }
            else if (xOffset == -1 && yOffset == 0)
            {
                Direction = OrientationDirection.W;
            }
            else if (xOffset == -1 && yOffset == 1)
            {
                Direction = OrientationDirection.NW;
            }
            else
            {
                Direction = OrientationDirection.Flat;
            }
        }
    };

    Action<Tile> cbTileTypeChanged;

    TileType _type = TileType.Empty;
    public TileType type
    {
        get
        {
            return _type;
        }
        set
        {
            TileType oldType = _type;
            _type = value;

            if (cbTileTypeChanged != null && oldType != value)
            {
                cbTileTypeChanged(this);
            }
        }
    }

    public int X { get; }
    public int Y { get; }

    public float Height;
    public float Depth;

    float _slopeValue = 0f;
    public float SloapValue
    {
        get => _slopeValue;
        set
        {
            _slopeValue = value;
            ErosionCoefficient = (value * 0.001f).Truncate(8);
        }
    } // = FlowSpeed;

    public Orientation SloapOrientation;
    public float ErosionCoefficient = 0f; // SloapValue * 0.001

    public Tile(int x, int y)
    {
        this.X = x;
        this.Y = y;
    }

    public void RegisterTileTypeChangedCallback(Action<Tile> callback)
    {
        cbTileTypeChanged += callback;
    }

    public void UnregisterTileTypeChangedCallback(Action<Tile> callback)
    {
        cbTileTypeChanged -= callback;
    }
}
