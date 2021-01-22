using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerController : MonoBehaviour
{
    Vector3 PlayerPos;
    public Vector3 GetSetPlayerPos
    {
        get{ return PlayerPos; }
        set{ this.transform.position = value; }
    }
    private void Awake()
    {
        GetComponent<Collider2D>().enabled = false;
    }
    private void Start()
    {
        SaveAndLoadInvoke.SALIKinstanse.AddSavingEventLisener(SavePlayerData);
        SaveAndLoadInvoke.SALIKinstanse.AddLoadingEventLisener(LoadPlayerData);    
    }
    private void Update()
    {
        PlayerPos = this.transform.position;
    }
    public void SavePlayerData()
    {
        JsonSaveSystem.JSInstanse.PlayerData.playerPos = PlayerPos;
        JsonSaveSystem.JSInstanse.PlayerSaveJson();
    }
    public void LoadPlayerData()
    {
        JsonSaveSystem.JSInstanse.PlayerLoadJson();
        this.transform.position = JsonSaveSystem.JSInstanse.PlayerData.playerPos;
    }
}
