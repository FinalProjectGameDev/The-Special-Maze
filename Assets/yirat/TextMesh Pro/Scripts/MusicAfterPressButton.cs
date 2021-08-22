using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicAfterPressButton : MonoBehaviour
{

    [SerializeField]
    public AudioSource[] allSounds = new AudioSource[18];
    [SerializeField]
    public static AudioSource[] fourSounds = new AudioSource[4];
    [SerializeField]
    public bool onButton;
    public PianoDoorController pianoDoorController;
    public UIController UIC;

    public Animator player;

    string _currentSelectedCharName;


    void OnTriggerEnter(Collider other)
    {
        onButton = true;
    }

    void OnTriggerExit(Collider other)
    {
        onButton = false;
    }

    void Start()
    {
        for (int i = 0; i < fourSounds.Length; i++)
        {
            int index = Random.Range(0, 18);
            fourSounds[i] = allSounds[index];
        }
        _currentSelectedCharName = PlayerPrefs.GetString("CurrentSelectedCharacter", "Deaf");

        if (_currentSelectedCharName == "Blindness" || _currentSelectedCharName == "Deaf")
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        }
    }

    void Update()
    {
        if (onButton)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (player) player.SetTrigger("push");
                onButton = false;
                pianoDoorController.playerInZone = false;
                RoutineWrap();
            }
        }
    }

    public void RoutineWrap()
    {
        StartCoroutine(playSound());
    }

    IEnumerator playSound()
    {
        for (int i = 0; i < fourSounds.Length; i++)
        {
            fourSounds[i].Play();
            Debug.Log(fourSounds[i].name);
            yield return new WaitForSeconds(fourSounds[i].clip.length);
        }
        UIC.openPiano();
    }

    void OnGUI()
    {
        GUIStyle gustyle = new GUIStyle(GUI.skin.box);
        gustyle.fontSize = 40;
        if (onButton && !pianoDoorController.playerInZone)
        {
            GUI.Box(new Rect(Screen.width / 2 - 300, Screen.height - 60, 600, 50), "Press E to play sound", gustyle);
        }
    }
}
