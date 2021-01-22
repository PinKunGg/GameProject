using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class StoneSprite : MonoBehaviour
{
    public TileMapReader tileMapReader;
    public bool isOverLap;

    private void Awake()
    {
        tileMapReader = GameObject.Find("GM").GetComponent<TileMapReader>();
    }
    void Start()
    {
        TileBase tileBase = tileMapReader.GetTileBase(Vector3Int.FloorToInt(this.transform.position) - Vector3Int.FloorToInt(BuildManager.BMinstanse.adjIslandPos));
        TileData tileData = tileMapReader.GetTileData(tileBase);

        if(tileData != null)
        {
            if(!tileData.isMountainZone)
            {
                isOverLap = true;
            }
            else
            {
                this.name = "Stone" + " / " + Vector3Int.FloorToInt(this.transform.position);
            }
        }
        else
        {
            Debug.Log("NULL");
            isOverLap = true;
        }
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Stone"))
        {
            isOverLap = true;
        }
    }
}
