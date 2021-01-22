using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GenerateMap : MonoBehaviour
{
    public static GenerateMap GenMapInstanse;
    private GameObject Island;
    public List<GameObject> IslandList;
    public List<int> IslandIndexSpawnList;
    public List<Vector3> IslandSpawnPosList;
    public GameObject Loading;
    public bool GenNextIsland;
    [SerializeField]private GameObject MapCollection;
    [SerializeField]private int GenX, GenY, IslandCount, IslandRandomGenPos;
    [SerializeField] Tilemap OceanTileMap;
    [SerializeField] Tile OceanTile;
    bool isFirstIslandGen;
    int IslandIndex;
    private void Awake()
    {
        if(GenMapInstanse == null)
        {
            GenMapInstanse = this;
        }
    }
    private void Start()
    {
        JsonSaveSystem.JSInstanse.MapLoadJson();
        SaveAndLoadInvoke.SALIKinstanse.AddSavingEventLisener(SaveMap);
        SaveAndLoadInvoke.SALIKinstanse.AddLoadingEventLisener(LoadMap);
        //StartCoroutine(IslandGenerate());

        Loading.SetActive(true);
    }
    void SaveMap()
    {
        JsonSaveSystem.JSInstanse.MapSaveJson();
    }
    void LoadMap()
    {
        if(JsonSaveSystem.JSInstanse.MapData.IslandIndexOnMap.Count == 0)
        {
            StartCoroutine(IslandGenerate());
        }
        if(JsonSaveSystem.JSInstanse.MapData.IslandIndexOnMap.Count != 0)
        {
            for(int i = 0; i < JsonSaveSystem.JSInstanse.MapData.IslandIndexOnMap.Count; i++)
            {
                Debug.Log("11111");
                Island =  Instantiate(IslandList[JsonSaveSystem.JSInstanse.MapData.IslandIndexOnMap[i]], JsonSaveSystem.JSInstanse.MapData.IslandPosOnMap[i], Quaternion.identity);
                IslandIndexSpawnList.Add(JsonSaveSystem.JSInstanse.MapData.IslandIndexOnMap[i]);
                IslandSpawnPosList.Add(JsonSaveSystem.JSInstanse.MapData.IslandPosOnMap[i]);
                Island.transform.parent = MapCollection.transform;
                Island.name = "Island_" + i + " / " + Island.transform.position;
            }
            FlatGenerate();
        }
    }
    IEnumerator IslandGenerate()
    {
        for(int i = 0; i < IslandCount;)
        {
            if(isFirstIslandGen == false)
            {
                isFirstIslandGen = true;
                Island = Instantiate(IslandList[0], new Vector3(0,0,-0.5f), Quaternion.identity);
                Island.transform.parent = MapCollection.transform;
                Debug.Log("222222222");
                IslandIndexSpawnList.Add(0);
                IslandSpawnPosList.Add(new Vector3(0,0,-0.5f));
                Island.name = "Island_" + i + " / " + Island.transform.position;
                GenNextIsland = false;
                Island.GetComponentInChildren<IslandManager>().StartGenerateBiome();
                    
                a:
                while(!GenNextIsland)
                {
                    yield return new WaitForSeconds(0.01f);
                    goto a;
                }

                if(IslandIndex < IslandList.Count - 1)
                {
                    IslandIndex++;
                }
                else
                {
                    IslandIndex = 1;
                }
                
            }
            else
            {
                Vector3 IslandSpawnPos = new Vector3(0 + Mathf.FloorToInt(Random.Range(-IslandRandomGenPos,IslandRandomGenPos)),0 + Random.Range(-IslandRandomGenPos,IslandRandomGenPos) ,-0.5f);
                Island = Instantiate(IslandList[IslandIndex], IslandSpawnPos, Quaternion.identity);
                yield return new WaitForSeconds(0.1f);
                
                
                if(Island.GetComponent<IslandSafeArea>().isOverlap == false)
                {
                    Debug.Log("33333333333");
                    Island.transform.parent = MapCollection.transform;
                    IslandIndexSpawnList.Add(IslandIndex);
                    IslandSpawnPosList.Add(IslandSpawnPos);
                    Island.name = "Island_" + i + " / " + Island.transform.position;
                    GenNextIsland = false;
                    Island.GetComponentInChildren<IslandManager>().StartGenerateBiome();
                    
                    a:
                    while(!GenNextIsland)
                    {
                        yield return new WaitForSeconds(0.01f);
                        goto a;
                    }

                    i++;
                    if(IslandIndex < IslandList.Count - 1)
                    {
                        IslandIndex++;
                    }
                    else
                    {
                        IslandIndex = 1;
                    }
                    
                }
                else
                {
                    Destroy(Island);
                }
            }
        }

        for(int j = 0; j < IslandIndexSpawnList.Count; j++)
        {
            Debug.Log("4444444444");
            JsonSaveSystem.JSInstanse.MapData.IslandIndexOnMap.Add(IslandIndexSpawnList[j]);
            JsonSaveSystem.JSInstanse.MapData.IslandPosOnMap.Add(IslandSpawnPosList[j]);
        }
        JsonSaveSystem.JSInstanse.MapSaveJson();
        FlatGenerate();
    }
    private void FlatGenerate()
    {
        for(int i = -(GenX / 2); i < GenX / 2; i ++)
        {
            for(int j = -(GenY / 2); j < GenY / 2; j ++)
            {
                if(j < 0)
                {
                    Vector3Int SpawnPos = new Vector3Int((int)transform.position.x + i, (int)transform.position.y + j, (int)transform.position.z);
                    OceanTileMap.SetTile(SpawnPos,OceanTile);
                }
                else
                {
                    Vector3Int SpawnPos = new Vector3Int((int)transform.position.x + i, (int)transform.position.y + j, (int)transform.position.z);
                    OceanTileMap.SetTile(SpawnPos,OceanTile);
                }
            }
        }
        GameObject.Find("Player").GetComponent<Collider2D>().enabled = true;
        foreach(var item in MapCollection.GetComponentsInChildren<IslandSafeArea>())
        {
            item.isGenMapFinish = true;
        }
        Loading.SetActive(false);
    }
}