
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class XMLManager : MonoBehaviour {

    public static XMLManager ins;

    public ConvoDatabase convoDB;
    
    public string scene;

    void Awake()
    {
        ins = this;

        scene = GameObject.FindGameObjectWithTag("Data").GetComponent<DataScript>().data.person + GameObject.FindGameObjectWithTag("Data").GetComponent<DataScript>().data.scene;
        Debug.Log("Reading file: "+scene);
        LoadConvos();
    }

    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
   
    }

    public void SaveConvos()
    {
        
        XmlSerializer serializer = new XmlSerializer(typeof(ConvoEntry));
        FileStream stream = new FileStream(Application.dataPath + "/StreamingAssets/XML/Dialoge/" + scene + "_dialog_data.xml", FileMode.Create);
        serializer.Serialize(stream, TranslateToEntry(convoDB));
        stream.Close();
    }
    
    public void LoadConvos()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(ConvoEntry));
//# if UNITY_EDITOR
//        FileStream stream = new FileStream(Application.dataPath + "/Resources/StreamingAssets/XML/Dialoge/" + scene + "_dialog_data.xml", FileMode.Open);
//        convoDB = TranslateToDatabase(serializer.Deserialize(stream) as ConvoEntry);
//        stream.Close();
//# else
        XmlDocument xmlDoc = new XmlDocument();
        TextAsset textAsset = (TextAsset)Resources.Load("StreamingAssets/XML/Dialoge/" + scene + "_dialog_data");
        xmlDoc.LoadXml(textAsset.text);
        XmlNodeList nodeList = xmlDoc.GetElementsByTagName("ConvoEntry");
        convoDB = TranslateToDatabase(serializer.Deserialize(new XmlNodeReader(nodeList.Item(0))) as ConvoEntry);
        

//# endif 




    }

    public ConvoEntry TranslateToEntry(ConvoDatabase database)
    {
        List<string> keys = new List<string>();
        List<List<string>> values = new List<List<string>>();
        foreach (KeyValuePair<string, List<string>> item in database.dictionary)
        {
            keys.Add(item.Key);
            values.Add(item.Value);
        }
        return new ConvoEntry(keys, values);
    }

    public ConvoDatabase TranslateToDatabase(ConvoEntry entry)
    {
        Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();
        //foreach (string key in entry.keys)
        //{
        //    foreach (List<string> value in entry.values)
        //    {
        //        dictionary.Add(key, value);
        //    }
        //}
        for (int i = 0; i < entry.keys.Count - 1; i++)
        {
            dictionary.Add(entry.keys[i], entry.values[i]);
        }
        return new ConvoDatabase(dictionary);
    }
}
[System.Serializable]
public class ConvoEntry
{
    public List<string> keys;
    public List<List<string>> values;

    public ConvoEntry() { }

    public ConvoEntry(List<string> keysIn, List<List<string>> valuesIn)
    {
        keys = keysIn;
        values = valuesIn;
    }
}

[System.Serializable]
public class ConvoDatabase
{
    public Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();

    //public ConvoDatabase() { }

    public ConvoDatabase(Dictionary<string, List<string>> dictionaryIn)
    {
        dictionary = dictionaryIn;
    }
}