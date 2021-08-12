using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class TutorialManager : MonoBehaviour
{
    public int tutorialIndex;
    public int keysToPlayIndex;

    // public Canvas tutorialCanvas;
    public Tutorial[] regularTutorials;
    public Tutorial[] wheelchairTutorials;
    public Tutorial[] currTutorials;
    public TMP_Text currentDescription;
    public Image currentKeys;
    public string typePlayer;
    public UIController UIC;


    private void Start()
    {
        tutorialIndex = -1;
        keysToPlayIndex = 0;
        typePlayer = PlayerPrefs.GetString("CurrentSelectedCharacter", "Deaf");
        if (typePlayer == "Wheelchair"){
            currTutorials = wheelchairTutorials;
        }
        else{
            currTutorials = regularTutorials;
        }
        UpdateCanvas();
    }

    void Update()
    {
        if (tutorialIndex < currTutorials.Length)
        {
            if (Input.GetKey(currTutorials[tutorialIndex].keysToPlay[keysToPlayIndex]))
            {

                if (keysToPlayIndex == currTutorials[tutorialIndex].keysToPlay.Length - 1)
                {
                    UpdateCanvas();
                }

                else keysToPlayIndex++;
            }
        }

    }

    public void UpdateCanvas()
    {
        if (tutorialIndex < currTutorials.Length - 1)
        {
            tutorialIndex++;
            keysToPlayIndex = 0;
            currentDescription.text = currTutorials[tutorialIndex].description;
            currentKeys.gameObject.SetActive(false);
            currentKeys = currTutorials[tutorialIndex].keys;
            currentKeys.gameObject.SetActive(true);
        }
        // else tutorialCanvas.gameObject.SetActive(false);    
        else UIC.closeTutorial();    
    }
}
