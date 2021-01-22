using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class IslandSafeArea : MonoBehaviour
{
    public bool isOverlap, isGenMapFinish;
    public Tilemap tilemap;
    [SerializeField] TileMapSelectMarker TileMapSelectMarker;
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

            foreach(var item in this.transform.GetComponentsInChildren<SpriteManager>())
            {
                //item.enabled = true;
            }
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Player") && TileMapReader.TMRinstanse.tilemap != tilemap && isGenMapFinish)
        {
            Debug.Log("Bruhhhhhhhhhhhhh");
            BuildManager.BMinstanse.GetSetinBuildMode = false;
            TileMapReader.TMRinstanse.tilemap = null;
            BuildManager.BMinstanse.adjIslandPos = Vector3.zero;
            TileMapSelectMarker.ResetGrid();

            TileMapReader.TMRinstanse.tilemap = tilemap;
            BuildManager.BMinstanse.adjIslandPos = this.transform.position;

            foreach(var item in this.transform.GetComponentsInChildren<SpriteManager>())
            {
                if(item.enabled == false)
                {
                    //item.enabled = true;
                }
                else
                {
                    break;
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player") && TileMapReader.TMRinstanse.tilemap == tilemap)
        {
            Debug.Log("Player Exit Island");
            BuildManager.BMinstanse.GetSetinBuildMode = false;
            TileMapReader.TMRinstanse.tilemap = null;
            BuildManager.BMinstanse.adjIslandPos = Vector3.zero;
            TileMapSelectMarker.ResetGrid();

            foreach(var item in this.transform.GetComponentsInChildren<SpriteManager>())
            {
                //item.enabled = false;
            }
        }
    }
}
