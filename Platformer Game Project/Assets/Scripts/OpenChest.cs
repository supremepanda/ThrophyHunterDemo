using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OpenChest : MonoBehaviour
{
    private Animator _anim;
    private Player _player;
    [SerializeField]
    private TMP_Text _warningText;
    private float _warningTextActiveTime;
    private bool _deactiveWarningText;
    private bool _chestOpened;

    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _winGame;

    private void Start()
    {
        _anim = GetComponent<Animator>();
        _player = GameObject.Find("Player").GetComponent<Player>();
        _warningTextActiveTime = 3f;
        _deactiveWarningText = false;
        _chestOpened = false;

        _audioSource = GameObject.Find("FxManager").GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (_deactiveWarningText)
        {
            _warningTextActiveTime -= Time.deltaTime;
            if(_warningTextActiveTime <= 0)
            {
                _deactiveWarningText = false;
                _warningText.gameObject.SetActive(false);
                _warningTextActiveTime = 3f;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_chestOpened)
        {
            if (collision.gameObject.tag == "Player")
            {
                if (_player.GetKeyAmount() == 3)
                {
                    _anim.SetTrigger("chestOpen");
                    _player.SetIsHaveThrophy(true);
                    StartCoroutine(WinGame());
                }
                else
                {
                    _warningText.gameObject.SetActive(true);
                    _warningText.text = "You should collect " + (3 - _player.GetKeyAmount()) + " more key!";
                    _deactiveWarningText = true;
                }
            }
        }
        
    }

    IEnumerator WinGame()
    {
        yield return new WaitForSeconds(1.5f);
        _audioSource.PlayOneShot(_winGame, 0.5f);
        _player.WinGame();
    }
}
