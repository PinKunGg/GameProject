using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapSelectMarker : MonoBehaviour
{
    public Tilemap selectTileMap;
    [SerializeField] TileBase markerTile, outOfRangeTile, unAbleToBuild;
    [SerializeField] TileMapReader tileMapReader;
    public Vector3Int[] markCellPos;
    public Vector3Int[] oldmarkCellPos;

    private void Awake()
    {
        tileMapReader = GameObject.Find("GM").GetComponent<TileMapReader>();
        selectTileMap = GameObject.Find("BuildingTilemap").GetComponent<Tilemap>();
    }
    public void ResetGrid()
    {
        foreach(var item in oldmarkCellPos)
        {
            selectTileMap.SetTile(item,null);
        }
        foreach(var item in markCellPos)
        {
            selectTileMap.SetTile(item,null);
        }
        ResetOldPos();
    }

    public void ShowSelect(bool value, bool isReadyToBuild)
    {
        if(value)
        {
            foreach(var item in oldmarkCellPos)
            {
                selectTileMap.SetTile(item,null);
            }

            if(isReadyToBuild)
            {
                foreach(var item in markCellPos)
                {
                    selectTileMap.SetTile(item,markerTile);
                }
            }
            else
            {
                foreach(var item in markCellPos)
                {
                    selectTileMap.SetTile(item,unAbleToBuild);
                }
            }
            
        }
        else
        {
            foreach(var item in oldmarkCellPos)
            {
                selectTileMap.SetTile(item,null);
            }
            foreach(var item in markCellPos)
            {
                selectTileMap.SetTile(item,outOfRangeTile);
            }
        }
        UpdateOldPos();
    }

    void UpdateOldPos()
    {
        int i = 0;

        for(i = 0; i < markCellPos.Length; i++)
        {
            oldmarkCellPos[i] = markCellPos[i];
        }
    }
    void ResetOldPos()
    {
        int i = 0;

        for(i = 0; i < markCellPos.Length; i++)
        {
            oldmarkCellPos[i] = Vector3Int.zero;
            markCellPos[i] = Vector3Int.zero;
            BuildManager.BMinstanse.AreaCheck[i] = false;
        }
    }
}
