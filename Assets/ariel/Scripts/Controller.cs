using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Controller : MonoBehaviour
{
    //public Camera cam;

    public NavMeshAgent agent;
    public Animator anim;
    public Transform player;

    //public Transform target;

    public bool onDog; //Change to private

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void OnTriggerEnter(Collider other)
    {
        onDog = true;
        anim.SetBool("Found", true);
    }

    void OnTriggerExit(Collider other)
    {
        onDog = false;
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("Move", agent.velocity.magnitude);
        if (onDog)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                agent.ResetPath();
                agent.SetDestination(new Vector3(-47.5f,-1,-2));
            }
        }
        if (player)
        {
            RaycastHit hit;
            var rayDirection = player.position - transform.position;
            if (Physics.Raycast(transform.position, rayDirection, out hit))
            {
                if (hit.transform == player)
                {
                    // enemy can see the player!
                    agent.isStopped = false;
                }
                else
                {
                    // there is something obstructing the view
                    agent.isStopped = true;
                }
            }
        }
        
    }

    void OnGUI()
    {
        GUIStyle gustyle = new GUIStyle(GUI.skin.box);
        gustyle.fontSize = 20;
        if (onDog && agent.velocity == Vector3.zero)
        {
            GUI.Box(new Rect(Screen.width / 2 - 150, Screen.height - 40, 300, 30), "Press E to Get the switch", gustyle);
        }
    }

}
