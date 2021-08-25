using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicNote : MonoBehaviour
{
    [SerializeField] 
    AudioSource audioSource;
    [SerializeField]
    PianoDoorController pianoDoorController;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(RoutineWrap);
    }
    public void RoutineWrap()
    {
        StartCoroutine(notePlay());
    }

    IEnumerator notePlay()
    {
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);
        pianoDoorController.setNotes(audioSource);
    }
}
