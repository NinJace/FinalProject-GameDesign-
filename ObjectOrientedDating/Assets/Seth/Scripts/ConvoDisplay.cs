using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ConvoDisplay : MonoBehaviour {
    /* While it's never good to make so many members public, these must be referenced this way.
     * If they are only referenced via a "GameObject.Find()" method, they will not be able to
     * be found again once their SetAcitve() has been set to false.
     */

    string dictInput;                               // Input this key into the dictionary to get the next List<string>

    public Text dialogText;                         // The text field for dialog
    public Text optionDialogText;                   // The text field for dialog when options are displayed
    public GameObject endText;                      // The text that displays when the conversation has ended. Like, literally "End"

    public GameObject dialogBackground;             // The image that serves as the background for the dialog text
    public GameObject optionDailogBackground;       // The image that serves as the background for the option dialog text and buttons



    // The text component of each button, respectively
    public Text button1;                         
    public Text button2;                          
    public Text button3;                        
    public Text button4;

    // The influence code for options/buttons
    influenceCode button1Influence = influenceCode.None;
    influenceCode button2Influence = influenceCode.None;
    influenceCode button3Influence = influenceCode.None;
    influenceCode button4Influence = influenceCode.None;

    // List for influence codes
    List<influenceCode> influenceList = new List<influenceCode>();

    // The image/sprite for each character, respectively
    public GameObject amini;           
    public GameObject arun;         
    public GameObject conrad;  
    public GameObject danny; 
    public GameObject jace;
    public GameObject ryn;
    public GameObject seth;
    public GameObject player;
    public GameObject narrator;

    GameObject currentSpeaker;

    public float delay = .01f;                             // The delay for the text typing speed
    

    dialogCode current;                             // The current dialog code, or the current mode of the dialog, or what's being displayed in the UI

    /* Originally, a reference to ins (the static member of XMLManager) was here,
     * but it turns out that reference this object in such a way loses some of its
     * dynamic properties. To put it simply, there would be a lot of null reference
     * errors if you tried to reference that object in this script. Instead, use
     * "XMLManager.ins.convoDB".
      */
    
    GameObject[] speakers;                          // An array to contain all the speaker images/sprites and set them all to inactive
    GameObject[] background;                       // An array to contain all the backgrounds and set them all to inactive

    string Name;
    int Influence;

    // Use this for initialization
    void Start() {

        //#if UNITY_EDITOR
        //        Name = "Josh";
        //        Influence = 0;
        //#else
        Name = GameObject.FindGameObjectWithTag("Data").GetComponent<DataScript>().data.playerName;
        Influence = 0;
        
//#endif


        Cursor.visible = true;

        // Populate the tag arrays
        speakers = GameObject.FindGameObjectsWithTag("Speaker");
        background = GameObject.FindGameObjectsWithTag("Background");

        // Hide all the speakers
        foreach (GameObject item in speakers)
        {
            item.SetActive(false);
        }

        // Hide all the backgrounds
        foreach (GameObject item in background)
        {
            item.SetActive(false);
        }

        // Add all the influence codes to list and set them to "None"
        influenceList.Add(button1Influence);
        influenceList.Add(button2Influence);
        influenceList.Add(button3Influence);
        influenceList.Add(button4Influence);
        
        // This will be the ignition key of the database to this conversation, or the first key to input into the database
        dictInput = "Start";

        // This calls the parser for the first time in every conversation
        Parser(XMLManager.ins.convoDB.dictionary[dictInput][0]);
    }

    // Update is called once per frame
    void Update() {
        // Check for the continuation key
        if (Input.GetKeyDown(KeyCode.Space) && current == dialogCode.Dialog)
        {
            StopAllCoroutines();
            try { Parser(XMLManager.ins.convoDB.dictionary[dictInput][0]); }
            catch (Exception) {  }
            //if (XMLManager.ins.convoDB.dictionary[dictInput][0] != "[EMPTY]")
            //{
            //    Parser(XMLManager.ins.convoDB.dictionary[dictInput][0]);
            //}
        }
        // Debug.Log("Influence: " + Influence);
    }

    // This really does everything: parses, displays, etc. 
    void Parser(string dialog)
    {
        string usable;
        string withoutSpeaker;                                                  // The string without the speaker command code
        string unformatted;                                                     // The string without any formatting

        // See who's speakig, display them
        switch (GetSpeakerCode(dialog))
        {
            case dialogCode.Amini:
                currentSpeaker = amini;
                DisplaySpeaker(dialogCode.Amini);
                break;
            case dialogCode.Arun:
                currentSpeaker = arun;
                DisplaySpeaker(dialogCode.Arun);
                break;
            case dialogCode.Conrad:
                currentSpeaker = conrad;
                DisplaySpeaker(dialogCode.Conrad);
                break;
            case dialogCode.Danny:
                currentSpeaker = danny;
                DisplaySpeaker(dialogCode.Danny);
                break;
            case dialogCode.Jace:
                currentSpeaker = jace;
                DisplaySpeaker(dialogCode.Jace);
                break;
            case dialogCode.Ryn:
                currentSpeaker = ryn;
                DisplaySpeaker(dialogCode.Ryn);
                break;
            case dialogCode.Seth:
                currentSpeaker = seth;
                DisplaySpeaker(dialogCode.Seth);
                break;
            case dialogCode.Player:
                DisplaySpeaker(dialogCode.Player);
                break;
            case dialogCode.Narrator:
                DisplaySpeaker(dialogCode.Narrator);
                break;
            case dialogCode.Malformation:
                Debug.Log("Malformation in the GetSpeaker Function");
                break;
        }

        usable = ReplaceName(dialog);
        unformatted = RemoveFormatting(dialog);                                         // Assign unformatted its value
        withoutSpeaker = RemoveSpeaker(dialog);                                         // Assign withoutSpeaker its value

        // Decide what background to show and how to display the dialog/options
        switch (GetCommandCode(withoutSpeaker))
        {
            case dialogCode.Dialog:
                dictInput = unformatted;
                DisplayBackground(dialogCode.Dialog);
                if (currentSpeaker != null)
                {
                    currentSpeaker.GetComponent<Animator>().enabled = true;
                }
                StartCoroutine(ShowDialog(usable));
                // Debug.Log("keyNext =" + unformatted);
                break;
            case dialogCode.Option:
                DisplayBackground(dialogCode.Option);
                
                if (currentSpeaker != null)
                {
                    currentSpeaker.GetComponent<Animator>().enabled = true;
                }
                StartCoroutine(ShowOptionDialog(usable));
                try { button1.text = InfluenceReader(ref button1Influence, XMLManager.ins.convoDB.dictionary[dictInput][1]); }
                catch (Exception) { button1.text = ""; }
                try { button2.text = InfluenceReader(ref button1Influence, XMLManager.ins.convoDB.dictionary[dictInput][2]); }
                catch (Exception) { button2.text = ""; }
                try { button3.text = InfluenceReader(ref button1Influence, XMLManager.ins.convoDB.dictionary[dictInput][3]); }
                catch (Exception) { button3.text = ""; }
                try { button4.text = InfluenceReader(ref button1Influence, XMLManager.ins.convoDB.dictionary[dictInput][4]); }
                catch (Exception) { button4.text = ""; }
                //if (XMLManager.ins.convoDB.dictionary[dictInput][1] != "[EMPTY]")
                //    button1.text = InfluenceReader(ref button1Influence, XMLManager.ins.convoDB.dictionary[dictInput][1]);
                //else button1.text = "";
                //if (XMLManager.ins.convoDB.dictionary[dictInput][2] != "[EMPTY]")
                //    button2.text = InfluenceReader(ref button1Influence, XMLManager.ins.convoDB.dictionary[dictInput][2]);
                //else button1.text = "";
                //if (XMLManager.ins.convoDB.dictionary[dictInput][3] != "[EMPTY]")
                //    button3.text = InfluenceReader(ref button1Influence, XMLManager.ins.convoDB.dictionary[dictInput][3]);
                //else button1.text = "";
                //if (XMLManager.ins.convoDB.dictionary[dictInput][4] != "[EMPTY]")
                //    button4.text = InfluenceReader(ref button1Influence, XMLManager.ins.convoDB.dictionary[dictInput][4]);
                //else button1.text = "";

                //button2.text = InfluenceReader(ref button2Influence, XMLManager.ins.convoDB.dictionary[dictInput][2]);
                //button3.text = InfluenceReader(ref button3Influence, XMLManager.ins.convoDB.dictionary[dictInput][3]);
                //button4.text = InfluenceReader(ref button4Influence, XMLManager.ins.convoDB.dictionary[dictInput][4]);
                break;
            case dialogCode.End:
                foreach (GameObject speaker in speakers)
                {
                    speaker.SetActive(false);
                }
                DisplayBackground(dialogCode.End);
                StartCoroutine("DramaticPause");
                break;
            case dialogCode.Scene:
                SceneManager.LoadScene(unformatted);
                break;
            case dialogCode.Malformation:
                Debug.Log("Malformation in the GetCommandCode Function");
                break;
        }



    }

    public void Button1()
    {
        try
        {
            dictInput = XMLManager.ins.convoDB.dictionary[dictInput][1];
            Parser(XMLManager.ins.convoDB.dictionary[dictInput][0]);
            switch (button1Influence)
            {
                case influenceCode.Positive:
                    GameObject.FindGameObjectWithTag("Data").GetComponent<DataScript>().data.rynRelation++;
                    break;
                case influenceCode.Negative:
                    Influence--;
                    break;
                case influenceCode.None:
                    break;
            }
        } catch (Exception) { }
        //if (XMLManager.ins.convoDB.dictionary[dictInput][1] != "[EMPTY]")
        //{
        //    dictInput = XMLManager.ins.convoDB.dictionary[dictInput][1];
        //    Parser(XMLManager.ins.convoDB.dictionary[dictInput][0]);
        //    switch (button1Influence)
        //    {
        //        case influenceCode.Positive:
        //            Influence++;
        //            break;
        //        case influenceCode.Negative:
        //            Influence--;
        //            break;
        //        case influenceCode.None:
        //            break;
        //    }
        //}
        
    }

    public void Button2()
    {
        try
        {
            dictInput = XMLManager.ins.convoDB.dictionary[dictInput][2];
            Parser(XMLManager.ins.convoDB.dictionary[dictInput][0]);
            switch (button1Influence)
            {
                case influenceCode.Positive:
                    GameObject.FindGameObjectWithTag("Data").GetComponent<DataScript>().data.rynRelation++;
                    break;
                case influenceCode.Negative:
                    Influence--;
                    break;
                case influenceCode.None:
                    break;
            }
        }
        catch (Exception) { }

        //if (XMLManager.ins.convoDB.dictionary[dictInput][2] != "[EMPTY]")
        //{
        //    dictInput = XMLManager.ins.convoDB.dictionary[dictInput][2];
        //    Parser(XMLManager.ins.convoDB.dictionary[dictInput][0]);
        //    switch (button1Influence)
        //    {
        //        case influenceCode.Positive:
        //            Influence++;
        //            break;
        //        case influenceCode.Negative:
        //            Influence--;
        //            break;
        //        case influenceCode.None:
        //            break;
        //    }
        //}

        //dictInput = XMLManager.ins.convoDB.dictionary[dictInput][2];
        //Parser(XMLManager.ins.convoDB.dictionary[dictInput][0]);

        //switch (button2Influence)
        //{
        //    case influenceCode.Positive:
        //        Influence++;
        //        break;
        //    case influenceCode.Negative:
        //        Influence--;
        //        break;
        //    case influenceCode.None:
        //        break;
        //}
    }

    public void Button3()
    {
        try
        {
            dictInput = XMLManager.ins.convoDB.dictionary[dictInput][3];
            Parser(XMLManager.ins.convoDB.dictionary[dictInput][0]);
            switch (button1Influence)
            {
                case influenceCode.Positive:
                    GameObject.FindGameObjectWithTag("Data").GetComponent<DataScript>().data.rynRelation++;
                    break;
                case influenceCode.Negative:
                    Influence--;
                    break;
                case influenceCode.None:
                    break;
            }
        }
        catch (Exception) { }

        //if (XMLManager.ins.convoDB.dictionary[dictInput][3] != "[EMPTY]")
        //{
        //    dictInput = XMLManager.ins.convoDB.dictionary[dictInput][3];
        //    Parser(XMLManager.ins.convoDB.dictionary[dictInput][0]);
        //    switch (button1Influence)
        //    {
        //        case influenceCode.Positive:
        //            Influence++;
        //            break;
        //        case influenceCode.Negative:
        //            Influence--;
        //            break;
        //        case influenceCode.None:
        //            break;
        //    }
        //}

        //dictInput = XMLManager.ins.convoDB.dictionary[dictInput][3];
        //Parser(XMLManager.ins.convoDB.dictionary[dictInput][0]);

        //switch (button3Influence)
        //{
        //    case influenceCode.Positive:
        //        Influence++;
        //        break;
        //    case influenceCode.Negative:
        //        Influence--;
        //        break;
        //    case influenceCode.None:
        //        break;
        //}
    }

    public void Button4()
    {
        try
        {
            dictInput = XMLManager.ins.convoDB.dictionary[dictInput][4];
            Parser(XMLManager.ins.convoDB.dictionary[dictInput][0]);
            switch (button1Influence)
            {
                case influenceCode.Positive:
                    GameObject.FindGameObjectWithTag("Data").GetComponent<DataScript>().data.rynRelation++;
                    break;
                case influenceCode.Negative:
                    Influence--;
                    break;
                case influenceCode.None:
                    break;
            }
        }
        catch (Exception) { }

        //if (XMLManager.ins.convoDB.dictionary[dictInput][4] != "[EMPTY]")
        //{
        //    dictInput = XMLManager.ins.convoDB.dictionary[dictInput][4];
        //    Parser(XMLManager.ins.convoDB.dictionary[dictInput][0]);
        //    switch (button1Influence)
        //    {
        //        case influenceCode.Positive:
        //            Influence++;
        //            break;
        //        case influenceCode.Negative:
        //            Influence--;
        //            break;
        //        case influenceCode.None:
        //            break;
        //    }
        //}

        //dictInput = XMLManager.ins.convoDB.dictionary[dictInput][4];
        //Parser(XMLManager.ins.convoDB.dictionary[dictInput][0]);

        //switch (button4Influence)
        //{
        //    case influenceCode.Positive:
        //        Influence++;
        //        break;
        //    case influenceCode.Negative:
        //        Influence--;
        //        break;
        //    case influenceCode.None:
        //        break;
        //}
    }

    void DisplayBackground(dialogCode backgroundCode)
    {
        //if (backgroundCode != dialogCode.End)
        //{
        //    foreach (GameObject item in backgrounds)
        //    {
        //        item.SetActive(false);
        //    }
        //}

        foreach (GameObject item in background)
        {
            item.SetActive(false);
        }

        switch (backgroundCode)
        {
            case dialogCode.Dialog:
                dialogBackground.SetActive(true);
                break;
            case dialogCode.Option:
                optionDailogBackground.SetActive(true);
                break;
            case dialogCode.End:
                endText.SetActive(true);
                break;
        }
    }

    void DisplaySpeaker(dialogCode speaker)
    {
        foreach(GameObject item in speakers) {
            item.SetActive(false);
        }

        switch (speaker)
        {
            case dialogCode.Amini:
                amini.SetActive(true);
                break;
            case dialogCode.Arun:
                arun.SetActive(true);
                break;
            case dialogCode.Conrad:
                conrad.SetActive(true);
                break;
            case dialogCode.Danny:
                danny.SetActive(true);
                break;
            case dialogCode.Jace:
                jace.SetActive(true);
                break;
            case dialogCode.Ryn:
                ryn.SetActive(true);
                break;
            case dialogCode.Seth:
                seth.SetActive(true);
                break;
            case dialogCode.Player:
                player.SetActive(true);
                break;
            case dialogCode.Narrator:
                narrator.SetActive(true);
                break;

        }
    }

    string InfluenceReader(ref influenceCode code, string input)
    {
        string output = "";
        char[] tempArray = input.ToCharArray();
        List<char> tempList = new List<char>();
        foreach (char character in tempArray)
        {
            tempList.Add(character);
        }

        foreach (char character in tempList)
        {
            if (character == '[')
                continue;
            else if (character == ']')
                break;
            else
                output = output + character;
        }

        switch (output)
        {
            case "Positive":
                code = influenceCode.Positive;
                break;
            case "Negative":
                code = influenceCode.Negative;
                break;
            case "None":
                code = influenceCode.None;
                break;
            default:
                code = influenceCode.None;
                break;
        }


        output = "";
        bool add = false;

        foreach (char character in tempList)
        {
            if (character == '[')
            {
                add = false;
                continue;
            }
            else if (character == ']')
            {
                add = true;
                continue;
            }
            else if (add)
            {
                output = output + character;
            }
            else continue;
        }

        return output;

    }

    //influenceCode GetInfluenceCode(string input)
    //{
    //    string output = "";
    //    char[] tempArray = input.ToCharArray();
    //    List<char> tempList = new List<char>();
    //    foreach (char character in tempArray)
    //    {
    //        tempList.Add(character);
    //    }

    //    foreach (char character in tempList)
    //    {
    //        if (character == '[')
    //            continue;
    //        else if (character == ']')
    //            break;
    //        else
    //            output = output + character;
    //    }

    //    switch (output)
    //    {
    //        case "Positive":
    //            return influenceCode.Positive;
    //        case "Negative":
    //            return influenceCode.Negative;
    //        case "None":
    //            return influenceCode.None;
    //    }


    //    //output = "";
    //    //bool add = false;

    //    //foreach (char character in tempList)
    //    //{
    //    //    if (character == '[')
    //    //    {
    //    //        add = false;
    //    //        continue;
    //    //    }
    //    //    else if (character == ']')
    //    //    {
    //    //        add = true;
    //    //        continue;
    //    //    }
    //    //    else if (add)
    //    //    {
    //    //        output = output + character;
    //    //        // usable.Add(character);
    //    //    }
    //    //    else continue;
    //    //}

    //    return influenceCode.None;

    //}

    string RemoveSpeaker(string input)
    {
        string output = "";
        bool removed = false;
        char[] tempArray = input.ToCharArray();
        List<char> original = new List<char>();

        foreach (char character in tempArray)
        {
            original.Add(character);
        }
        

        foreach (char character in original)
        {
            if (!removed)
            {
                if (character == '[')
                {
                    continue;
                }
                else if (character == ']')
                {
                    removed = true;
                    continue;
                }
                else
                    continue;
            }
            

            output = output + character;

        }

        

        return output;
    }

    string ReplaceName(string input)
    {
        string output = "";
        string content = "";
        bool add = true;
        char[] tempArray = input.ToCharArray();
        List<char> list = new List<char>();

        foreach (char character in tempArray)
        {
            list.Add(character);
        }

        foreach (char character in list)
        {
            if (add == true)
            {
                if (character == '[')
                {
                    add = false;
                    continue;
                }
                else output += character;
            } else
            {
                if (character == ']')
                {
                    if (content == "NAME")
                    {
                        output += Name;
                        content = "";
                    }
                    else content = "";
                    add = true;
                    continue;
                }
                else content += character;
            }
        }

        return output;


    }

    string RemoveFormatting(string input)
    {
        string output = "";
        string content = "";
        bool add = true;
        char[] tempArray = input.ToCharArray();
        List<char> list = new List<char>();

        foreach (char character in tempArray)
        {
            list.Add(character);
        }

        foreach (char character in list)
        {
            if (add == true)
            {
                if (character == '[')
                {
                    add = false;
                    continue;
                }
                else output += character;
            }
            else
            {
                if (character == ']')
                {
                    if (content == "NAME")
                    {
                        output += "[NAME]";
                        content = "";
                    }
                    else content = "";
                    add = true;
                    continue;
                }
                else content += character;
            }
        }

        //foreach (char character in original)
        //{
        //    if (character == '[')
        //    {
        //        add = false;
        //        continue;
        //    }
        //    else if (character == ']')
        //    {
        //        add = true;
        //        continue;
        //    }
        //    else if (add)
        //    {
        //        output = output + character;
        //    }
        //    else continue;

        //}

        return output;
    }

    dialogCode GetCommandCode(string input)
    {
        string output = "";
        char[] tempArray = input.ToCharArray();
        List<char> workingList = new List<char>();

        foreach (char character in tempArray)
        {
            workingList.Add(character);
        }
        

        foreach (char character in workingList)
        {
            if (character == '[')
            {
                continue;
            }
            else if (character == ']')
            {
                break;
            }
            else
                output = output + character;
        }
        

        switch (output)
        {
            case "Dialog":
                current = dialogCode.Dialog;
                return dialogCode.Dialog;
            case "Option":
                current = dialogCode.Option;
                return dialogCode.Option;
            case "End":
                current = dialogCode.End;
                string location = GameObject.FindGameObjectWithTag("Data").GetComponent<DataScript>().data.location;
                if(location == "dorm")
                {
                    SceneManager.LoadScene("Dorm");
                }
                if(location == "oliver")
                {
                    SceneManager.LoadScene("Oliver");
                }
                return dialogCode.End;
            case "Scene":
                current = dialogCode.Scene;
                return dialogCode.Scene;
        }

        return dialogCode.Malformation;
    }

    dialogCode GetSpeakerCode(string input)
    {
        string output = "";
        char[] tempArray = input.ToCharArray();
        List<char> workingList = new List<char>();

        foreach (char character in tempArray)
        {
            workingList.Add(character);
        }

        List<char> tempList = new List<char>();

        foreach (char character in workingList)
        {
            if (character == '[')
            {
                continue;
            }
            else if (character == ']')
            {
                break;
            }
            else tempList.Add(character);
        }

        foreach (char character in tempList)
        {
            output = output + character;
        }
        

        switch (output)
        {
            case "Character":
                return dialogCode.Character;
            case "Amini":
                return dialogCode.Amini;
            case "Arun":
                return dialogCode.Arun;
            case "Conrad":
                return dialogCode.Conrad;
            case "Danny":
                return dialogCode.Danny;
            case "Jace":
                return dialogCode.Jace;
            case "Ryn":
                return dialogCode.Ryn;
            case "Seth":
                return dialogCode.Seth;
            case "Player":
                return dialogCode.Player;
            case "Narrator":
                return dialogCode.Narrator;
        }

        return dialogCode.Malformation;

    }

    IEnumerator ShowDialog(string dialog)
    {
        if (dialog.Length % 2 ==0 )
        {
            string currentText;
            for (int i = 0; i < dialog.Length + 1; i++)
            {
                currentText = dialog.Substring(0, i);
                dialogText.GetComponent<Text>().text = currentText;

                // Animator controller here
                if (i == dialog.Length && currentSpeaker != null)
                {
                    currentSpeaker.GetComponent<Animator>().enabled = false;
                }

                yield return new WaitForSeconds(delay);
            }
        } else
        {
            dialog = dialog + "";
            string currentText;
            for (int i = 0; i < dialog.Length + 1; i++)
            {
                currentText = dialog.Substring(0, i);
                dialogText.GetComponent<Text>().text = currentText;

                // Animator controller here
                if (i == dialog.Length && currentSpeaker != null)
                {
                    currentSpeaker.GetComponent<Animator>().enabled = false;
                }

                yield return new WaitForSeconds(delay);
            }
        }
    }

    IEnumerator ShowOptionDialog(string dialog)
    {
        string currentText;
        for (int i = 0; i < dialog.Length + 1; i++)
        {
            currentText = dialog.Substring(0, i);
            optionDialogText.GetComponent<Text>().text = currentText;

            if (i == dialog.Length && currentSpeaker != null)
            {
                currentSpeaker.GetComponent<Animator>().enabled = false;
            }

            yield return new WaitForSeconds(delay);
        }

    }

    IEnumerator DramaticPause()
    {
        int count = 0;
        while (count != 20)
        {
            count++;
            yield return new WaitForSeconds(delay);
        }
        Application.Quit();
    }

    enum influenceCode {
        Positive,
        Negative,
        None
    }

    enum dialogCode
    {
        Character,
        Amini,
        Arun,
        Conrad,
        Danny,
        Jace,
        Ryn,
        Seth,
        Player,
        Narrator,
        Malformation,
        Dialog,
        Option,
        End,
        Scene
    }

}


