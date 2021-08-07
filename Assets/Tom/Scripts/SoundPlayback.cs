using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayback : MonoBehaviour
{
    [SerializeField]
    public AudioSource[] sounds;

    [SerializeField]
    public GameObject gameObjectGenerateRandomNumber;

    private GenerateRandomNumber generateRandomNumber;
    public int theChoosenNum;
    public bool onButton;

    void OnTriggerEnter(Collider other)
    {
        onButton = true;


        
    }

    void OnTriggerExit(Collider other)
    {
        onButton = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        generateRandomNumber = gameObjectGenerateRandomNumber.GetComponent<GenerateRandomNumber>();
        theChoosenNum = generateRandomNumber.theChoosenNum;
    }

    // Update is called once per frame
    void Update()
    {
        if (onButton)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Played");
                sounds[theChoosenNum].Play();
            }
        }
    }
        
        
    void OnGUI()
    {
        GUIStyle gustyle = new GUIStyle(GUI.skin.box);
        gustyle.fontSize = 40;
        if (onButton)
        {
            GUI.Box(new Rect(Screen.width / 2 - 300, Screen.height - 60, 600, 50), "Press E to play sound", gustyle);
        }

    }
}
