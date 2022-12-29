using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameInitializer : MonoBehaviour
{
    [SerializeField] PlayerManager _playerManager;

    [SerializeField] MenuManager2 _menuManager;

    private void Awake()
    {
        _playerManager.CurrentHealth = _playerManager.MaxHealth;
        _playerManager.CurrentLifeCount = _playerManager.MaxLifeCount;
        _playerManager.Score = 0;
    }

    private void Start()
    {
        SceneManager.LoadScene(_menuManager.Levelscenes[_menuManager.CurrentIndex]);
    }
}
