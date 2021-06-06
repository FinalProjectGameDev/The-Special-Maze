using UnityEngine;
using UnityEngine.UI;

public class PianoDoorController : MonoBehaviour
{
    public bool keyNeeded = false;              //Is key needed for the door
    public bool gotKey;                  //Has the player acquired key
    public bool HandleConnected = false;
    public GameObject keyGameObject;            //If player has Key,  assign it here
    public GameObject txtToDisplay;             //Display the information about how to close/open the door
    public GameObject doorHandle;               //The handle on the door

    private bool playerInZone;                  //Check if the player is in the zone
    private bool doorOpened;                    //Check if door is currently opened or not

    private Animation doorAnim;
    private BoxCollider doorCollider;           //To enable the player to go through the door if door is opened else block him

    public static AudioSource[] fourSounds;
    public static int index;
    [SerializeField]
    public AudioSource correctAnswer;
    [SerializeField]
    public AudioSource wrongAnswer;
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
        index = -1;
        gotKey = false;
        doorOpened = false;                     //Is the door currently opened
        playerInZone = false;                   //Player not in zone
        HandleConnected = false;
        doorState = DoorState.Closed;           //Starting state is door closed

        txtToDisplay.SetActive(false);
        doorHandle.SetActive(false);

        doorAnim = transform.parent.gameObject.GetComponent<Animation>();
        doorCollider = transform.parent.gameObject.GetComponent<BoxCollider>();

        //If Key is needed and the KeyGameObject is not assigned, stop playing and throw error
        //if keyNeeded && keyGameObject == null)
        //{
        //    //UnityEditor.EditorApplication.isPlaying = false;
        //    Debug.LogError("Assign Key GameObject");
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        txtToDisplay.SetActive(true);
        playerInZone = true;
    }

    private void OnTriggerExit(Collider other)
    {
        playerInZone = false;
        txtToDisplay.SetActive(false);
    }

    private void Update()
    {
        //To Check if the player is in the zone
        if (playerInZone)
        {
            if (doorState == DoorState.Opened)
            {
                txtToDisplay.GetComponent<Text>().text = "Press 'E' to Close";
                doorCollider.enabled = false;
            }
            else if (doorState == DoorState.Closed)
            {
                if (gotKey)
                {
                    txtToDisplay.GetComponent<Text>().text = "Press E to Open";
                    doorCollider.enabled = true;
                }
                else
                {
                    txtToDisplay.GetComponent<Text>().text = "Need to repeat the music";
                    doorCollider.enabled = true;
                }
            }
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
        if(index == 3)
        {
            for (int i = 0; i < fourSounds.Length; i++)
            {
                if (!fourSounds[i].Equals(MusicAfterPressButton.fourSounds[i]))
                {
                    gotKey = false;
                    wrongAnswer.Play();
                    break;
                }
                gotKey = true;
                correctAnswer.Play();
            }
        }
    }

    public static void setNotes(AudioSource audioSource)
    {
        index = (index + 1) % 4;
        fourSounds[index] = audioSource;
    }
        
}
