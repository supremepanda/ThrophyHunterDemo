using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private Player _player;
    private Animator _animator;
    private Rigidbody2D _velocityY;

    private bool _exitEnabled;

    private void Start()
    {
        _player = transform.GetComponentInParent<Player>();
        _animator = transform.GetComponentInParent<Animator>();
        _velocityY = _player.gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_exitEnabled)
        {
            Debug.Log("ExitEnabled");
            Debug.Log(_velocityY.velocity.y);
            if(!(_velocityY.velocity.y < 3f && _velocityY.velocity.y > -3f))
            {
                Debug.Log("Başarılı");
                _player.SetIsJumping(true);
                _animator.SetBool("isJumping", true);
                _exitEnabled = false;
            }
        }
    }

    // If the player hit the ground, the player can jump
    private void OnCollisionEnter2D(Collision2D collision)
     {
         if(collision.gameObject.tag == "Ground")
         {
             Debug.Log("Enter");
             _player.SetIsJumping(false);
             _animator.SetBool("isJumping", false);
         }

     }

     // If the player exit the ground, the player can not jump
     private void OnCollisionExit2D(Collision2D collision)
     {
         if(collision.gameObject.tag == "Ground")
         {
             Debug.Log("Exit");
            _exitEnabled = true;
        }
     }


}
