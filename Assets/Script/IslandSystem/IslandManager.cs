using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class IslandManager : MonoBehaviour
{
    public List<GameObject> BiomeList;
    public List<GameObject> BiomeColliderList;
    public bool isForestGen, isBeachGen, isShortGrassGen, isMountainGen, isOtherBiomeGen;
    public List<GameObject> TreeList, BerryList, StoneList;
    public int TreeCount, BurryCount, StoneCount;
    public Collider2D ColliderS;
    public bool StartGenEnvironment;
    int MinGenX, MaxGenX, MinGenY, MaxGenY;
    GameObject Environment;

    public int GetGenPos(int index)
    {
        if(index == 0)
        {
            return MinGenX;
        }
        else if(index == 1)
        {
            return MaxGenX;
        }
        else if(index == 2)
        {
            return MinGenY;
        }
        else if(index == 3)
        {
            return MaxGenY;
        }
        else
        {
            return 0;
        }
    }
    private void Start()
    {
        MinGenX = (int)ColliderS.bounds.min.x;
        MaxGenX = (int)ColliderS.bounds.max.x;
        MinGenY = (int)ColliderS.bounds.min.y;
        MaxGenY = (int)ColliderS.bounds.max.y;
        JsonSaveSystem.JSInstanse.BuildingLoadJson();
        
        if(JsonSaveSystem.JSInstanse.MapData.IslandIndexOnMap.Count != 0)
        {
            foreach(var item in BiomeColliderList)
            {
                item.GetComponent<Collider2D>().enabled = false;
            }
        }

        if(StartGenEnvironment)
        {
            StartGenerateBiome();
        }
    }
    public void StartGenerateBiome()
    {
        BuildManager.BMinstanse.adjIslandPos = Vector3.zero;
        TileMapReader.TMRinstanse.tilemap = GetComponentInParent<IslandSafeArea>().tilemap;
        BuildManager.BMinstanse.adjIslandPos = this.transform.parent.position;
        StartCoroutine(Generated());
    }
    IEnumerator Generated()
    {
        while(!isForestGen || !isBeachGen || !isShortGrassGen || !isMountainGen)
        {
            yield return new WaitForSeconds(0.1f);

            if(!isForestGen && !isOtherBiomeGen)
            {
                isOtherBiomeGen = true;
                if(BiomeList[0] != null)
                {
                    StartCoroutine(BiomeList[0].GetComponent<ForestGenerateSys>().GenerateTree());
                }
                else
                {
                    isOtherBiomeGen = false;
                    isForestGen = true;
                }
            }
            if(!isBeachGen && !isOtherBiomeGen)
            {
                isOtherBiomeGen = true;
                if(BiomeList[1] != null)
                {
                    //StartCoroutine(BiomeList[1].GetComponent<ForestGenerateSys>().GenerateTree());
                    isOtherBiomeGen = false;
                    isBeachGen = true;
                }
                else
                {
                    isOtherBiomeGen = false;
                    isBeachGen = true;
                }
            }
            if(!isShortGrassGen && !isOtherBiomeGen)
            {
                isOtherBiomeGen = true;
                if(BiomeList[2] != null)
                {
                    StartCoroutine(BiomeList[2].GetComponent<ShortGrassGenerateSys>().GenerateBurry());
                }
                else
                {
                    isOtherBiomeGen = false;
                    isShortGrassGen = true;
                }
            }
            if(!isMountainGen && !isOtherBiomeGen)
            {
                isOtherBiomeGen = true;
                if(BiomeList[3] != null)
                {
                    StartCoroutine(BiomeList[3].GetComponent<MountainGenerateSys>().GenerateStone());
                }
                else
                {
                    isOtherBiomeGen = false;
                    isMountainGen = true;
                }
            }
        }

        GenerateMap.GenMapInstanse.GenNextIsland = true;
        TileMapReader.TMRinstanse.tilemap = null;
    }
}
