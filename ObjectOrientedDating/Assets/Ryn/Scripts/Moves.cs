using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Moves : MonoBehaviour {

    GameObject manager;

    PuzzleManagerScript script;
    Text movesOrWhatever;

    private void Awake()
    {
        manager = GameObject.Find("Puzzle Manager");
        script = manager.GetComponent<PuzzleManagerScript>();
        this.GetComponent<Text>();
    }

    // Use this for initialization
    void Start () {
        movesOrWhatever.text = "0";
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
