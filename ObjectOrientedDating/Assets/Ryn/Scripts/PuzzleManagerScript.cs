using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using UnityEngine.SceneManagement;

public class PuzzleManagerScript : MonoBehaviour {

    public GameObject block;
    public GameObject solutionImg;

    public Image win;

    public RawImage card;
    public RawImage coffee;
    public RawImage cactus;
    public RawImage gray;
    public RawImage textbook;
    public RawImage vr;

    public GameObject exitButton;

    public int moves;

    public string level;

    GameObject selected;

    List<GameObject> blocks;
    List<Vector3> start;
    List<Vector3> solution;
    
    void Awake()
    {
        moves = 0;
//#if UNITY_EDITOR
//        level = "puzzle9";
//#else
        level = GameObject.FindGameObjectWithTag("Data").GetComponent<DataScript>().data.puzzleLevel;
//#endif
        blocks = new List<GameObject>();
    }

    // Use this for initialization
    void Start () {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        resetPuzzle();
	}
	
	// Update is called once per frame
	void Update () {
        //input to move rows of blocks
        if (Input.GetKeyDown(KeyCode.Q))
        {
            moveX(1);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            moveX(-1);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            moveY(1);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            moveY(-1);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            moveZ(1);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            moveZ(-1);
        }
        //r to reset
        else if (Input.GetKeyDown(KeyCode.R))
        {
            resetPuzzle();
        }
        else if (Input.GetKeyDown(KeyCode.V))
        {
            exitButton.SetActive(true);
        }
    }

    //needs to be finished
    public void blockSelected(GameObject block)
    {
        if(selected != null)
        {
            selected.GetComponent<BlockScript>().toggleSelected();
        }
        //update selected
        selected = block;
        //enable selection feedback
        selected.GetComponent<BlockScript>().toggleSelected();
    }

    void moveX(int dir)
    {
        //move selected row
        foreach(GameObject block in blocks)
        {
            if(block.transform.position.y == selected.transform.position.y && block.transform.position.z == selected.transform.position.z)
            {
                block.transform.position += Vector3.right * dir;
            }
        }

        //undoes invalid move
        if (!isValid())
        {
            moves--;
            moveX(-dir);
        }
        else
        {
            check();
            moves++;
        }
    }

    void moveY(int dir)
    {
        foreach (GameObject block in blocks)
        {
            if (block.transform.position.x == selected.transform.position.x && block.transform.position.z == selected.transform.position.z)
            {
                block.transform.position += Vector3.up * dir;
            }
        }

        if (!isValid())
        {
            moves--;
            moveY(-dir);
        }
        else
        {
            check();
            moves++;

        }
    }

    void moveZ(int dir)
    {
        foreach (GameObject block in blocks)
        {
            if (block.transform.position.y == selected.transform.position.y && block.transform.position.x == selected.transform.position.x)
            {
                block.transform.position += Vector3.forward * dir;
            }
        }

        if (!isValid())
        {
            moves--;
            moveZ(-dir);
        }
        else
        {
            check();
            moves++;

        }
    }

    //true if there is a block at the given vector
    bool blockAt(Vector3 position)
    {
        foreach (GameObject block in blocks)
        {
            if (block.transform.position == position)
            {
                return true;
            }
        }
        return false;
    }

    //comapares current block standing with solution
    bool check()
    {
        List<Vector3> tempSolution = new List<Vector3>();
        int minX = int.MaxValue;
        int minY = int.MaxValue;
        int minZ = int.MaxValue;

        //calculate mins and make temporary list
        foreach (GameObject block in blocks)
        {
            if (block.transform.position.x < minX)
            {
                minX = (int)block.transform.position.x;
            }
            if (block.transform.position.y < minY)
            {
                minY = (int)block.transform.position.y;
            }
            if (block.transform.position.z < minZ)
            {
                minZ = (int)block.transform.position.z;
            }
            tempSolution.Add(new Vector3(block.transform.position.x, block.transform.position.y, block.transform.position.z));
        }

        //standardize list
        for (int i = 0; i < tempSolution.Count; i++)
        {
            Vector3 tempVec = tempSolution[i];
            tempVec.x -= minX;
            tempVec.y -= minY;
            tempVec.z -= minZ;
            tempSolution[i] = tempVec;
        }

        //check solution
        bool correct = true;
        foreach(Vector3 vec in tempSolution)
        {
            if (!solution.Contains(vec)){
                correct = false;
                //Debug.Log("Block at " + vec + " is out of place.");
                break;
            }
        }

        //Debug.Log("The solution is " + correct);
        win.enabled = correct;
        if (correct)
        {
            exitButton.SetActive(true);
        }
        return correct;
    }

