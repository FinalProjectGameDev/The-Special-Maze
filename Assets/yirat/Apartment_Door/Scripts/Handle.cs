using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Handle : MonoBehaviour
{
    private bool playerInZone;
    private BoxCollider handleCollider;
    public DoorController DC;

    // Start is called before the first frame update
    void Start()
    {
        handleCollider = transform.gameObject.GetComponent<BoxCollider>();
        playerInZone = false;                   //Player not in zone
    }

    private void OnTriggerEnter(Collider other)
    {
        playerInZone = true;
    }

    private void OnTriggerExit(Collider other)
    {
        playerInZone = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInZone && this.gameObject.activeSelf)
        {
            handleCollider.enabled = true;
        }

        if (Input.GetKeyDown(KeyCode.T) && playerInZone)
        {
            if (this.gameObject.activeSelf)
            {
                this.gameObject.SetActive(false);
                playerInZone = false;
                DC.gotKey = true;
            }

            else
            {
                this.gameObject.SetActive(true);
                DC.gotKey = false;
            }

        }
    }

    void OnGUI()
    {
        GUIStyle gustyle = new GUIStyle(GUI.skin.box);
        gustyle.fontSize = 40;
        if (playerInZone && this.gameObject.activeSelf)
        {
            GUI.Box(new Rect(Screen.width / 2 - 300, Screen.height - 60, 600, 50), "Press 'T' to PickUp the handle", gustyle);
        }
    }
}
