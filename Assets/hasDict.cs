using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hasDict : MonoBehaviour
{
    public bool onDict;
    public bool hasdict=false;
    [SerializeField] GameObject DictCanvas;
    [SerializeField] GameObject Dict;
    public Camera CamToLoc;
    
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
        DictCanvas.gameObject.SetActive(false);
    }

    void Update()
    {
        if (onDict)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Dict.gameObject.SetActive(false);
                hasdict = true;
                // toOpen.gotKey = true;
                // finisgText.SetActive(true);
            }
        }
        if (hasdict)
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                if (!DictCanvas.gameObject.activeSelf)
                {
                    DictCanvas.gameObject.SetActive(true);
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    CamToLoc.GetComponent<vThirdPersonCamera>().enabled = false;
                    // lookx.enabled = false;
                    // looky.enabled = false;
                }
                else
                {
                    DictCanvas.gameObject.SetActive(false);
                    // if (!PassCanvas.gameObject.activeSelf && !sliderMenager.gameObject.activeSelf)
                    // {
                        Cursor.visible = false;
                        Cursor.lockState = CursorLockMode.Locked;
                        CamToLoc.GetComponent<vThirdPersonCamera>().enabled = true;

                        // lookx.enabled = true;
                        // looky.enabled = true;
                    // }
                }                
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
            GUI.Box(new Rect(Screen.width / 2 - 550, Screen.height - 40, 400, 30), "Press B to Open the Brille Dictionary", gustyle);
        }
    }
}
