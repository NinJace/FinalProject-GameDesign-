using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScript : MonoBehaviour {

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseDown()
    {
        //tell the puzzle manager that its selected
        GetComponentInParent<PuzzleManagerScript>().blockSelected(gameObject);
    }

    void toggleSelected()
    {
        
    }
}
