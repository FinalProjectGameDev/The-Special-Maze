using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {

    [Tooltip("Usee Blur in Pause Menu?")]
    public bool useBlur;

    [Header("Both UI Panels")]
    public GameObject saveMenu;
    public GameObject pauseMenu;
    public GameObject minimap;
    public GameObject keyboard;
    public GameObject piano;
    public GameObject dictionary;
    public GameObject instructions;
    public GameObject explains;

    public QuestGiver QG;

    Fader fader;
    //[HideInInspector]
    public bool pauseIsOpen;
    public bool minimapIsOpen;
    public bool keypadIsOpen;
    public bool pianoIsOpen;
    public bool dictIsOpen;
    public bool explainIsOpen;

    public Canvas[] allUI;

    [Header("Pause Game and Resume Game Events")]
    public UnityEngine.Events.UnityEvent onPause = new UnityEngine.Events.UnityEvent();
    public UnityEngine.Events.UnityEvent onUnpause = new UnityEngine.Events.UnityEvent();

    // [HideInInspector]
    public List<LoadSlotIdentifier> loadSlots;

    public Camera CamToLoc;

    // Use this for initialization
    IEnumerator Start () {

        //PlayerPrefs.DeleteAll();

        //find fader
        fader = FindObjectOfType<Fader>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        yield return new WaitForSeconds(0.5f);

        // QG.openExplain();
    }

    // Update is called once per frame
    void Update () {
        if (pauseIsOpen || keypadIsOpen || pianoIsOpen || dictIsOpen || explainIsOpen)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            CamToLoc.GetComponent<vThirdPersonCamera>().enabled = false;

        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            CamToLoc.GetComponent<vThirdPersonCamera>().enabled = true;

        }

        //if save menu is not opened
        // if (!saveMenu.active && canOpen())
        if (!saveMenu.active)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                if (!pauseIsOpen)
                    openPauseMenu();
                else
                    closePauseMenu();
            }
        }

        if (!saveMenu.active && !pauseMenu.active)
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                if (!minimapIsOpen && !pauseIsOpen)
                    openMinimap();
                else
                    closeMinimap();
            }
        }
	}

    public void openMinimap()
    {
        minimap.SetActive(true);
        minimapIsOpen = true;
    }
    public void closeMinimap()
    {
        minimap.SetActive(false);
        minimapIsOpen = false;
    }

    public void openKeypad()
    {
        keyboard.SetActive(true);
        keypadIsOpen = true;
    }
    public void closeKeypad()
    {
        keyboard.SetActive(false);
        keypadIsOpen = false;
    }

    public void openPiano()
    {
        piano.SetActive(true);
        pianoIsOpen = true;
    }
    public void closePiano()
    {
        piano.SetActive(false);
        pianoIsOpen = false;
    }

    public void openDict()
    {
        dictionary.SetActive(true);
        dictIsOpen = true;
    }
    public void closeDict()
    {
        dictionary.SetActive(false);
        dictIsOpen = false;
    }

    public void openExplain()
    {
        explains.SetActive(true);
        explainIsOpen = true;
    }

    public void closeExplain()
    {
        explains.SetActive(false);
        explainIsOpen = false;
    }

    public void openPauseMenu() {

        allUI = FindObjectsOfType<Canvas>();
        //disable all UI
        for (int i = 0; i < allUI.Length; i++)
        {
            if (allUI[i].name != "Fader")
                allUI[i].gameObject.SetActive(false);
        }


        saveMenu.SetActive(false);
        pauseMenu.SetActive(true);

        //play sound
        GetComponent<SaveGameUI>().playClickSound();

        //play anim
        GetComponent<Animator>().Play("OpenPauseMenu");

        //time = almost 0
        // if(!usingUFPS)
        //     Time.timeScale = 0.0001f;

        pauseIsOpen = true;

        //init pause menu options
        GetComponent<PauseMenuOptions>().Init();

        //enable blur
        if (useBlur)
        {
            if (Camera.main.GetComponent<Animator>())
                Camera.main.GetComponent<Animator>().Play("BlurOff");

        }

        onPause.Invoke();

    }


    public void closePauseMenu() {

        //enable all UI
        for (int i = 0; i < allUI.Length; i++)
        {
           allUI[i].gameObject.SetActive(true);
        }


        //play sound
        GetComponent<SaveGameUI>().playClickSound();

        //play anim
        GetComponent<Animator>().Play("ClosePauseMenu");
        // hideMenus();

 
        pauseIsOpen = false;

        //enable blur
        if (useBlur)
        {
            if(Camera.main.GetComponent<Animator>())
            Camera.main.GetComponent<Animator>().Play("BlurOff");
        }

        onUnpause.Invoke();

    }

    public void hideMenus() {
        saveMenu.SetActive(false);
        pauseMenu.SetActive(false);
    }

    public void goToMainMenu() {
        //delete player
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Destroy(player);

        //restore time scale (important)
        Time.timeScale = 1f;

        //load main menu
        #if !EMM_ES2
        PlayerPrefs.SetString("sceneToLoad", "");
        #else
        PlayerPrefs.SetString("sceneToLoad", "");
        ES2.Save("", "sceneToLoad");
        #endif

        //hide all menus
        hideMenus();

        //load level via fader
        fader.FadeIntoLevel("LoadingScreen");
    }

    public void openLoadGame() {
        GetComponent<Animator>().Play("loadGameOpen");
        initLoadGameMenu();
    }

    public void closeLoadGame() {
        GetComponent<Animator>().Play("loadGameClose");
    }

    void initLoadGameMenu() {
        if (loadSlots.Count > 0)
        {
            foreach (LoadSlotIdentifier lsi in loadSlots)
            {
                lsi.Init();
            }
        }

    }

    // [HideInInspector]
    // public bool openPMenu = true;
    // public bool canOpen()
    // {
    //     return openPMenu;
    // }


}
