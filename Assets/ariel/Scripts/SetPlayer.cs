using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine;

public class SetPlayer : MonoBehaviour
{

    string _currentSelectedCharName;
    //public GameObject toLoad;

    public Camera cam;
    public Camera focuscam;
    public Camera camWheel;
    public Camera minimap;

    public GameObject Deaf;
    public GameObject Parkinson;
    public GameObject Blindness;
    public GameObject Wheelchair;

    // Start is called before the first frame update
    void Start()
    {
        _currentSelectedCharName = PlayerPrefs.GetString("CurrentSelectedCharacter", "Deaf");
        Transform mm_player = minimap.GetComponent<Minimap>().player;

        switch (_currentSelectedCharName)
        {
            case "Deaf":
                Deaf.SetActive(true);
                cam.gameObject.SetActive(true);
                minimap.GetComponent<Minimap>().player = Deaf.transform;
                //focuscam.GetComponent<AudioListener>().enabled = false;
                Deaf.GetComponent<AudioListener>().enabled = false;
                break;
            case "Parkinson":
                Parkinson.SetActive(true);
                cam.gameObject.SetActive(true);
                minimap.GetComponent<Minimap>().player = Parkinson.transform;
                //focuscam.GetComponent<AudioListener>().enabled = true;
                Parkinson.GetComponent<AudioListener>().enabled = true;
                break;
            case "Blindness":
                Blindness.SetActive(true);
                cam.gameObject.SetActive(true);
                PostProcessLayer layer = cam.GetComponent<PostProcessLayer>();
                layer.enabled = true; 
                //PostProcessLayer mmlayer = minimap.GetComponent<PostProcessLayer>();
               // mmlayer.enabled = true;
                minimap.GetComponent<Minimap>().player = Blindness.transform;
                //focuscam.GetComponent<AudioListener>().enabled = true;
                Blindness.GetComponent<AudioListener>().enabled = true;
                break;
            case "Wheelchair":
                Wheelchair.SetActive(true);
                camWheel.gameObject.SetActive(true);
                minimap.GetComponent<Minimap>().player = Wheelchair.transform;
                //focuscam.GetComponent<AudioListener>().enabled = true;
                Wheelchair.GetComponent<AudioListener>().enabled = true;
                break;
        }
        focuscam.gameObject.SetActive(true);

    }


}
