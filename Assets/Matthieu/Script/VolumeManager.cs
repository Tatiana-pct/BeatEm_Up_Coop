using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using Unity.VisualScripting;

public class VolumeManager : MonoBehaviour
{
    [SerializeField] AudioMixer _audioMixer;
    GameObject _btnON;
    GameObject _btnOFF;
    bool _volume = true;

    private void Start()
    {
        _btnON = transform.GetChild(0).gameObject;
        _btnOFF = transform.GetChild(1).gameObject;
        _volume = PlayerPrefs.GetInt("VolumeOn", 1) == 1;
        ToggleVolume();
        SceneManager.LoadScene("GameMenu");
    }
    public void VolumePressed()
    {
        _volume = !_volume;
        ToggleVolume();
    }

    private void ToggleVolume()
    {
        if (_volume)
        {
            _btnON.SetActive(true);
            _btnOFF.SetActive(false);
            _audioMixer.SetFloat("GlobalVolume", 0f);
        }
        else
        {
            _btnON.SetActive(false);
            _btnOFF.SetActive(true);
            _audioMixer.SetFloat("GlobalVolume", -80f);
        }
        PlayerPrefs.SetInt("VolumeOn", _volume ? 1 : 0);
    }
}
