using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapReader : MonoBehaviour
{
    public static TileMapReader TMRinstanse;
    public Tilemap tilemap;
    [SerializeField] List<TileData> tileDatas;
    Dictionary<TileBase, TileData> dataFromTile;

    private void Awake()
    {
        if(TMRinstanse == null)
        {
            TMRinstanse = this;
        }
    }
    private void Start()
    {
        dataFromTile = new Dictionary<TileBase, TileData>();

        foreach(TileData tileData in tileDatas)
        {
            foreach(TileBase tileBase in tileData.tiles)
            {
                dataFromTile.Add(tileBase,tileData);
            }
        }
    }
    public Vector3Int GetGridPos(Vector2 position, bool mousePosition)
    {
        Vector3 worldPos;

        if(mousePosition)
        {
            worldPos = Camera.main.ScreenToWorldPoint(position);
        }
        else
        {
            worldPos = position;
        }

        if(tilemap != null)
        {
            Vector3Int gridPosition = tilemap.WorldToCell(worldPos + (Vector3)BuildManager.BMinstanse.adjIslandPos); //เอา position ของ mouse + position ของ Island เพื่อให้มัน return ค่าจริงของ position ใน Tilemap ที่ Player เลือก
            return gridPosition;
        }
        else
        {
            BuildManager.BMinstanse.GetSetinBuildMode = false;
            return Vector3Int.zero;
        }
    }

    public TileBase GetTileBase(Vector3Int gridPosition)
    {
        TileBase tile = tilemap.GetTile(gridPosition);
        return tile;
    }

    public TileData GetTileData(TileBase tilebase)
    {
        try
        {
            return dataFromTile[tilebase];
        }
        catch
        {
            return null;
        }
    }
}
