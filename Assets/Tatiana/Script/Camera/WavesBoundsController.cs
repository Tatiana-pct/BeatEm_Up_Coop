using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WavesBoundsController : MonoBehaviour
{
    [SerializeField] GameObject _bounds;
    [SerializeField] Transform _levelSpawnPoint;

    private WavesController _wavesController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            GetComponent<Collider2D>().enabled = false;

            GetComponent<WavesController>().IsTriggered = true;

            _levelSpawnPoint.transform.position = collision.transform.position;

            _bounds.gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        _wavesController = GetComponent<WavesController>();

        if (_wavesController.WaveWon )
            _bounds.gameObject.SetActive(false);
    }


};
