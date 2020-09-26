using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PikeDown : MonoBehaviour
{
    private Rigidbody2D _rb2d;
    private GameObject _player;

    [SerializeField]
    private float _destroyTime;

    private bool _isDestroyable;

    private void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _player = GameObject.Find("Player");
        _isDestroyable = false;
    }

    private void Update()
    {
        if(transform.position.x - _player.transform.position.x <= 1.4f)
        {
            _rb2d.gravityScale = 1;
        }

        if (_isDestroyable)
        {
            _destroyTime -= Time.deltaTime;
            if (_destroyTime <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Player")
        {
            _isDestroyable = true;
        }
    }




}
