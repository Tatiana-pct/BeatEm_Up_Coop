using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PauseManager")]
public class PauseManager : ScriptableObject
{
    [Header("Don't Populate this field, it's just a Showcase")]
    [SerializeField] GameObject _pauseMenu;
    bool _gamePause;


    public GameObject PauseMenu { get => _pauseMenu; set => _pauseMenu = value; }
    public bool GamePaused { get => _gamePause; set => _gamePause = value; }
}
