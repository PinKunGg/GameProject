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
                Island =  Instantiate(IslandList[JsonSaveSystem.JSInstanse.MapData.IslandIndexOnMap[i]], JsonSaveSystem.JSInstanse.MapData.IslandPosOnMap[i], Quaternion.identity);
                IslandIndexSpawnList.Add(JsonSaveSystem.JSInstanse.MapData.IslandIndexOnMap[i]);
                IslandSpawnPosList.Add(JsonSaveSystem.JSInstanse.MapData.IslandPosOnMap[i]);
                Island.transform.parent = MapCollection.transform;
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
                JsonSaveSystem.JSInstanse.MapData.IslandIndexOnMap.Add(0);
                IslandIndexSpawnList.Add(0);
                JsonSaveSystem.JSInstanse.MapData.IslandPosOnMap.Add(new Vector3(0,0,-0.5f));
                IslandSpawnPosList.Add(new Vector3(0,0,-0.5f));
                IslandIndex++;
            }
            else
            {
                Vector3 IslandSpawnPos = new Vector3(0 + Mathf.FloorToInt(Random.Range(-IslandRandomGenPos,IslandRandomGenPos)),0 + Random.Range(-IslandRandomGenPos,IslandRandomGenPos) ,-0.5f);
                Island = Instantiate(IslandList[1], IslandSpawnPos, Quaternion.identity);
                yield return new WaitForSeconds(0.1f);
                if(Island.GetComponent<IslandSafeArea>().isOverlap == false)
                {
                    Island.transform.parent = MapCollection.transform;
                    JsonSaveSystem.JSInstanse.MapData.IslandIndexOnMap.Add(1);
                    IslandIndexSpawnList.Add(1);
                    JsonSaveSystem.JSInstanse.MapData.IslandPosOnMap.Add(IslandSpawnPos);
                    IslandSpawnPosList.Add(IslandSpawnPos);
                    i++;
                    IslandIndex++;
                }
                else
                {
                    Destroy(Island);
                }
            }
        }
        JsonSaveSystem.JSInstanse.MapSaveJson();
        FlatGenerate();

        //Loading.SetActive(false);
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
        Loading.SetActive(false);
    }
}