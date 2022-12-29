using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScript : MonoBehaviour
{
    public void Replay()
    {
        Debug.Log("Replay btn");
        SceneManager.LoadScene("Menu");
    }

    public void Quit()
    {
        Debug.Log("Quitter");
        Application.Quit();
    }
}
