using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleController : MonoBehaviour
{

    [SerializeField] CollectiblesManager _collectibleManager;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") )
        {

            PlayerControls player = collision.GetComponent<PlayerControls>();
            player.DoLoot(_collectibleManager);

            Destroy(gameObject);
        }
    }
}
