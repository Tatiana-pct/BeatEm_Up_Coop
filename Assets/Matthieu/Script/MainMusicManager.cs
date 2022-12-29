using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMusicManager : MonoBehaviour
{
    [SerializeField] string[] _scenesMainMusic;
    AudioSource _audioSource;
    bool _canPlay;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        _audioSource = GetComponent<AudioSource>();
        _canPlay = false;
        for (int i = 0; i < _scenesMainMusic.Length; i++)
        {
            if (SceneManager.GetActiveScene().name == _scenesMainMusic[i])
            {
                _canPlay = true;
                break;
            }
        }

        MuteToggle();
    }

    public void MuteToggle()
    {
        if (_canPlay)
            _audioSource.mute = false;
        else
            _audioSource.mute = true;
    }
}
