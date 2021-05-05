using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Handle : MonoBehaviour
{
    public GameObject txtToDisplay;             //Display the information about how to close/open the door
    private bool playerInZone;
    private BoxCollider handleCollider;
    public DoorController DC;

    // Start is called before the first frame update
    void Start()
    {
        handleCollider = transform.gameObject.GetComponent<BoxCollider>();
        playerInZone = false;                   //Player not in zone
        txtToDisplay.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        txtToDisplay.SetActive(true);
        playerInZone = true;
    }

    private void OnTriggerExit(Collider other)
    {
        playerInZone = false;
        txtToDisplay.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInZone && this.gameObject.activeSelf)
        {
            txtToDisplay.GetComponent<Text>().text = "Press 'T' to PickUp the handle";
            handleCollider.enabled = true;
            Debug.Log("this.gameObject.activeSelf (active) take" + this.gameObject.activeSelf);
        }

        if (Input.GetKeyDown(KeyCode.T) && playerInZone)
        {
            Debug.Log("In Key");

            if (this.gameObject.activeSelf)
            {
                this.gameObject.SetActive(false);
                Debug.Log("this.gameObject.activeSelf (NOTactive)" + this.gameObject.activeSelf);
                playerInZone = false;
                txtToDisplay.SetActive(false);
                DC.gotKey = true;
            }

            else
            {
                this.gameObject.SetActive(true);
                Debug.Log("this.gameObject.activeSelf (active)" + this.gameObject.activeSelf);
                DC.gotKey = false;
            }

        }
    }
}
