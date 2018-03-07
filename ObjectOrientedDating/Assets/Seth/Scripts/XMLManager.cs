using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

public class XMLManager : MonoBehaviour {

    public static XMLManager ins;

    void Awake()
    {
        ins = this;
    }

    public ConvoDatabase convoDB;

    
}

[System.Serializable]
public class ConvoEntry
{
    public string optionA;

    public string optionB;

    public string optionC;
}

[System.Serializable]
public class ConvoDatabase
{
    public List<ConvoEntry> list = new List<ConvoEntry>();
}
