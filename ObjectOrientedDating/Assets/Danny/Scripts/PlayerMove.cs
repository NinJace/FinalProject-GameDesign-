using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{

    CharacterController charControl;

    public Transform character;

    private float moveFB, moveLR;


    // Various Inputs for battery

    public Image menu;

    // Various Input bools


    bool Moving = false;
    public bool Use = false;
    bool NearDoor = false;

    public float radius;
    public float moveSpeed = 750.0f;

    private float rotationSpeed = 60.0f;


    private float verticalVelocity;
    public float gravity = 60.0f;
    //public float attackRange = 5f;
    //public int score = 0;
    public float jumpDist = 10f;
    int jumpTimes;



    public AudioSource[] SFXClips = new AudioSource[11];



    private float resetHeight = 30.0f;

    void Awake()
    {
        charControl = GetComponent<CharacterController>();



    }

    void OnTriggerEnter(Collider collision)
    {


    }

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        GameObject.FindGameObjectWithTag("Data").GetComponent<DataScript>().data.save();
    }

    void Update()
    {

        MovePlayer();

        PlayerControl();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
             SceneManager.LoadScene("MenuTest");

        }
        
        //if (Input.GetKeyDown(KeyCode.V))
        //{
        //    List<string> dateItems = new List<string>();
        //    dateItems.Add("touchController"); 
        //    dateItems.Add("textbook");
        //    dateItems.Add("gourmetCoffee");
        //    dateItems.Add("cactus");
        //    dateItems.Add("graphicsCard");
        //    dateItems.Add("touchOfGray");

        //    GameObject.FindGameObjectWithTag("Data").GetComponent<DataScript>().data.items = dateItems; 
        //}
        
        
    }
   
    void MovePlayer()
    {




        moveFB = Input.GetAxis("Vertical") * moveSpeed;
        moveLR = Input.GetAxis("Horizontal") * moveSpeed;



        Vector3 movement = new Vector3(moveLR, verticalVelocity, moveFB);

        movement = character.rotation * movement;


        character.GetComponent<CharacterController>().Move(movement * Time.deltaTime);


        if (charControl.isGrounded == true)
        {
            //set jump times to 0
            jumpTimes = 0;
        }

        if (jumpTimes < 1)
        {
            //Jump Input

            if (Input.GetButtonDown("Jump"))
            {

                verticalVelocity += jumpDist;

                //set jumptimes +1

                jumpTimes += 1;
            }
        }

        if (charControl.isGrounded)
        {
            verticalVelocity = -gravity * Time.deltaTime;


            if (Input.GetKeyDown(KeyCode.Space))
            {
                verticalVelocity += jumpDist;
            }
        }
        else
        {

            verticalVelocity -= gravity * Time.deltaTime;
        }

        
        
  
    }
    
    void PlayerControl()
    {
        
        if (Input.GetKeyDown(KeyCode.Mouse0) && NearDoor == true)
        {
            Use = true;
            //Debug.Log("Use: " + Use);
        }

        
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Door")
        {
            NearDoor = true;

        }

        //Debug.Log("Near Door: " + NearDoor);
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Door")
        {
            NearDoor = false;
            Use = false;
        }

        //Debug.Log("Near Door: " + NearDoor);
    }


    //void UseCheck()
    //{
    //    GameObject[] doors;

    //    doors = GameObject.FindGameObjectsWithTag("Door");

    //    Vector3 player = transform.forward.normalized;

    //    foreach (GameObject Door in doors)
    //    {
    //        if (Vector3.Distance(character.position, Door.transform.position) <= radius)
    //        {

    //            Vector3 actDist = (Door.transform.position - character.transform.position).normalized;

    //            if (Vector3.Dot(player, actDist) >= 0.1f)
    //            {
    //                //Door.GetComponent<TrooperAI>().state = TrooperStates.Death;
    //                PlayerControl();
    //            }
    //        }
    //    }

    //    //enemies = GameObject.FindGameObjectsWithTag("Vader");
    //    //foreach (GameObject Vader in enemies)
    //    //{
    //    //    if (Vector3.Distance(character.position, Vader.transform.position) <= attackRange)
    //    //    {
    //    //        //Vector3 lukey = transform.forward.normalized;
    //    //        Vector3 vade = (Vader.transform.position - character.transform.position).normalized;

    //    //        if (Vector3.Dot(lukey, vade) >= 0.5f)
    //    //        {
    //    //            Vader.GetComponent<VaderAI>().takeDamage();
    //    //            score += 25;
    //    //        }

    //    //        if (Vader.GetComponent<VaderAI>().state == VaderStates.Death)
    //    //        {
    //    //            SceneManager.LoadScene("Victory");
    //    //        }
    //    //    }
    //    //}
    //}

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawSphere(transform.position, attackRange);
    //}

}
