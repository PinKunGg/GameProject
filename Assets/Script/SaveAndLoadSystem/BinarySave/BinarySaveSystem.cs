using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class BinarySaveSystem : MonoBehaviour
{
    public static BinarySaveSystem BSSInstanse;
    public PlayerSaveData player;
    private void Awake()
    {
        if(BSSInstanse == null)
        {
            BSSInstanse = this;
        }
    }
    #region Player Save & Load
    public void SavePlayerData()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string DirecPath = Application.dataPath + "/SaveBinaryData/PlayerSaveData";

        if(Directory.Exists(DirecPath))
        {
            Debug.Log("*--* Save 'PLAYER' Json *--*");
            string path = DirecPath + "/Player.gpj";
            FileStream stream = new FileStream(path, FileMode.Create);

            formatter.Serialize(stream,player);
            stream.Close();
        }
        else
        {
            Debug.LogError("*--* Directory not found *--*");
            Debug.Log("Create new directory and save");
            Directory.CreateDirectory(DirecPath);
            string pathNewCreate = DirecPath + "/Player.gpj";
            FileStream stream = new FileStream(pathNewCreate, FileMode.Create);

            formatter.Serialize(stream,player);
            stream.Close();
        }
    }
    public PlayerSaveData LoadPlayerData()
    {
        string DirecPath = Application.persistentDataPath + "/SaveBinaryData/PlayerSaveData";
        string path = DirecPath + "/Player.gpj";
        BinaryFormatter formatter = new BinaryFormatter();

        if(File.Exists(path))
        {
            Debug.Log("Load 'Inventory' save file data");
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerSaveData playerLoadData = formatter.Deserialize(stream) as PlayerSaveData;
            stream.Close();

            return playerLoadData;
        }
        else
        {
            Debug.LogError("*--* SAVE 'INVENTORY' FILE NOT FOUND *--*" + path);
            Debug.Log("*--* CREATE NEW 'INVENTORY' SAVE FILE *--*");
            FileStream stream = new FileStream(path, FileMode.Create);
            stream.Close();
            return null;
        }
    }
    #endregion
}
