using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayConvo : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Display();
	}
	
	// Update is called once per frame
	void Display()
    {
        foreach (ConvoEntry entry in XMLManager.ins.convoDB.list)
        {
            Debug.Log(entry.optionA + entry.optionB + entry.optionC);
        }
    }
}
