using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class WorldController : MonoBehaviour
{

    public static WorldController Instance { get; protected set; }

    //public World World { get; protected set; }

    public Sprite dirtSprite1;
    public Sprite dirtSprite2;

    public World World {get; protected set;}

    // Start is called before the first frame update
    void Start()
    {
        if (Instance != null) {
            Debug.LogError("Multiple World Controllers");
        }

        Instance = this;


        float viewHeight = Camera.main.orthographicSize * 2.0f;
        float viewWidth = viewHeight * Screen.width / Screen.height;

        World = new World();
        World.GenerateTilesHight();

        // create a game object to represent each tile
        for (int x = -Mathf.FloorToInt(viewWidth); x < World.Width + Mathf.FloorToInt(viewWidth); x++)
        {
            for (int y = -Mathf.FloorToInt(viewHeight); y < World.Height + Mathf.FloorToInt(viewHeight); y++)
            {
                Tile tile_data = World.GetTileAt(x, y);
                GameObject tile_go = new GameObject();
                tile_go.name = "Tile_" + x + "_" + y;
                tile_go.transform.position = new Vector3(x, y, 0);
                tile_go.transform.SetParent(this.transform, true);

                tile_go.AddComponent<SpriteRenderer>();
                tile_data.RegisterTileTypeChangedCallback((tile) => OnTileTypeChanged(tile, tile_go));
            }
        }

        World.ResetSetAllTiles();

        // create a game object to represent each tile
        //for (int x = -Mathf.FloorToInt(viewWidth); x < World.Width + Mathf.FloorToInt(viewWidth); x++)
        //{
        //    for (int y = -Mathf.FloorToInt(viewHeight); y < World.Height + Mathf.FloorToInt(viewHeight); y++)
        //    {
        //        Tile tile_data = World.GetTileAt(x, y);
        //        GameObject tile_go = new GameObject();
        //        tile_go.name = "Tile_" + x + "_" + y;
        //        tile_go.transform.position = new Vector3(x, y, 0);
        //        tile_go.transform.SetParent(this.transform);

        //        TextMeshPro mText = tile_go.AddComponent<TextMeshPro>();

        //        mText.autoSizeTextContainer = true;
        //        mText.text = " (X=" + tile_data.x + ",Y=" + tile_data.y + ") ";
        //        // Set various font settings.
        //        mText.fontSize = 1.5f;
        //        mText.alignment = TextAlignmentOptions.Center;

        //    }
        //}

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
