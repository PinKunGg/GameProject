using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefSaveSystem : MonoBehaviour
{
    //Save Location = Computer\HKEY_CURRENT_USER\SOFTWARE\Unity\UnityEditor\DefaultCompany\GameProject
    public static PlayerPrefSaveSystem PPSSInstanse;
    public float playerPosX,playerPosY,playerPosZ;
    private void Awake()
    {
        PPSSInstanse = this;

        playerPosX = PlayerPrefs.GetFloat("PlayerPositionX");
        playerPosY = PlayerPrefs.GetFloat("PlayerPositionY");
        playerPosZ = PlayerPrefs.GetFloat("PlayerPositionZ");
    }
    public void RecivePlayerPos(Vector3 playerPos)
    {
        SavePos(playerPos.x,playerPos.y,playerPos.z);
    }
    public Vector3 ExportPlayerPos()
    {
        return new Vector3(playerPosX,playerPosY,playerPosZ);
    }
    public void SavePos(float valueX, float valueY, float valueZ)
    {
        print("*--* Save PlayerPrefs *--*");
        PlayerPrefs.SetFloat("PlayerPositionX",valueX);
        PlayerPrefs.SetFloat("PlayerPositionY",valueY);
        PlayerPrefs.SetFloat("PlayerPositionZ",valueZ);
        
    }
    public void LoadPos()
    {
        print("*--* Load PlayerPrefs *--*");
        playerPosX = PlayerPrefs.GetFloat("PlayerPositionX",playerPosX);
        playerPosY = PlayerPrefs.GetFloat("PlayerPositionY",playerPosY);
        playerPosZ = PlayerPrefs.GetFloat("PlayerPositionZ",playerPosZ);
    }
}
