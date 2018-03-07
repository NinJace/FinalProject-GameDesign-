using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class XMLTest : MonoBehaviour
{
    void Awake()
    {
        makeFile();
        readFile();
    }

    void makeFile()
    {
        List<Vector3> blocks = new List<Vector3>();
        List<Vector3> solution = new List<Vector3>();

        for (int x = 0; x < 2; x++)
        {
            for (int y = 0; y < 2; y++)
            {
                for (int z = 0; z < 2; z++)
                {
                    blocks.Add(new Vector3(x, y, z));
                }
            }
        }

        for (int y = 0; y < 2; y++)
        {
            for (int x = y; x < y + 2; x++)
            {
                for (int z = y; z < y + 2; z++)
                {
                    solution.Add(new Vector3(x, y, z));
                }
            }
        }

        PuzzleData tutorial = new PuzzleData();
        tutorial.start = blocks;
        tutorial.solution = solution;

        XmlSerializer serializer = new XmlSerializer(typeof(PuzzleData));
        FileStream stream = new FileStream(Application.dataPath + "/PuzzleData/XML/tutorial.xml", FileMode.Create);
        serializer.Serialize(stream, tutorial);
        stream.Close();
    }

    void readFile()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(PuzzleData));
        FileStream stream = new FileStream(Application.dataPath + "/PuzzleData/XML/tutorial.xml", FileMode.Open);
        PuzzleData tutorial = serializer.Deserialize(stream) as PuzzleData;
        stream.Close();
        Debug.Log(tutorial.start[0]);
        Debug.Log(tutorial.solution[0]);
    }

    
}

[System.Serializable] public class PuzzleData{ 
    public List<Vector3> start;
    public List<Vector3> solution;
}
