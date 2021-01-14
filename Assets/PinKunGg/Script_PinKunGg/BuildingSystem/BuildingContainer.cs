using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BuildingContainer
{
    public List<GameObject> BuildingCon;
    public List<Vector2> ZoneCheckSizeCon;
    public List<bool> isGroundBuildCon;
    public List<bool> isWaterBuildCon;
}
