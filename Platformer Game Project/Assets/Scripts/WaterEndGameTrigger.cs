using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterEndGameTrigger : MonoBehaviour
{
    private Player _player;

    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            _player.GameOverForDead();
        }
        
    }
}
