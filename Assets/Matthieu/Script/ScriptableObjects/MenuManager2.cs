using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Menu/Manager", fileName ="MenuManager")]
public class MenuManager2 : ScriptableObject
{
    [Header("Copy / Paste Menu Scene Name under")]
    [SerializeField] string _menuSceneName;
    [Header("Copy / Paste Win Scene Name under")]
    [SerializeField] string _winSceneName;
    [Header("Copy / Paste Win Scene Name under")]
    [SerializeField] string _loseSceneName;
    [Header("Copy / Paste Loader Scene Name under")]
    [SerializeField] string _loaderSceneName;

    [Header("Copy / Paste all levels Scene Name under")]
    [SerializeField] string[] _levelScenes;

    [Header("Leave Blank")]
    [SerializeField] int _currentIndex;

    public string MenuSceneName { get => _menuSceneName; }
    public string WinSceneName { get => _winSceneName; }
    public string LoseSceneName { get => _loseSceneName; }
    public string[] Levelscenes { get => _levelScenes; }
    public int CurrentIndex { get => _currentIndex; set => _currentIndex = value; }
    public string LoaderSceneName { get => _loaderSceneName; }
}
