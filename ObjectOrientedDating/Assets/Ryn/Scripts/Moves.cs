using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Moves : MonoBehaviour {

    GameObject manager;

    PuzzleManagerScript script;
    Text moves;

    private void Awake()
    {
        manager = GameObject.Find("PuzzleManager");
        script = manager.GetComponent<PuzzleManagerScript>();
        moves = GetComponent<Text>();
    }

    // Use this for initialization
    void Start () {
        GetComponent<Text>().text = "0";
	}
	
	// Update is called once per frame
	void Update () {
       moves.text = script.moves.ToString();
	}
}
