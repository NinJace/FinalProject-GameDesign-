using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameScript : MonoBehaviour {

    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void LoadScene()
    {
        GameObject.Destroy(GameObject.FindGameObjectWithTag("Data"));
        SceneManager.LoadScene("MenuTest");
    }
	
	
}
