using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManagerScript : MonoBehaviour {

    public GameObject block;

    GameObject selected;

    List<GameObject> blocks;
    List<Vector3> solution;
    
    void Awake()
    {
        //make 2 x 2 x 2 cube
        blocks = new List<GameObject>();
        for(int x = 0; x < 2; x++)
        {
            for (int y = 0; y < 2; y++)
            {
                for (int z = 0; z < 2; z++)
                {
                    blocks.Add(Instantiate(block, new Vector3(x, y, z), Quaternion.identity, gameObject.transform));
                }
            }
        }

        //make/load  answers
        solution = new List<Vector3>();
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
    }

    // Use this for initialization
    void Start () {
        check();
	}
	
	// Update is called once per frame
	void Update () {
        //input to move rows of blocks
        if (Input.GetKeyDown(KeyCode.D))
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
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            moveZ(1);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            moveZ(-1);
        }
    }

    public void blockSelected(GameObject block)
    {
        if(selected != null)
        {
            //disable selection feedback
        }
        //update selected
        selected = block;
        //enable selection feedback
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
            moveX(-dir);
        }
        else
        {
            check();
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
            moveY(-dir);
        }
        else
        {
            check();
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
            moveZ(-dir);
        }
        else
        {
            check();
        }
    }

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

    //convert to check with 3D coordinates
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

        //normalilze list
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
                Debug.Log("Block at " + vec + " is out of place.");
                break;
            }
        }

        Debug.Log("The solution is " + correct);
        return correct;
    }

    bool isValid()
    {
        bool hasNeighbor = true;
        foreach (GameObject block in blocks)
        {
            //make sure each block is touching one other
            if (!blockAt(block.transform.position + Vector3.up) && !blockAt(block.transform.position - Vector3.up) && !blockAt(block.transform.position + Vector3.forward) && !blockAt(block.transform.position - Vector3.forward) && !blockAt(block.transform.position + Vector3.right) && !blockAt(block.transform.position - Vector3.right))
            {
                hasNeighbor = false;
                break;
            }
        
        }
        return hasNeighbor;
    }
}