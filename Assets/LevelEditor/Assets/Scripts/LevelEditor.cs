using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;

public class LevelEditor : MonoBehaviour
{
    
    [SerializeField] Tilemap currentTilemap;
    [SerializeField] TileBase currentTile;

    [SerializeField] Camera cam;

    private Vector2 currentTilemapMinBounds;
    private Vector2 currentTilemapMaxBounds;

    public bool canEdit = true;


    private void Start()
    {
        currentTilemapMinBounds = new Vector2(currentTilemap.origin.x, currentTilemap.origin.y);
        currentTilemapMaxBounds = new Vector2(currentTilemap.origin.x + currentTilemap.size.x - 1, currentTilemap.origin.y + currentTilemap.size.y - 1);
    }
    private void Update()
    {
        if (canEdit)
        {
            Vector3Int pos = currentTilemap.WorldToCell(cam.ScreenToWorldPoint(Input.mousePosition));

            //place tile with left click
            if (Input.GetMouseButton(0)) PlaceTile(pos);
            //delete tile with right click
            if (Input.GetMouseButton(1)) DeleteTile(pos);
        }

    }

    /// <summary>
    /// Place down the current tile on the current tilemap at pos
    /// </summary>
    /// <param name="pos"></param>
    void PlaceTile(Vector3Int pos)
    {
        //Check if it the pos is between the bounds of the tilemap
        if (pos.x >= currentTilemapMinBounds.x && pos.y >= currentTilemapMinBounds.y && pos.x <= currentTilemapMaxBounds.x && pos.y <= currentTilemapMaxBounds.y)
        {
            currentTilemap.SetTile(pos, currentTile);
        }
    }

    /// <summary>
    /// Delete the tile on the current tilemap at pos
    /// </summary>
    /// <param name="pos"></param>
    void DeleteTile(Vector3Int pos)
    {
        currentTilemap.SetTile(pos, null);
    }

    public void ChangeCurrentTile(TileBase tile)
    {
        currentTile = tile;
    }

    public void EnableEdition()
    {
        canEdit = true;
    }

    public void DisableEdition()
    {
        canEdit = false;
    }
}
