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
    }

    void Update()
    {
        if (onButton)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
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
        for (int i = 0; i< fourSounds.Length; i++)
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
