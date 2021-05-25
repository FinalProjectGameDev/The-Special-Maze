using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayer : MonoBehaviour
{

    string _currentSelectedCharName;
    public GameObject toLoad;

    // Start is called before the first frame update
    void Start()
    {
        _currentSelectedCharName = PlayerPrefs.GetString("CurrentSelectedCharacter", "Robot");

        switch (_currentSelectedCharName)
        {
            case "Robot":
                toLoad.GetComponent<Renderer>().material.color = Color.red;
                toLoad.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.red);
                break;
            case "Ethan":
                toLoad.GetComponent<Renderer>().material.color = Color.blue;
                toLoad.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.blue);
                break;
            case "Low Poly Char":
                toLoad.GetComponent<Renderer>().material.color = Color.green;
                toLoad.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.green);
                break;
            case "Test":
                toLoad.GetComponent<Renderer>().material.color = Color.cyan;
                toLoad.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.cyan);
                break;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
