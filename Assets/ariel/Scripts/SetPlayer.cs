using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine;

public class SetPlayer : MonoBehaviour
{

    public string _currentSelectedCharName;
    //public GameObject toLoad;

    public Camera cam;
    public Camera focuscam;
    public Camera camWheel;
    public Camera minimap;

    public GameObject Deaf;
    public GameObject Parkinson;
    public GameObject Blindness;
    public GameObject Wheelchair;

    public GameObject stepToHide;
    // public GameObject stepToHide2;

    // Start is called before the first frame update
    void Start()
    {
        _currentSelectedCharName = PlayerPrefs.GetString("CurrentSelectedCharacter", "Deaf");
        Transform mm_player = minimap.GetComponent<Minimap>().player;
        minimap.cullingMask &= ~(1 << LayerMask.NameToLayer("sphireMinimapD"));
        minimap.cullingMask &= ~(1 << LayerMask.NameToLayer("sphireMinimapB"));


        switch (_currentSelectedCharName)
        {
            case "Deaf":
                Deaf.SetActive(true);
                cam.gameObject.SetActive(true);
                minimap.GetComponent<Minimap>().player = Deaf.transform;
                minimap.cullingMask |= 1 << LayerMask.NameToLayer("sphireMinimapD");
                Deaf.GetComponent<AudioListener>().enabled = false;
                break;
            case "Parkinson":
                Parkinson.SetActive(true);
                cam.gameObject.SetActive(true);
                minimap.GetComponent<Minimap>().player = Parkinson.transform;
                Parkinson.GetComponent<AudioListener>().enabled = true;
                break;
            case "Blindness":
                Blindness.SetActive(true);
                cam.gameObject.SetActive(true);
                PostProcessLayer layer = cam.GetComponent<PostProcessLayer>();
                layer.enabled = true;
                PostProcessLayer mmlayer = minimap.GetComponent<PostProcessLayer>();
                mmlayer.enabled = true;
                minimap.GetComponent<Minimap>().player = Blindness.transform;
                minimap.cullingMask |= 1 << LayerMask.NameToLayer("sphireMinimapB");
                Blindness.GetComponent<AudioListener>().enabled = true;
                break;
            case "Wheelchair":
                Wheelchair.SetActive(true);
                camWheel.gameObject.SetActive(true);
                minimap.GetComponent<Minimap>().player = Wheelchair.transform;
                Wheelchair.GetComponent<AudioListener>().enabled = true;
                stepToHide.SetActive(false);
                // stepToHide2.SetActive(false);
                break;
        }
        focuscam.gameObject.SetActive(true);

    }


}
