using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DogController : MonoBehaviour
{
    //public Camera cam;

    public NavMeshAgent agent;
    public Animator anim;
    public Transform player;
    public bool seeing;

    public Transform[] targets;

    public static int currTarget = 0;

    public bool onDog; //Change to private
    private bool activated = false;

    //public float dogUp = 0.7f;
    //public float playerUp = 0.9f;

    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player").transform;
        // Debug.Log(player);
        seeing = false;
    }

    void OnTriggerEnter(Collider other)
    {
        onDog = true;
        anim.SetBool("Found", true);
        this.GetComponent<AudioSource>().Stop();
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
                //agent.SetDestination(new Vector3(-47.5f,-1,-2));
                agent.SetDestination(targets[currTarget].position);
                // Debug.Log("E");
                activated = true;
            }
        }
        if (player)
        {
            RaycastHit hit;
            var rayDirection = (player.position + new Vector3(0, 0.5f, 0)) - (transform.position + new Vector3(0, 0.7f, 0));
            if (Physics.Raycast(transform.position + new Vector3(0, 0.7f, 0), rayDirection, out hit))
            {
                Debug.DrawLine(transform.position + new Vector3(0, 0.7f, 0), hit.point);
                if (hit.transform.tag == "Player")
                {

                    // enemy can see the player!
                    agent.isStopped = false;
                    seeing = true;
                }
                else
                {
                    //    Debug.Log(hit.transform);

                    // there is something obstructing the view
                    agent.isStopped = true;
                    seeing = false;
                }
            }
            //    else {
            //        agent.isStopped = true;
            //            seeing = false;
            //    }
        }

    }

    public IEnumerator nextDestination()
    {
        yield return new WaitForSeconds(3);
        currTarget++;
        agent.ResetPath();
        //agent.SetDestination(new Vector3(-47.5f,-1,-2));
        agent.SetDestination(targets[currTarget].position);
        agent.isStopped = false;
    }

    void OnGUI()
    {
        GUIStyle gustyle = new GUIStyle(GUI.skin.box);
        gustyle.fontSize = 40;
        if (onDog && !activated && agent.velocity == Vector3.zero)
        {
            GUI.Box(new Rect(Screen.width / 2 - 300, Screen.height - 60, 600, 50), "Press E to Use the Guide Dog", gustyle);
        }
    }

}
