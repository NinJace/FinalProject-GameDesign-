using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

//This script just defines a data type, it is not a game object
public class SaveData{

    public int arunRelation;
    public int aminiRelation;
    public int conradRelation;
    public int rynRelation;
    public int sethRelation;
    public int jaceRelation;
    public int dannyRelation;
    public string playerName;
    public int scene;
    public string puzzleLevel;
    public string location;
    public string person;
    public List<string> items;

    //new constructor
    public SaveData()
    {
        arunRelation = 0;
        aminiRelation = 0;
        conradRelation = 0;
        rynRelation = 0;
        sethRelation = 0;
        jaceRelation = 0;
        dannyRelation = 0;
        playerName = "";
        scene = 1;
        puzzleLevel = "tutorial";
        location = "dorm";
        person = "";
        items = new List<string>();
    }

    //writes current SaveData to xml
    public void save()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(SaveData));
        FileStream stream = new FileStream(Application.dataPath + "/Resources/save.xml", FileMode.Create);
        serializer.Serialize(stream, this);
        stream.Close();
    }

    //returns a SaveData object from the xml
    //will return an empty SaveData if no save file can be found
    //made static bc it makes sense
    public static SaveData Load()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(SaveData));
        XmlDocument xmlDoc = new XmlDocument();
        TextAsset textAsset = (TextAsset)Resources.Load("save");
        xmlDoc.LoadXml(textAsset.text);
        XmlNodeList nodeList = xmlDoc.GetElementsByTagName("SaveData");
        SaveData data = serializer.Deserialize(new XmlNodeReader(nodeList.Item(0))) as SaveData;
        return data;
    }
}
