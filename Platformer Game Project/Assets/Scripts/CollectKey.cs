using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectKey : MonoBehaviour
{
    private Player _player;
    private AudioSource _audioSource;

    [SerializeField]
    private AudioClip _collectKey;

    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _audioSource = GameObject.Find("FxManager").GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            _player.AddKeyAmount(1);
            _audioSource.PlayOneShot(_collectKey, 0.3f);
            Destroy(gameObject);
        }
    }
}
