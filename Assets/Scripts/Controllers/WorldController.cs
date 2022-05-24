using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class WorldController : MonoBehaviour
{

    public static WorldController Instance { get; protected set; }

    public World World { get; protected set; }

    public Sprite dirtSprite1;
    public Sprite dirtSprite2;

    World world;

    // Start is called before the first frame update
    void Start()
    {
        world = new World();

        world.GenerateTilesHight();

        // create a game object to represent each tile
        for (int x = 0; x < world.width; x++)
        {
            for (int y = 0; y < world.height; y++)
            {
                Tile tile_data = world.GetTileAt(x, y);
                GameObject tile_go = new GameObject();
                tile_go.name = "Tile_" + x + "_" + y;
                tile_go.transform.position = new Vector3(tile_data.x, tile_data.y, 0);
                tile_go.transform.SetParent(this.transform);

                TextMeshPro mText = tile_go.AddComponent<TextMeshPro>();

                mText.autoSizeTextContainer = true;
                mText.text = tile_data.height.ToString();
                // Set various font settings.
                mText.fontSize = 2;
                mText.alignment = TextAlignmentOptions.Center;

                //tile_go.AddComponent<SpriteRenderer>();
                //tile_data.RegisterTileTypeChangedCallback((tile) => OnTileTypeChanged(tile, tile_go));
            }
        }

        
    }


    void OnTileTypeChanged(Tile tile_data, GameObject tile_go)
    {

        switch (tile_data.type)
        {
            case Tile.TileType.Dirt:
                tile_go.GetComponent<SpriteRenderer>().sprite = UnityEngine.Random.Range(0, 2) == 0 ? dirtSprite1 : dirtSprite2;
                break;
            default:
                tile_go.GetComponent<SpriteRenderer>().sprite = null;
                break;
        }

    }
}
