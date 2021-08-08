using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestGiver : MonoBehaviour
{
    public Quest [] visualImpairmentScenario;
    public Quest [] walkingDisabilityScenario;
    public Quest[] deafScenario;
    public Quest[] currArr;
    public int currIndex;
    public Quest currExplain;
    public GameObject window;
    public Text title;
    public Text description;
    public GameObject prev;
    public GameObject next;
    public GameObject close;

    void Awake()
    {
        currIndex = 1;
        OpenWindow('v');
    }

    public void OpenWindow(Quest[] givenArr)
    {
        currArr = givenArr;
        currExplain = givenArr[currIndex];
        window.SetActive(true);
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
        currIndex++;
    }

    public void OpenWindow(char c)
    {
        if(c == 'v')
        {
            currIndex = 0;
            currArr = visualImpairmentScenario;
            OpenWindow(visualImpairmentScenario);
        }
        else if(c == 'w')
        {
            currArr = walkingDisabilityScenario;
            OpenWindow(walkingDisabilityScenario);
        }
        else
        {
            currArr = deafScenario;
            OpenWindow(deafScenario);
        }
       
    }

    public void closeWindow()
    {
        Debug.Log("enter");
        while(currExplain.next)
        {
            currIndex++;
            currExplain = currArr[currIndex];
        }
        window.SetActive(false);
    }

    public void nextWindow()
    {
        OpenWindow(currArr);
    }

    public void prevWindow()
    {
        currIndex--;
        currIndex--;
        OpenWindow(currArr);
    }

}
