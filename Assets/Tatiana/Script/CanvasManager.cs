using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] PlayerManager _playerManager;
    [SerializeField] TMP_Text _score;

    [SerializeField] TMP_Text _health;
    [SerializeField] TMP_Text _life;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _score.text = $"Score : {_playerManager.Score}";

        _health.text = $"Health : {_playerManager.CurrentHealth}";

        _life.text = $"Life : {_playerManager.CurrentLifeCount}";
    }
}
