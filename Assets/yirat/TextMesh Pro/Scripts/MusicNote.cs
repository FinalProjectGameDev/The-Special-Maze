using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicNote : MonoBehaviour
{
    [SerializeField] 
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(notePlay);
    }
    public void notePlay()
    {
        audioSource.Play();
    }
}
