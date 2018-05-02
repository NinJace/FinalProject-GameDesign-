using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorScriptO : MonoBehaviour {

    public string Designation;
    
    public GameObject OpenPanel = null;

    bool clicked = false;

    void Start()
    {
        

    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Player")
        {
            //Debug.Log("Player collided with door with designation: " + Designation);
            OpenPanel.SetActive(true);
            if (Input.GetMouseButtonDown(0))
            {
                //Debug.Log("Clicked in Enter");
            }
        }
    }

    //// Update is called once per frame
    //void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        //Debug.Log("Player collided with door with designation: " + Designation);
    //        OpenPanel.SetActive(true);
    //        if (Input.GetMouseButtonDown(0))
    //        {
    //            //Debug.Log("Clicked in Enter");
    //        }
            
    //    }

    //}

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && Input.GetMouseButtonDown(0) && !clicked)
        {
            clicked = true;
            
                Debug.Log("Clicked in Stay");
                int scene = GameObject.FindGameObjectWithTag("Data").GetComponent<DataScript>().data.scene;
                if ((Designation == "101" || Designation == "102" || Designation == "103") && (scene == 2 || scene == 8))
                {
                    if (scene == 2)
                    {
                        GameObject.FindGameObjectWithTag("Data").GetComponent<DataScript>().data.puzzleLevel = "puzzle6";
                    }
                    GameObject.FindGameObjectWithTag("Data").GetComponent<DataScript>().data.person = "arun";
                    SceneManager.LoadScene("TestDialog");
                }
                switch (Designation)
                {
                    case "arun":
                        GameObject.FindGameObjectWithTag("Data").GetComponent<DataScript>().data.person = "arun";
                        if (scene == 5)
                        {
                            SceneManager.LoadScene("TestDialog");
                        }
                        else if (scene == 11 && GameObject.FindGameObjectWithTag("Data").GetComponent<DataScript>().data.items.Contains("touchController"))
                        {
                            SceneManager.LoadScene("TestDialog");
                        }
                        else if (scene == 14 && GameObject.FindGameObjectWithTag("Data").GetComponent<DataScript>().data.items.Contains("touchController") && GameObject.FindGameObjectWithTag("Data").GetComponent<DataScript>().data.items.Contains("cactus"))
                        {
                            SceneManager.LoadScene("TestDialog");
                        }
                        break;
                    case "conrad":
                        GameObject.FindGameObjectWithTag("Data").GetComponent<DataScript>().data.person = "conrad";
                        if (scene == 5)
                        {
                            SceneManager.LoadScene("TestDialog");
                        }
                        else if (scene == 11 && GameObject.FindGameObjectWithTag("Data").GetComponent<DataScript>().data.items.Contains("graphicsCard"))
                        {
                            SceneManager.LoadScene("TestDialog");
                        }
                        else if (scene == 14 && GameObject.FindGameObjectWithTag("Data").GetComponent<DataScript>().data.items.Contains("graphicsCard") && GameObject.FindGameObjectWithTag("Data").GetComponent<DataScript>().data.items.Contains("textbook"))
                        {
                            SceneManager.LoadScene("TestDialog");
                        }
                        break;
                    case "amini":
                        GameObject.FindGameObjectWithTag("Data").GetComponent<DataScript>().data.person = "amini";
                        if (scene == 5)
                        {
                            SceneManager.LoadScene("TestDialog");
                        }
                        else if (scene == 11 && GameObject.FindGameObjectWithTag("Data").GetComponent<DataScript>().data.items.Contains("gourmetCoffee"))
                        {
                            SceneManager.LoadScene("TestDialog");
                        }
                        else if (scene == 14 && GameObject.FindGameObjectWithTag("Data").GetComponent<DataScript>().data.items.Contains("gourmetCoffee") && GameObject.FindGameObjectWithTag("Data").GetComponent<DataScript>().data.items.Contains("touchOfGray"))
                        {
                            SceneManager.LoadScene("TestDialog");
                        }
                        break;
                    case "Exit":
                        GameObject.FindGameObjectWithTag("Data").GetComponent<DataScript>().data.scene++;
                        GameObject.FindGameObjectWithTag("Data").GetComponent<DataScript>().data.location = "dorm";
                        if (scene == 9)
                        {
                            GameObject.FindGameObjectWithTag("Data").GetComponent<DataScript>().data.person = "ryn";
                            SceneManager.LoadScene("TestDialog");
                        }
                        SceneManager.LoadScene("Dorm");
                        break;

                }
            
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            OpenPanel.SetActive(false);
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Clicked in Exit");
            }
        }
    }


    /*
    void (Collider other)
    {
        if (other.tag == "Player"){
            Debug.Log("Player collided");
            OpenPanel.SetActive(true);
            if (Input.GetKeyDown("0"))
            {
                anim.Play("door (4)");
                OpenPanel.SetActive(false);
            }
        }
    }
    void OnCollision(Collider other)
    {
        if(other.tag == "Player")
        {
            OpenPanel.SetActive(false);
        }

    }*/
}
