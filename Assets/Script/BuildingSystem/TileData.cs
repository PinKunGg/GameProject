﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Data/TileMap Data")]
public class TileData : ScriptableObject
{
    public List<TileBase> tiles;
    public bool isGround;
    public bool isWater;
    public bool isBeach;
    public bool isSand;
    public bool isShortGrass;
    public bool isMountainZone;
}
