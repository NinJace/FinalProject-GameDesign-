using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;
using System.Xml.Serialization;


public class DialogueManager : MonoBehaviour {
    public GameObject textBox;
    public Text theText;
    public int currentLine;
    public int endAtLine;
    public string fileName;
    public List<string> textLines;
    public Animator speaker;
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
        for(int i = 0; i < textLines[currentLine].Length+1; i++)
        {
            currentText = textLines[currentLine].Substring(0, i);
            theText.text = currentText;
            
            if(i == textLines[currentLine].Length)
            {
                speaker.enabled = false; 
            }
            yield return new WaitForSeconds(delay);
            
        }
    }
    
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space) & currentLine < textLines.Count - 1)
        {
            currentLine += 1;
            speaker.enabled = true;
            StartCoroutine(ShowText());
        }

    }

    public void Save() {

        XmlSerializer serializer = new XmlSerializer(typeof(List<string>));
        FileStream stream = new FileStream(Application.dataPath + "/StreamingAssets/XML/Demo/" + fileName + "_dialog_data.xml", FileMode.Create);
        serializer.Serialize(stream, textLines);
        stream.Close();
    }

    void Load() {

        XmlSerializer serializer = new XmlSerializer(typeof(List<string>));
        FileStream stream = new FileStream(Application.dataPath + "/StreamingAssets/XML/Demo/" + fileName + "_dialog_data.xml", FileMode.Open);
        textLines = serializer.Deserialize(stream) as List<string>;
        stream.Close();
    }
}
