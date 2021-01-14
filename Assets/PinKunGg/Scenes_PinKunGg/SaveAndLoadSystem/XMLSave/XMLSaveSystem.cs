using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class XMLSaveSystem : MonoBehaviour
{
    public static XMLSaveSystem XMLSSinstanse;

    private void Awake()
    {
        XMLSSinstanse = this;    
    }
    public PlayerSaveData player;

    public void XMLSave()
    {
        string DirecPath = Application.dataPath + "/SaveXMLData/PlayerSaveData";
        string path = DirecPath + "/PlayerSaveXML.xml";

        XmlSerializer serializer = new XmlSerializer(typeof(PlayerSaveData));

        if(Directory.Exists(DirecPath))
        {
            if(File.Exists(path))
            {
                print("*--* Save XML *--*");
                FileStream stream = new FileStream(path,FileMode.Create);
                serializer.Serialize(stream,player);
                stream.Close();
            }
            else
            {
                print("*--* XML Save File Not Found *--*");
                print("*--* Create New XML Save File *--*");
                FileStream stream = new FileStream(path,FileMode.Create);
                stream.Close();
            }
        }
        else
        {
            Debug.LogError("*--* Directory not found *--*");
            Debug.Log("Create new directory and save");
            Directory.CreateDirectory(DirecPath);
            FileStream stream = new FileStream(path, FileMode.Create);
            stream.Close();
        }
        
        
    }
    public void XMLLoad()
    {
        string DirecPath = Application.dataPath + "/SaveXMLData/PlayerSaveData";
        string path = DirecPath + "/PlayerSaveXML.xml";

        XmlSerializer serializer = new XmlSerializer(typeof(PlayerSaveData));
        FileStream stream = new FileStream(path,FileMode.Open);

        player = serializer.Deserialize(stream) as PlayerSaveData;
        stream.Close();


    }
}
