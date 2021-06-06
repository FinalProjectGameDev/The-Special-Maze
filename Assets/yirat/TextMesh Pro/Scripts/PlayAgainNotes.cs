using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayAgainNotes : MonoBehaviour
{

    // Start is called before the first frame update
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
        for (int i = 0; i < MusicAfterPressButton.fourSounds.Length; i++)
        {
            MusicAfterPressButton.fourSounds[i].Play();
            yield return new WaitForSeconds(MusicAfterPressButton.fourSounds[i].clip.length);
        }
    }
}
