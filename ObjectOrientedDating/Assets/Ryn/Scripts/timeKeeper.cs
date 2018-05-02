using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timeKeeper : MonoBehaviour {
    
   
	// Use this for initialization
	void Start () {
        int scene = GameObject.FindGameObjectWithTag("Data").GetComponent<DataScript>().data.scene;
        Debug.Log("Scene#: " + scene);
        string day, time;
        switch (scene)
        {
            case 1:
            case 2:
            case 3:
                day = "1";
                break;
            case 4:
            case 5:
            case 6:
                day = "2";
                break;
            case 7:
            case 8:
            case 9:
                day = "3";
                break;
            case 10:
            case 11:
            case 12:
                day = "4";
                break;
            case 13:
            case 14:
            case 15:
                day = "5";
                break;
            case 16:
                day = "6";
                break;
            default:
                day = "0";
                break;
        }
        scene = scene % 3;
        switch (scene)
        {
            case 1:
                time = "Morning";
                break;
            case 2:
                time = "Afternoon";
                break;
            case 0:
                time = "Night";
                break;
            default:
                time = "Never";
                break;
        }
        gameObject.GetComponent<Text>().text = "Day: "+ day +"\nTime: "+ time;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
