using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoadTest : MonoBehaviour {

    public GameObject persistentDataStore;
    public GameObject creditCanvas;
    public GameObject guideCanvas;
    bool cActive;
    bool gActive;

    private void Start()
    {
        cActive = false;
        gActive = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        if(GameObject.FindGameObjectsWithTag("Data").Length > 0)
        {
            foreach(GameObject datas in GameObject.FindGameObjectsWithTag("Data"))
            {
                Destroy(datas);
            }

        }
    }

    public void newGame()
    {
        //make new datastore game object
        GameObject data = Instantiate(persistentDataStore);
        data.GetComponent<DataScript>().data = new SaveData();
        //make it persistent
        DontDestroyOnLoad(data);
        //swap scenes
        SceneManager.LoadScene("tutorial");
    }

    public void loadGame()
    {
        //load in saved data to object
        GameObject data = Instantiate(persistentDataStore);
        data.GetComponent<DataScript>().data = SaveData.Load();
        //make persistent
        DontDestroyOnLoad(data);
        //load correct scene based on data
        if (GameObject.FindGameObjectWithTag("Data").GetComponent<DataScript>().data.location == "dorm")
        {
            SceneManager.LoadScene("Dorm");
        }
        else if (GameObject.FindGameObjectWithTag("Data").GetComponent<DataScript>().data.location == "oliver")
        {
            SceneManager.LoadScene("Oliver");
        }
    }

    public void quit()
    {
        Application.Quit();
    }

    public void toggleCredits()
    {
        cActive = !cActive;
        creditCanvas.SetActive(cActive);
    }

    public void toggleGuide()
    {
        gActive = !gActive;
        guideCanvas.SetActive(gActive);
    }

}
