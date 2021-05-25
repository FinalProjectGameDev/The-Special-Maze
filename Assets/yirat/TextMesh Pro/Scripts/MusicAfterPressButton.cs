using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicAfterPressButton : MonoBehaviour
{
    public AudioSource firstNote;
    public AudioSource secondNote;
    public AudioSource thirdNote;
    public AudioSource fourthNote;

    public void RoutineWrap()
    {
        StartCoroutine(playSound());
    }
    IEnumerator playSound()
    {
        firstNote.Play();
        yield return new WaitForSeconds(firstNote.clip.length);
        secondNote.Play();
        yield return new WaitForSeconds(secondNote.clip.length);
        thirdNote.Play();
        yield return new WaitForSeconds(thirdNote.clip.length);
        fourthNote.Play();
    }
}
