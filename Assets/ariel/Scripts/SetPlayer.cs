using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayer : MonoBehaviour
{

    string _currentSelectedCharName;
    //public GameObject toLoad;

    public Camera cam;

    public GameObject Robot;
    public GameObject Ethan;
    public GameObject Poly;
    public GameObject Test;

    // Start is called before the first frame update
    void Start()
    {
        _currentSelectedCharName = PlayerPrefs.GetString("CurrentSelectedCharacter", "Robot");

        switch (_currentSelectedCharName)
        {
            case "Robot":
                Robot.SetActive(true);
                //toLoad.GetComponent<Renderer>().material.color = Color.red;
                //toLoad.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.red);
                break;
            case "Ethan":
                Ethan.SetActive(true);

                //toLoad.GetComponent<Renderer>().material.color = Color.blue;
                //toLoad.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.blue);
                break;
            case "Low Poly Char":
                Poly.SetActive(true);

                //toLoad.GetComponent<Renderer>().material.color = Color.green;
                //toLoad.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.green);
                break;
            case "Test":
                Test.SetActive(true);

                //toLoad.GetComponent<Renderer>().material.color = Color.cyan;
                //toLoad.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.cyan);
                break;
        }
        cam.gameObject.SetActive(true);
        //cam.clearFlags = CameraClearFlags.Nothing;
        //StartCoroutine(ExampleCoroutine());
    }

    // Update is called once per frame
    //IEnumerator ExampleCoroutine()
    //{
        ///yield return new WaitForSeconds(2);
        //cam.clearFlags = CameraClearFlags.Depth;
        //cam.cullingMask = (1 << LayerMask.NameToLayer("Player"));
        
    //}
}
