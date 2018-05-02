using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayConvo : MonoBehaviour {
    Text playerDialog;
    Text playerIndex;
    Text dictKey;
    Text dictValueIndex;
    Text dictValueDialog;
    InputField playerDailogInput;
    InputField playerIndexInput;
    InputField dictKeyInput;
    InputField dictValueIndexInput;
    InputField dictDailogInput;
    

    private void Awake()
    {
        playerDialog = GameObject.Find("Player Dialog Input Text").GetComponent<Text>();
        playerIndex = GameObject.Find("Player Index Input Text").GetComponent<Text>();
        dictKey = GameObject.Find("Dictionary Key Input Text").GetComponent<Text>();
        dictValueIndex = GameObject.Find("Dictionary Value Index Input Text").GetComponent<Text>();
        dictValueDialog = GameObject.Find("Dictionary Dialog Input Text").GetComponent<Text>();

        playerDailogInput = GameObject.Find("Player Dialog Input").GetComponent<InputField>();
        playerIndexInput = GameObject.Find("Player Index Input").GetComponent<InputField>();
        dictKeyInput = GameObject.Find("Dictionary Key Input").GetComponent<InputField>();
        dictValueIndexInput = GameObject.Find("Dictionary Value Index Input").GetComponent <InputField>();
        dictDailogInput = GameObject.Find("Dictionary Dialog Input").GetComponent<InputField>();
    }

    public void Start()
    {
       
    }

    //public void AddPlayer()
    //{
    //    XMLManager.ins.convoDB.player.Add(playerDialog.text.ToString());
    //    playerDailogInput.text = "";
    //}

    //public void ReplacePlayer()
    //{
    //    if (int.Parse(playerIndex.text.ToString()) <= XMLManager.ins.convoDB.player.Count - 1)
    //    {
    //        XMLManager.ins.convoDB.player[int.Parse(playerIndex.text.ToString())] = playerDialog.text.ToString();
    //        playerDailogInput.text = "";
    //        playerIndexInput.text = "";
    //    }
    //}

    public void AddDialogList()
    {
        List<string> list = new List<string>();
        list.Add(dictValueDialog.text.ToString());
        XMLManager.ins.convoDB.dictionary.Add(dictKey.text.ToString(), list);
    }

    public void AddDialog()
    {
        XMLManager.ins.convoDB.dictionary[dictKey.text.ToString()].Add(dictValueDialog.text.ToString());
    }

    public void ReplaceDialog()
    {
        if (XMLManager.ins.convoDB.dictionary.ContainsKey(dictKey.text.ToString()))
        {
            List<string> list = new List<string>();
            list.Add(dictValueDialog.text.ToString());
            XMLManager.ins.convoDB.dictionary[dictKey.text.ToString()] = list;
        }
    }

    public void ReplaceDialogAtIndex()
    {
        if (XMLManager.ins.convoDB.dictionary.ContainsKey(dictKey.text.ToString()) && int.Parse(dictValueIndex.text.ToString()) < XMLManager.ins.convoDB.dictionary[dictKey.text.ToString()].Count - 1)
        {
            XMLManager.ins.convoDB.dictionary[dictKey.text.ToString()][int.Parse(dictValueIndex.text.ToString())] = dictValueDialog.text.ToString();
        }
    }

}

    
