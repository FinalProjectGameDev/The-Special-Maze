using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicAfterPressButton : MonoBehaviour
{

    [SerializeField]
    public AudioSource[] sound;

    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(RoutineWrap);
    }
    public void RoutineWrap()
    {
        StartCoroutine(playSound());
    }
    IEnumerator playSound()
    {
        for(int i = 0; i<sound.Length; i++)
        {
            sound[i].Play();
            yield return new WaitForSeconds(sound[i].clip.length);
        }
    }
}
