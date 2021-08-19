using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class hasGlasses : MonoBehaviour
{
    public bool onGlasses;

    public Animator player;
    public Camera camera;

    public Camera minimap;
    public GameObject glassesOnPlayer;

    string _currentSelectedCharName;

    //public bool hasTheRightGlasses;
    //[SerializeField] Material color;
    //[SerializeField] private int blure;
    //[SerializeField] GameObject bodyGlasses;
    //[SerializeField]  glassesManager gm;
    //[SerializeField]  Color initColor;
    //private Material[] matArr;

    void OnTriggerEnter(Collider other)
    {
        if (_currentSelectedCharName == "Blindness") onGlasses = true;
    }

    void OnTriggerExit(Collider other)
    {
        onGlasses = false;
    }

    void Start()
    {
        _currentSelectedCharName = PlayerPrefs.GetString("CurrentSelectedCharacter", "Deaf");

        if (_currentSelectedCharName == "Blindness" || _currentSelectedCharName == "Deaf")
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        }

        //color.color = initColor;
        //matArr = bodyGlasses.GetComponent<MeshRenderer>().materials;
        //matArr[0].color = initColor;
        //matArr[1].color = initColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (onGlasses && Input.GetKeyDown(KeyCode.E))
        {
            camera.gameObject.SetActive(true);
            PostProcessLayer layer = camera.GetComponent<PostProcessLayer>();
            layer.enabled = false;
            PostProcessLayer mmlayer = minimap.GetComponent<PostProcessLayer>();
            mmlayer.enabled = false;
            this.gameObject.SetActive(false);
            glassesOnPlayer.SetActive(true);
            // if (player) player.Play("Lifting");
        }
    }

    void OnGUI()
    {
        GUIStyle gustyle = new GUIStyle(GUI.skin.box);
        gustyle.fontSize = 40;
        if (onGlasses)
        {
            //if (gm.isFirst)
            //{
            //    GUI.Box(new Rect(Screen.width / 2 - 300, Screen.height - 60, 600, 50), "Press E to Get the glasses", gustyle);
            //}
            //else
            //{
            GUI.Box(new Rect(Screen.width / 2 - 300, Screen.height - 60, 600, 50), "Press E to Get the glasses", gustyle);
            //}
        }
    }
}