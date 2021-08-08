using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hasDict : MonoBehaviour
{
    public bool onDict;
    public bool hasdict=false;
    [SerializeField] UIController UIC;
    [SerializeField] GameObject Dict;
    
    void OnTriggerEnter(Collider other)
    {
        onDict = true;
    }

    void OnTriggerExit(Collider other)
    {
        onDict = false;
    }

    void Start()
    {
        UIC.closeDict();
    }

    void Update()
    {
        if (onDict)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Dict.gameObject.SetActive(false);
                hasdict = true;
            }
        }
        if (hasdict)
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                if(UIC.dictIsOpen){
                    UIC.closeDict();
                }
                else UIC.openDict();
            }
        }
    }

    void OnGUI()
    {
        GUIStyle gustyle = new GUIStyle(GUI.skin.box);
        // gustyle.fontSize = 40;
        if (onDict && !hasdict)
        {
            gustyle.fontSize = 40;
            GUI.Box(new Rect(Screen.width / 2 - 300, Screen.height - 60, 600, 50), "Press E to Get the Dictionary", gustyle);
        }
        if (hasdict)
        {
            gustyle.fontSize = 20;
            if (!UIC.dictIsOpen)
            {
                GUI.Box(new Rect(40, Screen.height - 40, 300, 30), "Press B to Open the Dictionary", gustyle);
            }
            else
            {
                GUI.Box(new Rect(40, Screen.height - 40, 300, 30), "Press B to Close the Dictionary", gustyle);
            }
        }
    }
}
