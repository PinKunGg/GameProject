using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildManager : MonoBehaviour
{
    public static BuildManager BMinstanse;
    public Vector2 adjIslandPos;
    public GameObject BuildingCollection;
    public TileMapSelectMarker TileMapSelectMarker;
    public List<BuildingSave> BuildingSaveList = new List<BuildingSave>();
    public BuidlingPositionCheck AllBuidlingPositionCheckList;
    public bool[] AreaCheck;

    [SerializeField] TileMapReader tileMapReader;
    [SerializeField] BuildingContainer BuildingContainer;
    [SerializeField] BuildingData[] BuildingDataList;
    [SerializeField] Vector2 ZoneSize = new Vector2(1,1);
    [SerializeField] float MaxBuildDistanse = 2f;

    GameObject Player;
    Vector3Int selectTilePos;
    bool isSelect, inBuildMode, isArrayPosCreate, isReadyToBuild;
    int BuildingType = 0, BuildingIndex = 0;

    public bool GetSetinBuildMode
    {
        get
        {
            return inBuildMode;
        }
        set
        {
            inBuildMode = value;
        }
    }

    private void Awake()
    {
        if(BMinstanse == null)
        {
            BMinstanse = this;
        }
        Player = GameObject.Find("Player");
    }

    private void Start()
    {
        AllBuidlingPositionCheckList.AllBuildPositionSave = new List<Vector3Int>();
        AllBuidlingPositionCheckList.AllBuildPositionSave.Add(Vector3Int.FloorToInt(Player.transform.position));
        
        for(int i = 0; i < BuildingDataList.Length; i++)
        {
            BuildingContainer.BuildingCon.Add(BuildingDataList[i].Building);
            BuildingContainer.ZoneCheckSizeCon.Add(BuildingDataList[i].BuildingZoneSize);
            BuildingContainer.isGroundBuildCon.Add(BuildingDataList[i].isGroundBuildCon);
            BuildingContainer.isWaterBuildCon.Add(BuildingDataList[i].isWaterBuildCon);
        }

        JsonSaveSystem.JSInstanse.BuildingLoadJson();

        SaveAndLoadInvoke.SALIKinstanse.AddSavingEventLisener(SaveBuilding);
        SaveAndLoadInvoke.SALIKinstanse.AddLoadingEventLisener(LoadBuilding);
    }

    private void Update()
    {
        AllBuidlingPositionCheckList.AllBuildPositionSave[0] = Vector3Int.FloorToInt(Player.transform.position); //ทำให้ไม่สามารถสร้างในจุดที่ "Player" ยืนอยู่ได้

        if(Input.GetKeyDown(KeyCode.Tab)) //toggle buildmode
        {
            inBuildMode = !inBuildMode;
        }

        if(inBuildMode)
        {
            SelectTile();
            SelectTileDistanceCheck();
            BuildAndDestroyBuilding();
        }
        else
        {
            isArrayPosCreate = false;
            
            if(TileMapSelectMarker != null)
            {
                TileMapSelectMarker.ResetGrid();
            }
        }
    }
    void SaveBuilding()
    {
        if(BuildingSaveList.Count != 0)
        {
            JsonSaveSystem.JSInstanse.BuildingData.BuildingIndexOnMap.Clear();
            JsonSaveSystem.JSInstanse.BuildingData.BuildingSize.Clear();
            JsonSaveSystem.JSInstanse.BuildingData.BuildingPosOnMap.Clear();

            for(int i = 0; i < BuildingSaveList.Count;i++)
            {
                JsonSaveSystem.JSInstanse.BuildingData.BuildingIndexOnMap.Add(BuildingSaveList[i].BuildingIndex);
                JsonSaveSystem.JSInstanse.BuildingData.BuildingSize.Add(BuildingSaveList[i].BuildingSize);

                for(int j = 0; j < BuildingSaveList[i].SavePosition.Count;j++)
                {
                    JsonSaveSystem.JSInstanse.BuildingData.BuildingPosOnMap.Add(BuildingSaveList[i].SavePosition[j]);
                }
            }
        }
        else
        {
            JsonSaveSystem.JSInstanse.BuildingData.BuildingIndexOnMap.Clear();
            JsonSaveSystem.JSInstanse.BuildingData.BuildingSize.Clear();
            JsonSaveSystem.JSInstanse.BuildingData.BuildingPosOnMap.Clear();
        }

        JsonSaveSystem.JSInstanse.BuildingSaveJson();
    }
    void LoadBuilding()
    {
        int LastPosIndex = 0; //ใช้สำหรับกำหนดลำดับเริ่มต้นของ Position Building ตัวต่อไป
        Vector3Int[] SavePos = new Vector3Int[0]; //ใช้สำหรับเก็บข้อมูล Position ของ Building ตัวนั้น ๆ
        
        if(JsonSaveSystem.JSInstanse.BuildingData.BuildingIndexOnMap.Count != 0) //ถ้ามีข้อมูล
        {
            for(int i = 0; i < JsonSaveSystem.JSInstanse.BuildingData.BuildingIndexOnMap.Count; i++) //นับ Building ทั้งหมด
            {
                SavePos = new Vector3Int[JsonSaveSystem.JSInstanse.BuildingData.BuildingSize[i]]; //สร้าง Array ตามขนาดของ Building นั้น
                for(int x = 0; x < SavePos.Length; x++) //Reset SpawnPos เพื่อใช้สำหรับ Building ตัวต่อไป
                {
                    SavePos[x] = Vector3Int.zero; //ลบข้อมูลทั้งหมดใน Array SavePos
                }
                GameObject building = Instantiate(BuildingContainer.BuildingCon[JsonSaveSystem.JSInstanse.BuildingData.BuildingIndexOnMap[i]]); //สร้าง Building จาก Index ที่ save ไว้

                for(int j = 0; j < JsonSaveSystem.JSInstanse.BuildingData.BuildingSize[i]; j++) //นับว่าขนาดพื้นที่ของ Building นี้มีขนาดเท่าไร
                {
                    building.transform.position = JsonSaveSystem.JSInstanse.BuildingData.BuildingPosOnMap[LastPosIndex]; //กำหนด Position ของ Building นี้
                    building.transform.parent = BuildingCollection.transform; //นำ Building ไปเก็บไว้ใน Gameobject "BuildingCollection"
                    
                    SavePos[j] = JsonSaveSystem.JSInstanse.BuildingData.BuildingPosOnMap[LastPosIndex + j]; //set position ทั้งหมดของ Building ใน Array SavePos
                    AllBuidlingPositionCheckList.AllBuildPositionSave.Add(SavePos[j]); //นำ Position ทั้งหมดใน file save คืนค่าให้กับ "ตัว check position ในการสร้าง" ทั้งหมด 
                }
                BuildingSaveList.Add(new BuildingSave(building,JsonSaveSystem.JSInstanse.BuildingData.BuildingIndexOnMap[i],JsonSaveSystem.JSInstanse.BuildingData.BuildingSize[i],SavePos)); //set ค่าคืนให้กับ BuildingSaveList เพื่อใช้ในการ "ลบ" Building หาก player ต้องการ
                LastPosIndex += JsonSaveSystem.JSInstanse.BuildingData.BuildingSize[i]; //กำหนดจุดเริ่มต้นของ Position ใน Array ตัวต่อไป
            }
        }
    }
    void SelectTile() //ส่งพิกัดไปให้ Tilemap แสดงว่ากำลังเลือกพื้นที่ไหนอยู่
    {
        selectTilePos = tileMapReader.GetGridPos(Input.mousePosition, true); //return ค่าจาก scirpt TileMapReader

        /*
        !Note: หลังจากที่คำนวณ position ใน script TileMapReader ใหม่แล้ว TileMapSelectMark.markCellPos ก็จะเป็น position ที่ถูกคำนวณใหม่เหมือนกัน
                !ฉนั้นเลยไม่ต้องเอา TileMapSelectMark.marCellPos ไป + กับ adjIslandPos อีก
        */
        
    }
    void SelectTileDistanceCheck() //check ว่าอยู่ใน Area สร้างรึป่าว
    {
        Vector2 playerPos = Player.transform.localPosition;

        Vector2 camPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        isSelect = Vector2.Distance(playerPos,(Vector2Int)selectTilePos) < MaxBuildDistanse; //check ว่า พื้นที่นั้นยังอยู่ใน area ที่สามารถสร้างได้รึป่าว

        TileMapSelectMarker.ShowSelect(isSelect, isReadyToBuild);
    }
    void BuildAndDestroyBuilding() //Method สร้าง กับ ลบ
    {
        float index = ZoneSize.x * ZoneSize.y;

        if(isArrayPosCreate == false) //สร้าง Array ครั้งแรก
        {
            isArrayPosCreate = true;
            AreaCheck = new bool[(int)index];
            TileMapSelectMarker.markCellPos = new Vector3Int[(int)index];
            TileMapSelectMarker.oldmarkCellPos = new Vector3Int[(int)index];
        }

        if(isSelect)
        {
            SelectBuilding();
            CreateBuilding();
            DestroyBuilding();
        }
        if(isArrayPosCreate == true)
        {
            CreateAreaChecker();
        }
    }
    void SelectBuilding()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1)) //Previous Building
        {
            if(BuildingIndex > 0)
            {
                TileMapSelectMarker.markCellPos[0] = selectTilePos;
                BuildingIndex--;

                BuildingIndex = Mathf.Clamp(BuildingIndex,0,BuildingDataList.Length - 1);
                isArrayPosCreate = false;
                BuildingType = BuildingDataList[BuildingIndex].BuildingType;
                ZoneSize = BuildingContainer.ZoneCheckSizeCon[BuildingIndex];
                TileMapSelectMarker.ResetGrid();
            }
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2)) //Next Building
        {
            if(BuildingIndex < BuildingDataList.Length - 1)
            {
                TileMapSelectMarker.markCellPos[0] = selectTilePos;
                BuildingIndex++;

                BuildingIndex = Mathf.Clamp(BuildingIndex,0,BuildingDataList.Length - 1);
                isArrayPosCreate = false;
                BuildingType = BuildingDataList[BuildingIndex].BuildingType;
                ZoneSize = BuildingContainer.ZoneCheckSizeCon[BuildingIndex];
                TileMapSelectMarker.ResetGrid();
            }
        }
    }
    void CreateBuilding()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            if(isReadyToBuild)
            {
                CreateBuildingTile(BuildingIndex);
            }
            else
            {
                return;
            }
        }
    }
    void DestroyBuilding()
    {
        if(Input.GetButtonDown("Fire2"))
        {
            int objSize = 0;

            for(int i = 0; i < AllBuidlingPositionCheckList.AllBuildPositionSave.Count; i++) //check position ของใน list ใหญ่ทั้งหมด
            {
                if(TileMapSelectMarker.markCellPos[0] == AllBuidlingPositionCheckList.AllBuildPositionSave[i]) //ถ้า mouse position เท่ากับ position ใน list ใหญ่
                {
                    for(int x = 0; x < BuildingSaveList.Count; x++) //check list ของ building ที่ save ไว้ใน list ย้อย
                    {
                        for(int j = 0; j < BuildingSaveList[x].SavePosition.Count; j++) //check postion ใน list ย้อยทั้งหมด
                        {
                            if(BuildingSaveList[x].SavePosition[j] == AllBuidlingPositionCheckList.AllBuildPositionSave[i]) // ถ้า position ใน list ย้อยเท่ากับ position ใน list ใหญ
                            {
                                AllBuidlingPositionCheckList.AllBuildPositionSave.Remove(BuildingSaveList[x].SavePosition[j]); //ลบ postion ใน list ใหญ่
                                objSize++;
                                    
                                if(objSize == BuildingSaveList[x].SavePosition.Count) //ถ้าลบใน list ใหญ่หมดแล้วค่อยลบใน list ย้อย
                                {
                                    Destroy(BuildingSaveList[x].Building.gameObject);
                                    BuildingSaveList[x].SavePosition = null;
                                    BuildingSaveList.RemoveAt(x);
                                    objSize = 0;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    void CreateAreaChecker() //สร้างตัว check พื้นที่โดยใช้ mouse position เป็นจุดเริ่มต้น
    {
        int size = 0;
        
        TileMapSelectMarker.markCellPos[0] = selectTilePos; //mouse position

        for(int x = TileMapSelectMarker.markCellPos[0].x; x < (TileMapSelectMarker.markCellPos[0].x + ZoneSize.x); x++)
        {
            for(int y = TileMapSelectMarker.markCellPos[0].y; y < (TileMapSelectMarker.markCellPos[0].y + ZoneSize.y); y++)
            {
                TileMapSelectMarker.markCellPos[size] = new Vector3Int(x,y,0);

                if(isSelect) //bool check ว่าตัวพื้นที่เราเลือกนั้นอยู่ในระยะที่สร้างได้รึป่าว
                {
                    TileBase tileBase = tileMapReader.GetTileBase(TileMapSelectMarker.markCellPos[size] - (Vector3Int.FloorToInt(adjIslandPos)));
                    TileData tileData = tileMapReader.GetTileData(tileBase);

                    if(tileData != null) //check ว่ามี Tilemap ให้เราเลือกรึป่าว
                    {
                        if(BuildingType == 0)
                        {
                            AreaCheck[size] = (tileData.isGround == true && tileData.isWater == false);
                        }
                        else if(BuildingType == 1)
                        {
                            AreaCheck[size] = (tileData.isGround == false && tileData.isWater == true);
                        }
                        else if(BuildingType == 2)
                        {
                            AreaCheck[size] = (tileData.isGround == true || tileData.isWater == true);
                        }
                    }
                    else
                    {
                        AreaCheck[size] = false;
                    }
                    
                    AreaCheckMethod(); //ตัว check ว่าพื้นที่ "ทั้งหมด" ที่เราเลือกนั้นสามารถสร้างได้รึป่าว
                }
                size++;
            }
        }
    }
    void AreaCheckMethod()
    {
        if(AreaCheck.All(ac => ac)) //check ว่าพื้นที่ "ทั้งหมด" ที่เราเลือกนั้นสามารถสร้างได้รึป่าว
        {
            for(int i = 0; i < TileMapSelectMarker.markCellPos.Length; i++)
            {
                foreach(var item in AllBuidlingPositionCheckList.AllBuildPositionSave) //check ว่าพื้นที่นั้นมรสิ่งก่อสร้างอื่นรึป่าว
                {
                    if(TileMapSelectMarker.markCellPos[i] == item)
                    {
                        isReadyToBuild = false;
                        break;
                    }
                    else
                    {
                        isReadyToBuild = true;
                    }
                }
                if(isReadyToBuild == false)
                {
                    break;
                }
            }
        }
        else
        {
            isReadyToBuild = false;
        }
    }
    void CreateBuildingTile(int BuildingIndex) //สร้าง
    {
        float size = ZoneSize.x * ZoneSize.y;

        GameObject building = Instantiate(BuildingContainer.BuildingCon[BuildingIndex]);
        building.transform.position = TileMapSelectMarker.markCellPos[0];
        building.transform.parent = BuildingCollection.transform;
        for(int i = 0; i < TileMapSelectMarker.markCellPos.Length; i++)
        {
            AllBuidlingPositionCheckList.AllBuildPositionSave.Add(TileMapSelectMarker.markCellPos[i]);
        }
        BuildingSaveList.Add(new BuildingSave(building, BuildingIndex, (int)size,TileMapSelectMarker.markCellPos));
    }
    private void OnDrawGizmos()
    {
        if(Player != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(Player.transform.position,MaxBuildDistanse);
        }
    }
}
[System.Serializable]
public class BuildingSave
{
    public GameObject Building;
    public List<Vector3Int> SavePosition;
    public int BuildingSize;
    public int BuildingIndex;

    public BuildingSave(GameObject obj, int index, int size, Vector3Int[] pos)
    {
        Building = obj;
        BuildingIndex = index;
        BuildingSize = size;

        SavePosition = new List<Vector3Int>();
    
        foreach(var item in pos)
        {
            SavePosition.Add(item);
        }
    }
}
[System.Serializable]
public class BuidlingPositionCheck
{
    public List<Vector3Int> AllBuildPositionSave;
}
