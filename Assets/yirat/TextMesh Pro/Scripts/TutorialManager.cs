using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class TutorialManager : MonoBehaviour
{
    private int tutorialIndex;
    private int keysToPlayIndedx;

    public Canvas tutorialCanvas;
    public Tutorial[] regularTutorials;
    public Tutorial[] wheelchairTutorials;
    public TMP_Text currentDescription;
    public Image currentKeys;

    private void Start()
    {
        tutorialIndex = 0;
        keysToPlayIndedx = 0;
    }

    void Update()
    {
        if (tutorialIndex < regularTutorials.Length)
        {
            if (Input.GetKey(regularTutorials[tutorialIndex].keysToPlay[keysToPlayIndedx]))
            {

                if (keysToPlayIndedx == regularTutorials[tutorialIndex].keysToPlay.Length - 1)
                {
                    UpdateCanvas();
                }

                else keysToPlayIndedx++;
            }
        }

    }

    public void UpdateCanvas()
    {
        if (tutorialIndex < regularTutorials.Length - 1)
        {
            tutorialIndex++;
            keysToPlayIndedx = 0;
            currentDescription.text = regularTutorials[tutorialIndex].description;
            currentKeys.gameObject.SetActive(false);
            currentKeys = regularTutorials[tutorialIndex].keys;
            currentKeys.gameObject.SetActive(true);
        }
        else tutorialCanvas.gameObject.SetActive(false);    
    }
}
