using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class JsonSaveSystem : MonoBehaviour
{
    public static JsonSaveSystem JSInstanse;
    public MapSaveData MapData;
    public BuildingSaveData BuildingData;
    public InventorySaveData InventData;
    public PlayerSaveData PlayerData;

    private void Awake()
    {
        if(JSInstanse == null)
        {
            JSInstanse = this; 
        }
    }
    #region Map Save & Load
    public void MapSaveJson()
    {
        string json = JsonUtility.ToJson(MapData);
        MapSaveData(json);
    }
    public void MapLoadJson()
    {
        string json = MapLoadData();
        JsonUtility.FromJsonOverwrite(json,MapData);
    }
    private void MapSaveData(string json)
    {
        string DirecPath = Application.dataPath + "/SaveJsonData/MapSaveData";
        string path = DirecPath + "/MapSaveJson_" + SceneManager.GetActiveScene().buildIndex + ".json";

        if(Directory.Exists(DirecPath))
        {
            if(File.Exists(path))
            {
                Debug.Log("*--* Save 'MAP' Json *--*");
                FileStream stream = new FileStream(path,FileMode.Create);

                StreamWriter writer = new StreamWriter(stream);
                writer.Write(json);
                writer.Close();
                stream.Close();
            }
            else
            {
                Debug.LogError("*--* Save 'MAP' File Json Not Found *--*");
                Debug.Log("*--* Create New Save 'MAP' Json *--*");
                FileStream stream = new FileStream(path,FileMode.Create);
                stream.Close();
                MapSaveJson();
            }
        }
        else
        {
            Debug.LogError("*--* Directory 'MapSaveData' Not Found *--*");
            Debug.Log("*--* Create New 'MapSaveData' Directory *--*");
            Directory.CreateDirectory(DirecPath);
            FileStream stream = new FileStream(path,FileMode.Create);
            stream.Close();
            MapSaveJson();
        }

        
    }
    private string MapLoadData()
    {
        string DirecPath = Application.dataPath + "/SaveJsonData/MapSaveData";
        string path = DirecPath + "/MapSaveJson_" + SceneManager.GetActiveScene().buildIndex + ".json";

        if(Directory.Exists(DirecPath))
        {
            if(File.Exists(path))
            {
                Debug.Log("*--* Load 'MAP' Json *--*");
                StreamReader reader = new StreamReader(path);
                string json = reader.ReadToEnd();
                reader.Close();
                return json;
            }
            else
            {
                Debug.LogError("*--* Save 'MAP' File Json Not Found");
                Debug.Log("*--* Create New Save 'MAP' Json *--*");
                FileStream stream = new FileStream(path,FileMode.Create);
                stream.Close();
                MapSaveJson();
                return null;
            }
        }
        else
        {
            Debug.LogError("*--* Directory 'MapSaveData' Not Found *--*");
            Debug.Log("*--* Create New 'MapSaveData' Directory *--*");
            Directory.CreateDirectory(DirecPath);
            FileStream stream = new FileStream(path,FileMode.Create);
            stream.Close();
            MapSaveJson();
            return null;
        }
    }
    #endregion

    #region Building Save & Load

    public void BuildingSaveJson()
    {
        string json = JsonUtility.ToJson(BuildingData);
        BuildingSaveData(json);
    }
    public void BuildingLoadJson()
    {
        string json = BuildingLoadData();
        JsonUtility.FromJsonOverwrite(json,BuildingData);
    }
    private void BuildingSaveData(string json)
    {
        string DirecPath = Application.dataPath + "/SaveJsonData/BuildingSaveData";
        string path = DirecPath + "/BuildingSaveJson_" + SceneManager.GetActiveScene().buildIndex + ".json";

        if(Directory.Exists(DirecPath))
        {
            if(File.Exists(path))
            {
                Debug.Log("*--* Save 'Building' Json *--*");
                FileStream stream = new FileStream(path,FileMode.Create);

                StreamWriter writer = new StreamWriter(stream);
                writer.Write(json);
                writer.Close();
                stream.Close();
            }
            else
            {
                Debug.LogError("*--* Save 'Building' File Json Not Found *--*");
                Debug.Log("*--* Create New Save 'Building' Json *--*");
                FileStream stream = new FileStream(path,FileMode.Create);
                stream.Close();
                BuildingSaveJson();
            }
        }
        else
        {
            Debug.LogError("*--* Directory 'BuildingSaveData' Not Found *--*");
            Debug.Log("*--* Create New 'BuildingSaveData' Directory *--*");
            Directory.CreateDirectory(DirecPath);
            FileStream stream = new FileStream(path,FileMode.Create);
            stream.Close();
            BuildingSaveJson();
        }

        
    }
    private string BuildingLoadData()
    {
        string DirecPath = Application.dataPath + "/SaveJsonData/BuildingSaveData";
        string path = DirecPath + "/BuildingSaveJson_" + SceneManager.GetActiveScene().buildIndex + ".json";

        if(Directory.Exists(DirecPath))
        {
            if(File.Exists(path))
            {
                Debug.Log("*--* Load 'Building' Json *--*");
                StreamReader reader = new StreamReader(path);
                string json = reader.ReadToEnd();
                reader.Close();
                return json;
            }
            else
            {
                Debug.LogError("*--* Save 'Building' File Json Not Found");
                Debug.Log("*--* Create New Save 'Building' Json *--*");
                FileStream stream = new FileStream(path,FileMode.Create);
                stream.Close();
                BuildingSaveJson();
                return null;
            }
        }
        else
        {
            Debug.LogError("*--* Directory 'BuildingSaveData' Not Found *--*");
            Debug.Log("*--* Create New 'BuildingSaveData' Directory *--*");
            Directory.CreateDirectory(DirecPath);
            FileStream stream = new FileStream(path,FileMode.Create);
            stream.Close();
            BuildingSaveJson();
            return null;
        }
    }

    #endregion

    #region Inventory Save & Load
    public void InventSaveJson()
    {
        string json = JsonUtility.ToJson(InventData);
        InventSaveData(json);
    }
    public void InventLoadJson()
    {
        string json = InventLoadData();
        JsonUtility.FromJsonOverwrite(json,InventData);
    }
    private void InventSaveData(string json)
    {
        string DirecPath = Application.dataPath + "/SaveJsonData/InventSaveData";
        string path = DirecPath + "/InventSaveJson.json";

        if(Directory.Exists(DirecPath))
        {
            if(File.Exists(path))
            {
                Debug.Log("*--* Save 'INVENTORY' Json *--*");
                FileStream stream = new FileStream(path,FileMode.Create);

                StreamWriter writer = new StreamWriter(stream);
                writer.Write(json);
                writer.Close();
                stream.Close();
            }
            else
            {
                Debug.LogError("*--* Save 'INVENTORY' File Json Not Found *--*");
                Debug.Log("*--* Create New Save 'INVENTORY' Json *--*");
                FileStream stream = new FileStream(path,FileMode.Create);
                stream.Close();
                InventSaveJson();
            }
        }
        else
        {
            Debug.LogError("*--* Directory 'MapSaveData' Not Found *--*");
            Debug.Log("*--* Create New 'MapSaveData' Directory *--*");
            Directory.CreateDirectory(DirecPath);
            FileStream stream = new FileStream(path,FileMode.Create);
            stream.Close();
            InventSaveJson();
        }
    }
    private string InventLoadData()
    {
        string DirecPath = Application.dataPath + "/SaveJsonData/InventSaveData";
        string path = DirecPath + "/InventSaveJson.json";

        if(Directory.Exists(DirecPath))
        {
            if(File.Exists(path))
            {
                Debug.Log("*--* Load 'INVENTORY' Json *--*");
                StreamReader reader = new StreamReader(path);
                string json = reader.ReadToEnd();
                return json;
            }
            else
            {
                Debug.LogError("*--* Save 'INVENTORY' File Json Not Found");
                Debug.Log("*--* Create New Save 'INVENTORY' Json *--*");
                FileStream stream = new FileStream(path,FileMode.Create);
                stream.Close();
                InventSaveJson();
                return null;
            }
        }
        else
        {
            Debug.LogError("*--* Directory 'MapSaveData' Not Found *--*");
            Debug.Log("*--* Create New 'MapSaveData' Directory *--*");
            Directory.CreateDirectory(DirecPath);
            FileStream stream = new FileStream(path,FileMode.Create);
            stream.Close();
            InventSaveJson();
            return null;
        }
    }
    #endregion

    #region Player Save & Load
    public void PlayerSaveJson()
    {
        string json = JsonUtility.ToJson(PlayerData);
        PlayerSaveData(json);
    }
    public void PlayerLoadJson()
    {
        string json = PlayerLoadData();
        JsonUtility.FromJsonOverwrite(json,PlayerData);
    }
    private void PlayerSaveData(string json)
    {
        string DirecPath = Application.dataPath + "/SaveJsonData/PlayerSaveData";
        string path = DirecPath + "/PlayerSaveJson.json";

        if(Directory.Exists(DirecPath))
        {
            if(File.Exists(path))
            {
                Debug.Log("*--* Save 'PLAYER' Json *--*");
                FileStream stream = new FileStream(path,FileMode.Create);

                StreamWriter writer = new StreamWriter(stream);
                writer.Write(json);
                writer.Close();
                stream.Close();
            }
            else
            {
                Debug.LogError("*--* Save 'PLAYER' File Json Not Found *--*");
                Debug.Log("*--* Create New Save 'PLAYER' Json *--*");
                FileStream stream = new FileStream(path,FileMode.Create);

                stream.Close();
                PlayerSaveJson();
            }
        }
        else
        {
            Debug.LogError("*--* Directory 'PlayerSaveData' Not Found *--*");
            Debug.Log("*--* Create New 'PlayerSaveData' Directory *--*");
            Directory.CreateDirectory(DirecPath);
            FileStream stream = new FileStream(path,FileMode.Create);
            stream.Close();
            PlayerSaveJson();
        }
    }
    private string PlayerLoadData()
    {
        string DirecPath = Application.dataPath + "/SaveJsonData/PlayerSaveData";
        string path = DirecPath + "/PlayerSaveJson.json";

        if(Directory.Exists(DirecPath))
        {
            if(File.Exists(path))
            {
                Debug.Log("*--* Load 'PLAYER' Json *--*");
                StreamReader reader = new StreamReader(path);
                string json = reader.ReadToEnd();
                return json;
            }
            else
            {
                Debug.LogError("*--* Save 'PLAYER' File Json Not Found");
                Debug.Log("*--* Create New Save 'PLAYER' Json *--*");
                FileStream stream = new FileStream(path,FileMode.Create);
                stream.Close();
                PlayerSaveJson();
                return null;
            }
        }
        else
        {
            Debug.LogError("*--* Directory 'PlayerSaveData' Not Found *--*");
            Debug.Log("*--* Create New 'PlayerSaveData' Directory *--*");
            Directory.CreateDirectory(DirecPath);
            FileStream stream = new FileStream(path,FileMode.Create);
            stream.Close();
            PlayerSaveJson();
            return null;
        }
    }
    #endregion

}