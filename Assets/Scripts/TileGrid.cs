using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class TileGrid : MonoBehaviour {

    // Bitmap for the level
    public Texture2D levelText;


    public List<Color> _keys = new List<Color>();
    public List<GameObject> _values = new List<GameObject>();

    // Dictionary that specifiecs which tile to assign to corresponding color
    private Dictionary<Color, GameObject> ColorToTile;

    // TODO: create spritesheet with a bitmap of colors for the spritesheet. (same color = select random sprite for variation?)

    // Use this for initialization
    void Start () {
        generateColorTileDictionary();
        createGrid();
    }

    // Spawns the tiles
    private void createGrid() {
        Assert.IsNotNull(levelText, "Level bitmap was not assigned");
        Assert.AreNotEqual(ColorToTile.Count, 0, "Colormap should contain at least one tile");

        Color[] levelBitmap = levelText.GetPixels();

        Vector3 gridPos = transform.position;

        for (int x = 0; x < levelText.width; x++) {
            for(int y = 0; y < levelText.height; y++) {

                Color mapColor = levelBitmap[x + y * levelText.width];
                GameObject tilePrefab;

                // TODO: Check if it really is a tile
                if (ColorToTile.ContainsKey(mapColor)) {
                    tilePrefab = ColorToTile[mapColor];
                } else { 
                    print("Skipping tile");
                    continue;
                }

                GameObject tile = Instantiate(tilePrefab, gridPos + new Vector3(x, y, 0), Quaternion.identity, transform) as GameObject;
            }
        }
    }


    private void generateColorTileDictionary() {
        ColorToTile = new Dictionary<Color, GameObject>();
        for (int i = 0; i != Math.Min(_keys.Count, _values.Count); i++)
            ColorToTile.Add(_keys[i], _values[i]);
    }
}
