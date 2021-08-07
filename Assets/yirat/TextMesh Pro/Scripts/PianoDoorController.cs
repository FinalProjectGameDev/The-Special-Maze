using UnityEngine;
using UnityEngine.UI;

public class PianoDoorController : MonoBehaviour
{
    public bool keyNeeded = false;              //Is key needed for the door
    public bool gotKey;                  //Has the player acquired key

    public bool playerInZone;                  //Check if the player is in the zone
    private bool doorOpened;                    //Check if door is currently opened or not

    private Animation doorAnim;
    private BoxCollider doorCollider;           //To enable the player to go through the door if door is opened else block him

    private AudioSource[] fourSounds = new AudioSource[4];
    public static int index;
    [SerializeField]
    public AudioSource correctAnswer;
    [SerializeField]
    public AudioSource wrongAnswer;
    [SerializeField]
    public Canvas pianoCanvas;

    public UIController UIC;

    public DogController dog;

    public MusicAfterPressButton musicAfterPressButton;
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
        doorState = DoorState.Closed;           //Starting state is door closed

        doorAnim = transform.parent.gameObject.GetComponent<Animation>();
        doorCollider = transform.parent.gameObject.GetComponent<BoxCollider>();
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
                    doorCollider.enabled = true;
                }
                else
                {
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
            if (doorState == DoorState.Closed && gotKey && !doorAnim.isPlaying)
            {
                doorAnim.Play("Door_Open");
                doorState = DoorState.Opened;
                StartCoroutine(dog.GetComponent<DogController>().nextDestination());
            }
            if (doorState == DoorState.Opened && !doorAnim.isPlaying)
            {
                doorAnim.Play("Door_Close");
                doorState = DoorState.Closed;
            }
            else if (doorState == DoorState.Jammed && gotKey && !doorAnim.isPlaying)
            {
                doorAnim.Play("Door_Open");
                doorState = DoorState.Opened;
            }
        }
        if(index == 3)
        {
            index = -1;
            gotKey = true;
            for (int i = 0; i < fourSounds.Length; i++)
            {
                if (!fourSounds[i].Equals(MusicAfterPressButton.fourSounds[i]))
                {
                    gotKey = false;
                    wrongAnswer.Play();
                }
            }
            if (gotKey) { 
                correctAnswer.Play();
                UIC.isOpen = false;
                pianoCanvas.gameObject.SetActive(false);
                 Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            //CamToLoc.GetComponent<vThirdPersonCamera>().enabled = true;
            }           
        }

        if(!pianoCanvas.gameObject.activeSelf)
        {
            index = -1;
        }
    }

    public void setNotes(AudioSource audioSource)
    {
        index += 1 ;
        Debug.Log(index);
        fourSounds[index] = audioSource;
    }

    void OnGUI()
    {
        GUIStyle gustyle = new GUIStyle(GUI.skin.box);
        gustyle.fontSize = 40;
        if (playerInZone && !musicAfterPressButton.onButton)
        {
            if (doorState == DoorState.Opened)
            {
                GUI.Box(new Rect(Screen.width / 2 - 300, Screen.height - 60, 600, 50), "Press 'E' to Close", gustyle);
            }
            else if (doorState == DoorState.Closed)
            {
                if (gotKey)
                {
                    GUI.Box(new Rect(Screen.width / 2 - 300, Screen.height - 60, 600, 50), "Press E to Open", gustyle);
                }
                else
                {
                    GUI.Box(new Rect(Screen.width / 2 - 300, Screen.height - 60, 600, 50), "Need to repeat the music", gustyle);
                }
            }
        }
    }
}
