using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsManager : MonoBehaviour
{
    [SerializeField] MenuManager2 _menuManager;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _menuManager.CurrentIndex++;
            if (_menuManager.CurrentIndex < _menuManager.Levelscenes.Length)
            {
                SceneManager.LoadScene(_menuManager.Levelscenes[_menuManager.CurrentIndex]);
            }
            else
            {
                SceneManager.LoadScene(_menuManager.WinSceneName);
            }
        }
    }

    public void Death()
    {
        SceneManager.LoadScene(_menuManager.LoseSceneName);
    }
}
