using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;
using System.Xml.Serialization;


public class DialogueManagerR : MonoBehaviour {
    public GameObject textBox;
    public Text theText;
    public int currentLine;
    public int endAtLine;
    public string fileName;
    public List<string> textLines;
    public float delay = 0.1f;
    private string currentText;

    void Awake()
    {
        Load();
    }

    // Use this for initialization
    void Start()
    {
        StartCoroutine(ShowText());
        

    }

    IEnumerator ShowText()
    {
        for(int i = 0; i < textLines[currentLine].Length + 1; i++)
        {
            currentText = textLines[currentLine].Substring(0, i);
            theText.text = currentText;
            yield return new WaitForSeconds(delay);
        }
        
    }
    
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space) & currentLine < textLines.Count - 1)
        {
            currentLine += 1;
            StopAllCoroutines();
            StartCoroutine(ShowText());
        }

        if (Input.GetKeyDown(KeyCode.Space) & currentLine == textLines.Count - 1)
        {
            StopAllCoroutines();
            theText.enabled = false;
            Destroy(textBox);
        }
    }

    void Load() {

        XmlSerializer serializer = new XmlSerializer(typeof(List<string>));
//#if UNITY_EDITOR
//        FileStream stream = new FileStream(Application.dataPath + "/Resources/StreamingAssets/XML/Demo/tutorial.xml", FileMode.Open);
//        textLines = serializer.Deserialize(stream) as List<string>;
//        stream.Close();
//#else
        XmlDocument xmlDoc = new XmlDocument();
        TextAsset textAsset = (TextAsset)Resources.Load("StreamingAssets/XML/Demo/tutorial");
        xmlDoc.LoadXml(textAsset.text);
        XmlNodeList nodeList = xmlDoc.GetElementsByTagName("ArrayOfString");
        textLines = serializer.Deserialize(new XmlNodeReader(nodeList.Item(0))) as List<string>;
//#endif

    }
}