    //returns if a move is valid by using BFS to find all the blocks
    //if not all blocks are found the move is invalid/returns false
    bool isValid()
    {
        List<Vector3> toDo = new List<Vector3>();
        List<Vector3> done = new List<Vector3>();
        toDo.Add(blocks[0].transform.position);

        while (toDo.Count > 0)
        {
            done.Add(toDo[0]);
            if (blockAt(toDo[0] + Vector3.up) && !toDo.Contains(toDo[0] + Vector3.up) && !done.Contains(toDo[0] + Vector3.up)) {
                toDo.Add(toDo[0] + Vector3.up);
            }
            if (blockAt(toDo[0] - Vector3.up) && !toDo.Contains(toDo[0] - Vector3.up) && !done.Contains(toDo[0] - Vector3.up))
            {
                toDo.Add(toDo[0] - Vector3.up);
            }
            if (blockAt(toDo[0] + Vector3.right) && !toDo.Contains(toDo[0] + Vector3.right) && !done.Contains(toDo[0] + Vector3.right))
            {
                toDo.Add(toDo[0] + Vector3.right);
            }
            if (blockAt(toDo[0] - Vector3.right) && !toDo.Contains(toDo[0] - Vector3.right) && !done.Contains(toDo[0] - Vector3.right))
            {
                toDo.Add(toDo[0] - Vector3.right);
            }
            if (blockAt(toDo[0] + Vector3.forward) && !toDo.Contains(toDo[0] + Vector3.forward) && !done.Contains(toDo[0] + Vector3.forward))
            {
                toDo.Add(toDo[0] + Vector3.forward);
            }
            if (blockAt(toDo[0] - Vector3.forward) && !toDo.Contains(toDo[0] - Vector3.forward) && !done.Contains(toDo[0] - Vector3.forward))
            {
                toDo.Add(toDo[0] - Vector3.forward);
            }
            toDo.RemoveAt(0);
        }

        return blocks.Count == done.Count;
    }

    //void moveCount()
    //{
    //    if (temp != blocks)
    //    {
    //        moves++;
    //    }
    //}

    //loads the puzzle in from the XML document
    //also load in the image that shows the solution
    void resetPuzzle()
    {
        Object tempImg = Resources.Load(level);
        Texture2D levelSolution = (Texture2D)tempImg;
        solutionImg.GetComponent<RawImage>().texture = levelSolution;

        XmlSerializer serializer = new XmlSerializer(typeof(PuzzleData));

#if UNITY_EDITOR
        FileStream stream = new FileStream(Application.dataPath + "/Resources/PuzzleData/" + level + ".xml", FileMode.Open);
        PuzzleData data = serializer.Deserialize(stream) as PuzzleData;
        stream.Close();
#else
        XmlDocument xmlDoc = new XmlDocument();
        TextAsset textAsset = (TextAsset)Resources.Load("PuzzleData/" + level);
        xmlDoc.LoadXml(textAsset.text);
        XmlNodeList nodeList = xmlDoc.GetElementsByTagName("PuzzleData");
        PuzzleData data = serializer.Deserialize(new XmlNodeReader(nodeList.Item(0))) as PuzzleData;
#endif

        start = data.start;
        solution = data.solution;
        
        foreach (GameObject block in blocks)
        {
            Destroy(block);
        }
        blocks.Clear();

        foreach(Vector3 vec in start)
        {
            blocks.Add(Instantiate(block, vec, Quaternion.identity, gameObject.transform));
        }
        moves = 0;
       
        check();
    }

