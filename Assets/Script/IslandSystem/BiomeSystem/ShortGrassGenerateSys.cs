﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortGrassGenerateSys : MonoBehaviour
{
    public IslandManager islandManager;
    public Collider2D ColliderGenerateArea;
    public IEnumerator GenerateBurry()
    {
        for(int i = 0; i < islandManager.BurryCount;)
        {
            int RandPos = Random.Range(0,1);
            Vector3 AdjPos;

            if(RandPos == 0)
            {
                AdjPos = new Vector3(-0.5f,-0.5f,0f);
            }
            else
            {
                AdjPos = new Vector3(0.5f,0.5f,0f);
            }
            
            Vector3 SpawnPos = new Vector3(Random.Range((int)ColliderGenerateArea.bounds.min.x,(int)ColliderGenerateArea.bounds.max.x),Random.Range((int)ColliderGenerateArea.bounds.min.y,(int)ColliderGenerateArea.bounds.max.y),0);
            GameObject Berry = Instantiate(islandManager.BerryList[Random.Range(0,islandManager.BerryList.Count)], SpawnPos + AdjPos, Quaternion.identity);
            yield return new WaitForSeconds(0.01f);

            if(Berry.GetComponent<BerrySprite>().isOverLap == false)
            {
                BuildManager.BMinstanse.AllBuidlingPositionCheckList.AllBuildPositionSave.Add((Vector3Int.FloorToInt(SpawnPos + AdjPos)));
                Vector3[] pos = new Vector3[]{ SpawnPos + AdjPos };
                Vector3Int[] alterPos = new Vector3Int[] { Vector3Int.FloorToInt(SpawnPos + AdjPos) };
                BuildManager.BMinstanse.BuildingSaveList.Add(new BuildingSave(Berry,4,1,pos,alterPos));
                Berry.transform.parent = this.transform.parent;
                i++;
            }
            else
            {
                Destroy(Berry);
            }
        }

        ColliderGenerateArea.enabled = false;
        islandManager.isShortGrassGen = true;
        islandManager.isOtherBiomeGen = false;
    }
}
