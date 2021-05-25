using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Controller : MonoBehaviour
{
    //public Camera cam;

    public NavMeshAgent agent;

    //public Transform target;

    public bool onDog; //Change to private

    void OnTriggerEnter(Collider other)
    {
        onDog = true;
    }

    void OnTriggerExit(Collider other)
    {
        onDog = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (onDog)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                //onDog = false;
                agent.SetDestination(new Vector3(-33,0,3.8f));
                //this.gameObject.SetActive(false);
                //Box.GetComponent<LightSwitch>().hasPin = true;
                //if (Box.GetComponent<LightSwitch>().LastSwitch != null) Box.GetComponent<LightSwitch>().LastSwitch.SetActive(true);
                //Box.GetComponent<LightSwitch>().LastSwitch = this.gameObject;
                //backgroundSound.Pause();
                //foundBox.Play();
                //soundImage.SetActive(true);
            }
        }
    }

    void OnGUI()
    {
        GUIStyle gustyle = new GUIStyle(GUI.skin.box);
        gustyle.fontSize = 20;
        if (onDog)
        {
            GUI.Box(new Rect(Screen.width / 2 - 150, Screen.height - 40, 300, 30), "Press E to Get the switch", gustyle);
        }
    }

    //// Update is called once per frame
    //void Update()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
    //        RaycastHit hit;

    //        if(Physics.Raycast(ray,out hit))
    //        {
    //            agent.SetDestination(hit.point);
    //        }
    //    }
    //}


}
