using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestGiver : MonoBehaviour
{
    public Quest[] visualImpairmentScenario;
    public Quest[] walkingDisabilityScenario;
    public Quest[] deafScenario;
    public Quest[] currArr;
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
        currIndex = 0;
        typePlayer = PlayerPrefs.GetString("CurrentSelectedCharacter", "Deaf");
        openExplain();
        wasClosed = false;
    }

    public void openWindow()
    {
        currExplain = currArr[currIndex];
        UIC.openExplain();
        if (currExplain.next)
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
                currArr = deafScenario;
                break;
            case "Parkinson":
                break;
            case "Blindness":
                currArr = visualImpairmentScenario;
                break;
            case "Wheelchair":
                currArr = walkingDisabilityScenario;
                break;
        }
        openWindow();
    }

    public void closeWindow()
    {
        if (!wasClosed)
        {
            wasClosed = true;
            UIC.openTutorial();
        }
        while (currExplain.next) currExplain = currArr[++currIndex];
        currIndex++;
        UIC.closeExplain();
    }

    public void reopenExplain()
    {
        currIndex--;
        while (currIndex > 0 && currArr[currIndex - 1].next) currIndex--;
        openExplain();
    }

    public void nextWindow()
    {
        currIndex++;
        openWindow();
    }

    public void prevWindow()
    {
        currIndex--;
        openWindow();
    }

}
