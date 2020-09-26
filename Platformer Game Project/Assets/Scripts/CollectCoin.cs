using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCoin : MonoBehaviour
{
    private AudioSource _audioSource;

    [SerializeField]
    private AudioClip _collectCoin;

    private void Start()
    {
        _audioSource = GameObject.Find("FxManager").GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            GameObject.Find("Player").GetComponent<Player>().AddCoin(1);
            _audioSource.PlayOneShot(_collectCoin, 0.3f);
            Destroy(gameObject);
        }
    }
}
