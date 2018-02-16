using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManagerScript : MonoBehaviour {

    public GameObject block;

    GameObject selected;

    List<GameObject> blocks;

    void Awake()
    {
        blocks = new List<GameObject>();
        blocks.Add(Instantiate(block, new Vector3(0, 0, 0), Quaternion.identity));
        blocks.Add(Instantiate(block, new Vector3(0, 1, 0), Quaternion.identity));
        blocks.Add(Instantiate(block, new Vector3(1, 0, 0), Quaternion.identity));
        blocks.Add(Instantiate(block, new Vector3(1, 1, 0), Quaternion.identity));
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
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
}
