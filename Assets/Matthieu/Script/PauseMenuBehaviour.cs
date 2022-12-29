using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuBehaviour : MonoBehaviour
{
    [SerializeField] PauseManager _manager;
    [SerializeField] AudioMixer _audioMixer;
    [SerializeField] GameObject _pauseMenu;
    [SerializeField] string[] _allowedScenes;

    bool _menuOn;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        GetComponentInChildren<Slider>().value = PlayerPrefs.GetFloat("MasterVolume");
        _manager.GamePaused = false;
        _manager.PauseMenu = gameObject;
        _pauseMenu.SetActive(false);
    }

    private void Start()
    {
        _audioMixer.SetFloat("MasterVolume", PlayerPrefs.GetFloat("MasterVolume"));      
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            for(int i = 0; i < _allowedScenes.Length; i++)
            {
                if (SceneManager.GetActiveScene().name == _allowedScenes[i])
                    TogglePauseMenu();
            }
    }

    public void TogglePauseMenu()
    {
        _menuOn = !_menuOn;
        _manager.GamePaused = _menuOn;
        _pauseMenu.SetActive(_menuOn);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("GameMenu");
    }
    
    public void QuitTheGame()
    {
        Application.Quit();
    }

    public void VolumeChange(float slider)
    {
        _audioMixer.SetFloat("MasterVolume", slider);
        PlayerPrefs.SetFloat("MasterVolume", slider);
    }
}
