using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine.SceneManagement;


public class DialogueManagerT : MonoBehaviour {
    public InputField UInput;
    public GameObject textBox;
    public Text theText;
    public int currentLine;
    public int endAtLine;
    public string fileName;
    public List<string> textLines;
    public Animator speaker;
    public float delay = 0.1f;
    private string currentText;
    private string Name;
    public Button beginTest;

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
                Name = UInput.text;
             
                
            }
            yield return new WaitForSeconds(delay);
            
        }
    }
    public void BPress()
    {
        GameObject.FindGameObjectWithTag("Data").GetComponent<DataScript>().data.playerName = UInput.textComponent.text;
        Debug.Log("Saved name: "+GameObject.FindGameObjectWithTag("Data").GetComponent<DataScript>().data.playerName);
        GameObject.FindGameObjectWithTag("Data").GetComponent<DataScript>().data.puzzleLevel = "tutorial";
        SceneManager.LoadScene("TutorialDemo");
    }
    
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space) & currentLine < textLines.Count - 1)
        {
            currentLine += 1;
            //speaker.enabled = true;
            StartCoroutine(ShowText());
        }
       

    }

    void Load() {
        XmlSerializer serializer = new XmlSerializer(typeof(List<string>));
#if UNITY_EDITOR
        FileStream stream = new FileStream(Application.dataPath + "/Resources/StreamingAssets/XML/Demo/GRE_Dial.xml", FileMode.Open);
        textLines = serializer.Deserialize(stream) as List<string>;
        stream.Close();
#else
        XmlDocument xmlDoc = new XmlDocument(); // xmlDoc is the new xml document.
        TextAsset textAsset = (TextAsset)Resources.Load("StreamingAssets/XML/Demo/GRE_Dial", typeof(TextAsset));
        xmlDoc.LoadXml(textAsset.text);
        XmlNodeList nodeList = xmlDoc.GetElementsByTagName("ArrayOfString");
        textLines = serializer.Deserialize(new XmlNodeReader(nodeList.Item(0))) as List<string>;
#endif
    }
}
