using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManagerScript : MonoBehaviour {

    public GameObject block;

    GameObject selected;

    List<GameObject> blocks;

    int[,] yzSolution;
    int[,] ySolution;
    int[,] zSolution;

    void Awake()
    {
        //make/load  answers
        yzSolution = new int[2, 3] {{1, 1, 0}, {0, 1, 1}};
        //ySolution = new int[]
    
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
    }

    // Use this for initialization
    void Start () {
        checkX();
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
        //makes sure all block still connected
        bool hasNeighbor = false;
        foreach (GameObject block in blocks)
        {
            if (block.transform.position.y == selected.transform.position.y && block.transform.position.z == selected.transform.position.z && !hasNeighbor)
            {
                if (blockAt(block.transform.position + Vector3.up) || blockAt(block.transform.position - Vector3.up) || blockAt(block.transform.position + Vector3.forward) || blockAt(block.transform.position - Vector3.forward))
                {
                    hasNeighbor = true;
                }
            }
        }
        //undoes invalid move
        if (!hasNeighbor)
        {
            moveX(-dir);
        }
        else
        {
            //check Y and Z if valid
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

        bool hasNeighbor = false;
        foreach (GameObject block in blocks)
        {
            if (block.transform.position.x == selected.transform.position.x && block.transform.position.z == selected.transform.position.z)
            {
                if (blockAt(block.transform.position + Vector3.right) || blockAt(block.transform.position - Vector3.right) || blockAt(block.transform.position + Vector3.forward) || blockAt(block.transform.position - Vector3.forward))
                {
                    hasNeighbor = true;
                }
            }
        }

        if (!hasNeighbor)
        {
            moveY(-dir);
        }
        else
        {
            checkX();
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

        bool hasNeighbor = false;
        foreach (GameObject block in blocks)
        {
            if (block.transform.position.y == selected.transform.position.y && block.transform.position.x == selected.transform.position.x)
            {
                if (blockAt(block.transform.position + Vector3.right) || blockAt(block.transform.position - Vector3.right) || blockAt(block.transform.position + Vector3.up) || blockAt(block.transform.position - Vector3.up))
                {
                    hasNeighbor = true;
                }
            }
        }

        if (!hasNeighbor)
        {
            moveZ(-dir);
        }
        else
        {
            checkX();
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

    //fix this
    bool checkX()
    {
        List<Tuple> tempYZ = new List<Tuple>();
        int minY = int.MaxValue;
        int minZ = int.MaxValue;

        //calculate mins and make temporary list
        foreach (GameObject block in blocks)
        {
            if(block.transform.position.y < minY)
            {
                minY = (int)block.transform.position.y;
            }
            if (block.transform.position.z < minZ)
            {
                minZ = (int)block.transform.position.z;
            }
            tempYZ.Add(new Tuple((int)block.transform.position.y, (int)block.transform.position.z));
        }

        //normalilze list
        foreach(Tuple pair in tempYZ)
        {
            pair.x = pair.x + -minY;
            pair.y = pair.y + -minZ;
        }

        //check solution
        bool correct = true;
        foreach(Tuple pair in tempYZ)
        {
            if(pair.x >= yzSolution.GetLength(0) || pair.y >= yzSolution.GetLength(1) || yzSolution[pair.x, pair.y] != 1)
            {
                correct = false;
                break;
            }
        }

        Debug.Log("The X side is " + correct);
        return correct;
    }

}

class Tuple
{
    public int x, y;

    public Tuple(int x, int y)
    {
        x = this.x;
        y = this.y;
    }
}
