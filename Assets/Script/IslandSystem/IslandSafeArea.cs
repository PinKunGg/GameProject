using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class IslandSafeArea : MonoBehaviour
{
    public bool isOverlap;
    [SerializeField] Tilemap tilemap;
    [SerializeField] TileMapSelectMarker TileMapSelectMarker;
    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.LogError(other.gameObject.name);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Island"))
        {
            isOverlap = true;
        }
        
        if(other.CompareTag("Player"))
        {
            TileMapReader.TMRinstanse.tilemap = tilemap;
            BuildManager.BMinstanse.adjIslandPos = this.transform.position;
        } 
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Player") && TileMapReader.TMRinstanse.tilemap != tilemap)
        {
            BuildManager.BMinstanse.GetSetinBuildMode = false;
            TileMapReader.TMRinstanse.tilemap = null;
            BuildManager.BMinstanse.adjIslandPos = Vector3.zero;
            TileMapSelectMarker.ResetGrid();

            TileMapReader.TMRinstanse.tilemap = tilemap;
            BuildManager.BMinstanse.adjIslandPos = this.transform.position;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player") && TileMapReader.TMRinstanse.tilemap == tilemap)
        {
            BuildManager.BMinstanse.GetSetinBuildMode = false;
            TileMapReader.TMRinstanse.tilemap = null;
            BuildManager.BMinstanse.adjIslandPos = Vector3.zero;
            TileMapSelectMarker.ResetGrid();
        }
    }
}
