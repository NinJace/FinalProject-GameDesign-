using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorScriptD : MonoBehaviour
{

    public string Designation;

    public GameObject OpenPanel = null;

    void Start()
    {
        if (Designation == "jace" && GameObject.FindGameObjectWithTag("Data").GetComponent<DataScript>().data.scene == 9)
        {
            Destroy(gameObject);
        }

    }
    // Update is called once per frame
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Debug.Log("Player collided with door with designation: " + Designation);
            OpenPanel.SetActive(true);
            if (Input.GetMouseButtonDown(0))
            {
                //Debug.Log("Clicked in Enter");
                
            }
        }

    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (Input.GetMouseButtonDown(0))
            {
                //Debug.Log("Clicked in Stay");
                int scene = GameObject.FindGameObjectWithTag("Data").GetComponent<DataScript>().data.scene;

                switch (Designation)
                {
                    case "ryn":
                        GameObject.FindGameObjectWithTag("Data").GetComponent<DataScript>().data.person = "ryn";
                        if (scene == 10 && !GameObject.FindGameObjectWithTag("Data").GetComponent<DataScript>().data.items.Contains("cactus"))
                        {
                            GameObject.FindGameObjectWithTag("Data").GetComponent<DataScript>().data.puzzleLevel = "puzzle3";
                            SceneManager.LoadScene("TestDialog");
                        }
                        else if (scene == 10 && GameObject.FindGameObjectWithTag("Data").GetComponent<DataScript>().data.items.Contains("cactus"))
                        {
                            //nothing
                        }
                        else
                        {
                            SceneManager.LoadScene("TestDialog");
                        }
                        break;
                    case "jace":
                        GameObject.FindGameObjectWithTag("Data").GetComponent<DataScript>().data.person = "jace";
                        if (scene == 12 && !GameObject.FindGameObjectWithTag("Data").GetComponent<DataScript>().data.items.Contains("touchOfGray"))
                        {
                            GameObject.FindGameObjectWithTag("Data").GetComponent<DataScript>().data.puzzleLevel = "puzzle5";
                            SceneManager.LoadScene("TestDialog");
                        }
                        else if (scene == 12 && GameObject.FindGameObjectWithTag("Data").GetComponent<DataScript>().data.items.Contains("touchOfGray"))
                        {
                            //nothing
                        }
                        else
                        {
                            SceneManager.LoadScene("TestDialog");
                        }
                        break;
                    case "seth":
                        GameObject.FindGameObjectWithTag("Data").GetComponent<DataScript>().data.person = "seth";
                        if (scene == 4 && !GameObject.FindGameObjectWithTag("Data").GetComponent<DataScript>().data.items.Contains("graphicsCard"))
                        {
                            GameObject.FindGameObjectWithTag("Data").GetComponent<DataScript>().data.puzzleLevel = "puzzle4";
                            SceneManager.LoadScene("TestDialog");
                        }
                        else if (scene == 4 && GameObject.FindGameObjectWithTag("Data").GetComponent<DataScript>().data.items.Contains("graphicsCard"))
                        {
                            //nothing
                        }
                        else
                        {
                            SceneManager.LoadScene("TestDialog");
                        }
                        break;
                    case "danny":
                        GameObject.FindGameObjectWithTag("Data").GetComponent<DataScript>().data.person = "danny";
                        if (scene == 6 && !GameObject.FindGameObjectWithTag("Data").GetComponent<DataScript>().data.items.Contains("gourmetCoffee"))
                        {
                            GameObject.FindGameObjectWithTag("Data").GetComponent<DataScript>().data.puzzleLevel = "puzzle9";
                            SceneManager.LoadScene("TestDialog");
                        }
                        else if (scene == 16 && GameObject.FindGameObjectWithTag("Data").GetComponent<DataScript>().data.items.Contains("gourmetCoffee"))
                        {
                            //nothing
                        }
                        else
                        {
                            SceneManager.LoadScene("TestDialog");
                        }
                        break;
                    case "night":
                        if (scene % 3 == 0)
                        {
                            GameObject.FindGameObjectWithTag("Data").GetComponent<DataScript>().data.person = "night";
                            GameObject.FindGameObjectWithTag("Data").GetComponent<DataScript>().data.scene++;
                            if (scene == 3)
                            {
                                GameObject.FindGameObjectWithTag("Data").GetComponent<DataScript>().data.puzzleLevel = "puzzle2";
                                SceneManager.LoadScene("PuzzleTest");
                            }
                            SceneManager.LoadScene("TestDialog");
                        }
                        break;
                    case "closet":
                        if (scene < 11 && !GameObject.FindGameObjectWithTag("Data").GetComponent<DataScript>().data.items.Contains("touchController"))
                        {
                            GameObject.FindGameObjectWithTag("Data").GetComponent<DataScript>().data.puzzleLevel = "puzzle1";
                            SceneManager.LoadScene("PuzzleTest");
                        }
                        break;
                    case "Exit":
                        if (scene % 3 == 1)
                        {
                            GameObject.FindGameObjectWithTag("Data").GetComponent<DataScript>().data.scene++;
                            GameObject.FindGameObjectWithTag("Data").GetComponent<DataScript>().data.location = "oliver";
                            if (scene == 16 || scene == 17)
                            {
                                GameObject.FindGameObjectWithTag("Data").GetComponent<DataScript>().data.person = "end";
                                SceneManager.LoadScene("TestDialog");
                            }
                            SceneManager.LoadScene("Oliver");
                        }
                        break;
                }
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
                //Debug.Log("Clicked in Exit");
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
