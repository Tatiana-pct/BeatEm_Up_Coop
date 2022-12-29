using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] MenuManager2 _menuManager;

    public void Quit()
    {
        Application.Quit();
    }

    public void Continue()
    {
        SceneManager.LoadScene(_menuManager.MenuSceneName);
    }

    public void Play()
    {
        _menuManager.CurrentIndex = 0;
        SceneManager.LoadScene(_menuManager.LoaderSceneName);
    }
}
