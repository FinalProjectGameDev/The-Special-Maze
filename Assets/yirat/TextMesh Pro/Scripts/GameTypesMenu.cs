using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameTypesMenu : MonoBehaviour
{
    public void PlayDemoGame()
    {
        SceneManager.LoadScene(2);
        Debug.Log("DemoGame!");
    }
}
