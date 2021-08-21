using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DoorController : MonoBehaviour
{
    public bool keyNeeded = false;              //Is key needed for the door
    public bool gotKey;                  //Has the player acquired key
    public bool HandleConnected = false;
    public GameObject keyGameObject;            //If player has Key,  assign it here
    public GameObject doorHandle;               //The handle on the door

    private bool playerInZone;                  //Check if the player is in the zone
    private bool doorOpened;                    //Check if door is currently opened or not

    private Animation doorAnim;
    private BoxCollider doorCollider;           //To enable the player to go through the door if door is opened else block him

    public DogController dog;

    public QuestGiver QG;

    enum DoorState
    {
        Closed,
        Opened,
        Jammed
    }

    DoorState doorState = new DoorState();      //To check the current state of the door

    /// <summary>
    /// Initial State of every variables
    /// </summary>
    private void Start()
    {
        gotKey = false;
        doorOpened = false;                     //Is the door currently opened
        playerInZone = false;                   //Player not in zone
        HandleConnected = false;
        doorState = DoorState.Closed;           //Starting state is door closed

        doorHandle.SetActive(false);

        doorAnim = transform.parent.gameObject.GetComponent<Animation>();
        doorCollider = transform.parent.gameObject.GetComponent<BoxCollider>();

        //If Key is needed and the KeyGameObject is not assigned, stop playing and throw error
        if (keyNeeded && keyGameObject == null)
        {
            //UnityEditor.EditorApplication.isPlaying = false;
            Debug.LogError("Assign Key GameObject");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        playerInZone = true;
    }

    private void OnTriggerExit(Collider other)
    {
        playerInZone = false;
    }

    private void Update()
    {
        //To Check if the player is in the zone
        if (playerInZone)
        {
            if (doorState == DoorState.Opened)
            {
                doorCollider.enabled = false;
            }
            else if (doorState == DoorState.Closed)
            {
                if (gotKey)
                {
                    if (HandleConnected)
                    {
                        doorCollider.enabled = true;
                    }
                }
                else
                {
                    doorCollider.enabled = true;
                }
            }   
        }
        if (Input.GetKeyDown(KeyCode.T) && playerInZone && gotKey && !HandleConnected && doorState == DoorState.Closed)
        {
            doorHandle.SetActive(true);
            HandleConnected = true;
        }

        if (Input.GetKeyDown(KeyCode.E) && playerInZone)
        {
            doorOpened = !doorOpened;           //The toggle function of door to open/close

            if (doorState == DoorState.Closed && !doorAnim.isPlaying && !keyNeeded)
            {
                doorAnim.Play("Door_Open");
                doorState = DoorState.Opened;
            }
            if (doorState == DoorState.Closed && gotKey && !doorAnim.isPlaying && HandleConnected)
            {
                doorAnim.Play("Door_Open");
                doorState = DoorState.Opened;
                StartCoroutine(dog.GetComponent<DogController>().nextDestination());
                QG.openExplain();
                StartCoroutine(loadMainMenuScene());
            }
            if (doorState == DoorState.Opened && !doorAnim.isPlaying)
            {
                doorAnim.Play("Door_Close");
                doorState = DoorState.Closed;
            }
            else if (doorState == DoorState.Jammed && gotKey && !doorAnim.isPlaying && HandleConnected)
            {
                doorAnim.Play("Door_Open");
                doorState = DoorState.Opened;
            }
        }
    }

    IEnumerator loadMainMenuScene()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(0);
    }

    void OnGUI()
    {
        GUIStyle gustyle = new GUIStyle(GUI.skin.box);
        gustyle.fontSize = 40;
        if (playerInZone)
        {
            if (doorState == DoorState.Opened)
            {
                GUI.Box(new Rect(Screen.width / 2 - 300, Screen.height - 60, 600, 50), "Press 'E' to Close", gustyle);
            }
            else if (doorState == DoorState.Closed)
            {
                if (gotKey)
                {
                    if (HandleConnected)
                    {
                        GUI.Box(new Rect(Screen.width / 2 - 300, Screen.height - 60, 600, 50), "Press E to Open", gustyle);
                    }
                    else
                    {
                        GUI.Box(new Rect(Screen.width / 2 - 300, Screen.height - 60, 600, 50), "Press T to Connect The Hendle", gustyle);
                    }
                }
                else
                {
                    GUI.Box(new Rect(Screen.width / 2 - 300, Screen.height - 60, 600, 50), "Needs Handle", gustyle);
                }
            }
        }
    }
}
