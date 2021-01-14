using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Building Data")]
public class BuildingData : ScriptableObject
{
    public GameObject Building;
    public Vector2 BuildingZoneSize;
    public int BuildingType;
    public bool isGroundBuildCon;
    public bool isWaterBuildCon;
}
