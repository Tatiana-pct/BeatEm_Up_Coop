using Cinemachine;
using System.Collections.Generic;
using UnityEngine;

public class WavesController : MonoBehaviour
{
    [SerializeField] WaveScriptableObject _waves;
    [SerializeField] Transform _instantiatePoint;
    [SerializeField] Transform _spawnPoint;
    [SerializeField] CinemachineVirtualCamera _mainCam;
    [SerializeField] CinemachineVirtualCamera _waveCam;
    bool _waveSpawned;
    bool _waveWon;
    bool overworldCamera = true;


    public bool IsTriggered { get; set; }
    public List<GameObject> EnnemiesGO { get; set; } = new List<GameObject>();
    public bool WaveWon { get => _waveWon; }

    private void Update()
    {

        if (IsTriggered)
        {
            IsTriggered = false;
            _waveSpawned = true;
            SwitchCamPriority();
            SpawnEnnemies();
        }

        if (_waveSpawned && !_waveWon && EnnemiesGO.Count == 0)
        {
            _waveWon = true;
            SwitchCamPriority();
            Debug.Log("Win");
        }
    }

    private void SpawnEnnemies()
    {
        for (int i = 0; i < _waves.EnnemyCount; i++)
        {
            int j = Random.Range(0, _waves.EnnemyTypes.Length);
            EnnemiesGO.Add(Instantiate(_waves.EnnemyTypes[j], _instantiatePoint.position, Quaternion.identity, transform));
            EnnemiesGO[i].GetComponent<EnnemyController>().FinalSpawnPoint = _spawnPoint;
        }
    }

    public void EnnemiKilled(GameObject ennemy)
    {
        EnnemiesGO.Remove(ennemy);
    }

    private void SwitchCamPriority()
    {
        if(overworldCamera)
        {
            _mainCam.Priority = 0;
            _waveCam.Priority = 1;
        }
        else
        {
            _mainCam.Priority = 1;
            _waveCam.Priority = 0;
        }
        overworldCamera = !overworldCamera;
    }
}
