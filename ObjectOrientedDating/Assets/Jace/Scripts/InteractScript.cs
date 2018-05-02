using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractScript : MonoBehaviour {
    public string Designation;
    public GameObject OpenPanel = null;
    void Start()
    {


    }
    // Update is called once per frame
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Debug.Log("Player collided with door with designation: " + Designation);
            OpenPanel.SetActive(true);
            if (Input.GetKeyDown("0"))
            {

            }
        }

    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            OpenPanel.SetActive(false);
        }
    }

}