    //saves current puzzle layout as a new xml doucument
    //esentially a level editor
    //does not make a corresponding solution image
    void savePuzzle()
    {
        PuzzleData data = new PuzzleData();
        data.start = start;
        data.solution = new List<Vector3>();
        foreach (GameObject block in blocks)
        {
            data.solution.Add(block.transform.position);
        }

        //stadardize solution
        int minX = int.MaxValue;
        int minY = int.MaxValue;
        int minZ = int.MaxValue;

        foreach (Vector3 vec in data.solution)
        {
            if (vec.x < minX)
            {
                minX = (int)vec.x;
            }
            if (vec.y < minY)
            {
                minY = (int)vec.y;
            }
            if (vec.z < minZ)
            {
                minZ = (int)vec.z;
            }
        }

        for (int i = 0; i < data.solution.Count; i++)
        {
            Vector3 tempVec = data.solution[i];
            tempVec.x -= minX;
            tempVec.y -= minY;
            tempVec.z -= minZ;
            data.solution[i] = tempVec;
        }

        //foreach(Vector3 vec in data.solution)
        //{
        //    Debug.Log(vec + " ");
        //}

        //store to xml
        XmlSerializer serializer = new XmlSerializer(typeof(PuzzleData));
        FileStream stream = new FileStream(Application.dataPath + "/Resources/PuzzleData/newPuzzle.xml", FileMode.Create);
        serializer.Serialize(stream, data);
        if (File.Exists(Application.dataPath + "/Resources/PuzzleData/newPuzzle.xml"))
        {
            Debug.Log("level saved");
        }
        stream.Close();

    }

    public void exit()
    {
        StartCoroutine("exitScene");
    }

    IEnumerator exitScene()
    {

        switch (level[level.Length - 1]){
            case '1':
                GameObject.FindGameObjectWithTag("Data").GetComponent<DataScript>().data.items.Add("touchController");
                vr.gameObject.SetActive(true);
                yield return new WaitForSeconds(1);
                break;
            case '6':
                GameObject.FindGameObjectWithTag("Data").GetComponent<DataScript>().data.items.Add("textbook");
                textbook.gameObject.SetActive(true);
                Debug.Log("textbook");
                yield return new WaitForSeconds(1);
                break;
            case '9':
                GameObject.FindGameObjectWithTag("Data").GetComponent<DataScript>().data.items.Add("gourmetCoffee");
                coffee.gameObject.SetActive(true);
                yield return new WaitForSeconds(1);
                break;
            case '3':
                GameObject.FindGameObjectWithTag("Data").GetComponent<DataScript>().data.items.Add("cactus");
                cactus.gameObject.SetActive(true);
                yield return new WaitForSeconds(1);
                break;
            case '4':
                GameObject.FindGameObjectWithTag("Data").GetComponent<DataScript>().data.items.Add("graphicsCard");
                card.gameObject.SetActive(true);
                yield return new WaitForSeconds(1);
                break;
            case '5':
                GameObject.FindGameObjectWithTag("Data").GetComponent<DataScript>().data.items.Add("touchOfGray");
                gray.gameObject.SetActive(true);
                yield return new WaitForSeconds(1);
                break;
        }


        if (level == "puzzle2")
        {
            level = GameObject.FindGameObjectWithTag("Data").GetComponent<DataScript>().data.puzzleLevel = "puzzle8";
            SceneManager.LoadScene("PuzzleTest");
        }else if (level == "puzzle8")
        {
            level = GameObject.FindGameObjectWithTag("Data").GetComponent<DataScript>().data.puzzleLevel = "puzzle7";
            SceneManager.LoadScene("PuzzleTest");
        }
        else if (level == "puzzle7")
        {
            SceneManager.LoadScene("TestDialog");
        }
        else if (GameObject.FindGameObjectWithTag("Data").GetComponent<DataScript>().data.location == "dorm")
        {
            SceneManager.LoadScene("Dorm");
        }
        else if(GameObject.FindGameObjectWithTag("Data").GetComponent<DataScript>().data.location == "oliver")
        {
            SceneManager.LoadScene("Oliver");
        }
    }
}