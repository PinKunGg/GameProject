using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BuildingSaveData
{
    public List<int> BuildingIndexOnMap;
    public List<int> BuildingSize;
    public List<Vector3Int> BuildingPosOnMap;
}