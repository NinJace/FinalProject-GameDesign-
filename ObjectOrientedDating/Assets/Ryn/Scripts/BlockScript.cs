using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScript : MonoBehaviour {
    bool selected;

    // Use this for initialization
    void Start () {
        selected = false;	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseDown()
    {
        //tell the puzzle manager that its selected
        if (Input.GetMouseButtonDown(0))
        {
            GetComponentInParent<PuzzleManagerScript>().blockSelected(gameObject);
        }
    }

    public void toggleSelected()
    {
        selected = !selected;
        if (selected)
        {
            gameObject.GetComponent<Renderer>().material = new Material (Shader.Find("Selected"));
        }
        else
        {
            gameObject.GetComponent<Renderer>().material = new Material (Shader.Find("Normals"));
        }
    }        
}
