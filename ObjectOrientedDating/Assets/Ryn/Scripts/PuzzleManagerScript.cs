using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManagerScript : MonoBehaviour {

    public GameObject block;

    GameObject selected;

    List<GameObject> blocks;

    int[] xSolution;
    int[] ySolution;
    int[] zSolution;

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
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //input to move rows of blocks
        if (Input.GetKeyDown(KeyCode.D))
        {
            moveX(1);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            moveX(-1);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            moveY(1);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            moveY(-1);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            moveZ(1);
        }
        if (Input.GetKeyDown(KeyCode.E))
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

    bool checkX()
    {

        return false;
    }

}
