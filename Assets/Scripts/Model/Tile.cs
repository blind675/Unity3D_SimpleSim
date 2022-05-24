using System;

public class Tile {

  public enum TileType { Dirt, Sand, Grass, Rock, Water, Empty };

  Action<Tile> cbTileTypeChanged;

  TileType _type = TileType.Empty;
  public TileType type {
    get {
      return _type;
    } 
    set {
      TileType oldType = _type;
      _type = value;

      if(cbTileTypeChanged != null && oldType != value) {
        cbTileTypeChanged(this);
      }
    }
  }

  World world;
  public int x {get;}
  public int y {get;}
 
  public float height;

  public Tile(World world, int x, int y) {
    this.world = world;
    this.x = x;
    this.y = y;
  }

  public void RegisterTileTypeChangedCallback(Action<Tile> callback) {
    cbTileTypeChanged += callback;
  }

  public void UnregisterTileTypeChangedCallback(Action<Tile> callback) {
    cbTileTypeChanged -= callback;
  }
}
