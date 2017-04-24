using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class TileGrid : MonoBehaviour {

    // Bitmap for the level
    public Texture2D levelTexture;

    public String nextLevel = "GameWon";

    public List<Color> _keys = new List<Color>();
    public List<GameObject> _values = new List<GameObject>();

    // Dictionary that specifiecs which tile to assign to corresponding color
    private Dictionary<Color, GameObject> ColorToTile;

    // TODO: create spritesheet with a bitmap of colors for the spritesheet. (same color = select random sprite for variation?)

    // Use this for initialization
    void Start () {
    }

    // Spawns the tiles
    public void createGrid() {
        generateColorTileDictionary();
        Assert.IsNotNull(levelTexture, "Level bitmap was not assigned");
        Assert.AreNotEqual(ColorToTile.Count, 0, "Colormap should contain at least one tile");

        Color[] levelBitmap = levelTexture.GetPixels();

        Vector3 gridPos = transform.position;

        for (int x = 0; x < levelTexture.width; x++) {
            for(int y = 0; y < levelTexture.height; y++) {

                Color mapColor = levelBitmap[x + y * levelTexture.width];
                GameObject tilePrefab;

                // TODO: Check if it really is a tile
                if (ColorToTile.ContainsKey(mapColor)) {
                    tilePrefab = ColorToTile[mapColor];
                } else { 
                    continue;
                }

                Instantiate(tilePrefab, gridPos + new Vector3(x, y, 0), Quaternion.identity, transform);
            }
        }
    }


    private void generateColorTileDictionary() {
        ColorToTile = new Dictionary<Color, GameObject>();
        for (int i = 0; i != Math.Min(_keys.Count, _values.Count); i++)
            ColorToTile.Add(_keys[i], _values[i]);
    }
}
