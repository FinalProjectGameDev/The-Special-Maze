using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestGiver : MonoBehaviour
{
    public Quest [] visualImpairmentScenario;
    public Quest [] walkingDisabilityScenario;
    public Quest [] deafScenario;
    public Quest [] currArr;
    public int currIndex;
    public Quest currExplain;
    public Text title;
    public Text description;
    public GameObject prev;
    public GameObject next;
    public GameObject close;
    public UIController UIC;
    public string typePlayer;
    public bool wasClosed;

    void Awake()
    {
        currIndex = -1;
        typePlayer = PlayerPrefs.GetString("CurrentSelectedCharacter", "Deaf");
        openExplain();
        wasClosed = false;
    }

    public void openWindow(Quest[] givenArr)
    {
        currIndex++;
        currArr = givenArr;
        currExplain = givenArr[currIndex];
        UIC.openExplain();
        if(currExplain.next)
        {
            next.SetActive(true);
        }
        else
        {
            next.SetActive(false);
        }
        if (currExplain.Prev)
        {
            prev.SetActive(true);
        }
        else
        {
            prev.SetActive(false);
        }
        title.text = currExplain.title;
        description.text = currExplain.description;
        
    }

    public void openExplain()
    {
        switch (typePlayer)
        {
            case "Deaf":
               openWindow(deafScenario); 
                break;
            case "Parkinson": 
                break;
            case "Blindness":
                  openWindow(visualImpairmentScenario);
                break;
            case "Wheelchair":
                openWindow(walkingDisabilityScenario);
                break;
        }
       
    }

    public void closeWindow()
    {
        if(!wasClosed) {
            wasClosed = true;
            UIC.openTutorial();
        }
        while(currExplain.next)
        {
            currIndex++;
            currExplain = currArr[currIndex];
        }
        UIC.closeExplain();
    }

    public void nextWindow()
    {
        openWindow(currArr);
    }

    public void prevWindow()
    {
        currIndex--;
        currIndex--;
        openWindow(currArr);
    }

}
