using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Background : MonoBehaviour {

    int scene;
    string person;


    public GameObject background;
    public Sprite oliver;
    public Sprite dRoom;
    public Sprite jRoom;
    public Sprite rRoom;
    public Sprite sRoom;
    public Sprite office;
    public Sprite park;
    public Sprite food;
    public Sprite club;
    public Sprite malformation;

    public AudioSource audio;
    public AudioClip dormMusic;
    public AudioClip oliverMusic;
    public AudioClip clubMusic;
    public AudioClip dateMusic;

    private void Awake()
    {
        audio = GameObject.Find("Scripts").GetComponent<AudioSource>();
    }

    // Use this for initialization
    void Start () {
        background.GetComponent<SpriteRenderer>().sprite = malformation;
        scene = GameObject.FindGameObjectWithTag("Data").GetComponent<DataScript>().data.scene;
        person = GameObject.FindGameObjectWithTag("Data").GetComponent<DataScript>().data.person;

        switch (scene % 3)
        {
            //Dorms Night
            case 0:
                if (scene == 9)
                {

                    switch (person)
                    {
                        case "danny":
                            background.GetComponent<SpriteRenderer>().sprite = dRoom;
                            audio.clip = dormMusic;
                            audio.Play();
                            break;
                        case "jace":
                            background.GetComponent<SpriteRenderer>().sprite = club;
                            audio.clip = clubMusic;
                            audio.Play();
                            break;
                        case "ryn":
                            background.GetComponent<SpriteRenderer>().sprite = club;
                            audio.clip = clubMusic;
                            audio.Play();
                            break;
                        case "seth":
                            background.GetComponent<SpriteRenderer>().sprite = sRoom;
                            audio.clip = dormMusic;
                            audio.Play();
                            break;
                    }
                } else
                {
                    switch(person)
                    {
                        case "danny":
                            background.GetComponent<SpriteRenderer>().sprite = dRoom;
                            break;
                        case "jace":
                            background.GetComponent<SpriteRenderer>().sprite = jRoom;
                            break;
                        case "ryn":
                            background.GetComponent<SpriteRenderer>().sprite = rRoom;
                            break;
                        case "seth":
                            background.GetComponent<SpriteRenderer>().sprite = sRoom;
                            break;
                    }

                    audio.clip = dormMusic;
                    audio.Play();
                }
                break;

            //Dorms day
            case 1:
                switch(person)
                {
                    case "danny":
                        background.GetComponent<SpriteRenderer>().sprite = dRoom;
                        break;
                    case "jace":
                        background.GetComponent<SpriteRenderer>().sprite = jRoom;
                        break;
                    case "ryn":
                        background.GetComponent<SpriteRenderer>().sprite = rRoom;
                        break;
                    case "seth":
                        background.GetComponent<SpriteRenderer>().sprite = sRoom;
                        break;
                }
                audio.clip = dormMusic;
                audio.Play();
                break;

            //Oliver
            case 2:
                if (scene == 11 && (person == "amini" || person == "arun" || person == "conrad"))
                {
                    switch (person)
                    {
                        case "amini":
                            background.GetComponent<SpriteRenderer>().sprite = park;
                            break;
                        case "arun":
                            background.GetComponent<SpriteRenderer>().sprite = food;
                            break;
                        case "conrad":
                            background.GetComponent<SpriteRenderer>().sprite = office;
                            break;
                    }
                    audio.clip = dateMusic;
                    audio.Play();
                }else if(scene == 14 && (person == "amini" || person == "arun" || person == "conrad"))
                {
                    background.GetComponent<SpriteRenderer>().sprite = office;
                    audio.clip = dateMusic;
                    audio.Play();
                }
                else if (scene == 2 || scene == 8)
                {
                    background.GetComponent<SpriteRenderer>().sprite = oliver;
                    audio.clip = oliverMusic;
                    audio.Play();
                }
                else
                {
                    background.GetComponent<SpriteRenderer>().sprite = office;
                    audio.clip = oliverMusic;
                    audio.Play();
                }

                    
                break;
        }
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    enum location
    {
        Oliver,
        DRoom,
        JRoom,
        RRoom,
        SRoom,
        Closet,
        Office,
        Park,
        Food,
        Club
    }
}
