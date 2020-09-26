using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Movement and Jumping Values")]

    private Rigidbody2D _rb2d;                  // Rigidbody component of player
    private Animator _animator;                 // Animator component of player

    [SerializeField]                    
    private float _characterMovementSpeed;      // Movement speed on horizontal line

    private float _horizontal;                  // Input float value of "Horizontal"

    private Quaternion _leftRotation = new Quaternion(0, 0.5f, 0f, 0f);     // Left looking rotation
    private Quaternion _rightRotation = new Quaternion(0, 0f, 0f, 0f);      // Right looking rotation

    [SerializeField]
    private float _characterJumpingForce;       // Jumping force value on vertical line

    private bool _isJumping;                    // Boolean to check is character jumping
    private bool _isJumpingToUp;                // Boolean to check character falling animation

    [Header("Health")]

    [SerializeField]
    private int _health;

    private bool _canDamaged;

    [SerializeField]
    private GameObject[] _hearthImages = new GameObject[3];

    [Header("Coin")]
    
    private int _coinAmount;

    [SerializeField]
    private TMP_Text _coinText;

    [Header("Enemy")]

    [SerializeField]
    private float _reflectForcePower;

    private int _keyAmount;
    [SerializeField]
    private GameObject[] _keyImages = new GameObject[3];

    private bool _isHaveThrophy;
    [SerializeField]
    private GameObject _throphyImage;

    [SerializeField]
    private GameObject _level1UIManager;

    private AudioSource _fxAudioSource;
    [SerializeField]
    private AudioClip _jumpFx;
    
    private void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _isJumping = false;
        _isJumpingToUp = true;
        _health = 3;
        _canDamaged = true;
        _coinAmount = 0;
        _coinText.text = _coinAmount + "x";

        _keyAmount = 0;

        _fxAudioSource = GameObject.Find("FxManager").GetComponent<AudioSource>();
    }

    private void Update()
    {
        HorizontalMovement();
        Jump();
    }

    // <Movement>

    // Getter function of "_isJumping"
    public bool GetIsJumping()
    {
        return _isJumping;
    }

    // Setter function of "_isJumping"
    public void SetIsJumping(bool isJumping)
    {
        _isJumping = isJumping;
    }

    /// <summary>
    /// Horizontal movement function with animations
    /// </summary>
    private void HorizontalMovement()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");

        if (_horizontal > 0)
        {
            transform.rotation = _rightRotation;
            _animator.SetBool("isRunning", true);
        }
        else if (_horizontal < 0)
        {
            transform.rotation = _leftRotation;
            _animator.SetBool("isRunning", true);
        }
        else
        {
            _animator.SetBool("isRunning", false);
        }

        Vector3 movementVector = new Vector3(_horizontal, 0f, 0f);
        transform.position += movementVector * Time.deltaTime * _characterMovementSpeed;

    }

    /// <summary>
    /// Jumping function with animations
    /// </summary>
    private void Jump()
    {
        if (_isJumping)
        {
            if (_rb2d.velocity.y > 0)
            {
                _isJumpingToUp = true;
                _animator.SetBool("isJumpingToUp", _isJumpingToUp);
            }
            else
            {
                _isJumpingToUp = false;
                _animator.SetBool("isJumpingToUp", _isJumpingToUp);
            }
        }

        if (Input.GetButtonDown("Jump") && !_isJumping)
        {
            _fxAudioSource.PlayOneShot(_jumpFx, 0.3f);
            _rb2d.AddForce(new Vector2(0f, _characterJumpingForce), ForceMode2D.Impulse);
        }

    }

    // </Movement>

    // <Health>

    // Getter function of "_health"
    public int GetHealth()
    {
        return _health;
    }

    // Setter function of "_health"
    public void SetHealth(int health)
    {
        _health = health;
    }

    public void DamageHealth(int damage)
    {
        if (_canDamaged)
        {
            _health -= damage;
            if (_health < 0)
            {
                _health = 0;
                RemoveHeartFromUI(_health - 1);
            }
            else
            {
                RemoveHeartFromUI(_health - 1);
            }

            if(_health == 0)
            {
                GameOverForDead();
            }
        }
    }

    public void HealHealth(int healAmount)
    {
        _health += healAmount;
        if(_health > 3)
        {
            _health = 3;
        }
        ActiveHeartFromUI(_health);

    }

    private void RemoveHeartFromUI(int index)
    {
        for(int i = 2; i > index; i--)
        {
            _hearthImages[i].SetActive(false);
        }
    }

    private void ActiveHeartFromUI(int index)
    {
        for(int i = 0; i < index; i++)
        {
            _hearthImages[i].SetActive(true);
        }
    }

    // </Health>

    // <Coin>

    // Getter function for "_coinAmount"
    public int GetCoin()
    {
        return _coinAmount;
    }

    // Setter function for "_coinAmount"
    public void SetCoin(int amount)
    {
        _coinAmount = amount;
        _coinText.text = _coinAmount + "x";
    }

    public void AddCoin(int amount)
    {
        _coinAmount += amount;
        _coinText.text = _coinAmount + "x";
    }

    public int GetKeyAmount()
    {
        return _keyAmount;
    }

    public void SetKeyAmount(int amount)
    {
        _keyAmount = amount;
        ActiveKeyImages(_keyAmount);
    }

    public void AddKeyAmount(int amount)
    {
        _keyAmount += amount;
        ActiveKeyImages(_keyAmount);
    }

    private void ActiveKeyImages(int keyAmount)
    {
        for(int i = 0; i < keyAmount; i++)
        {
            _keyImages[i].GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
    }

    public bool GetIsHaveThrophy()
    {
        return _isHaveThrophy;
    }

    public void SetIsHaveThrophy(bool isHave)
    {
        _isHaveThrophy = true;
        if (_isHaveThrophy)
        {
            _throphyImage.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
    }

    public void GameOverForDead()
    {
        Debug.Log("Game over");
        _rb2d.gravityScale = 0;
        _rb2d.velocity = Vector2.zero;

        _level1UIManager.GetComponent<Level1UIManager>().OpenGameOverPanel();

        Time.timeScale = 0;
        
    }

    public void WinGame()
    {
        Debug.Log("You Win");
        _level1UIManager.GetComponent<Level1UIManager>().OpenWinGamePanel();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            if(transform.position.x <= collision.transform.position.x)
            {
                if(transform.position.y - 1.5f > collision.transform.position.y)
                {
                    _rb2d.AddForce(new Vector2(-1, 1) * _reflectForcePower, ForceMode2D.Impulse);
                }
                else
                {
                    _rb2d.AddForce(Vector2.left * _reflectForcePower, ForceMode2D.Impulse);
                }     
            }
            else 
            {
                if(transform.position.y - 1.5f > collision.transform.position.y)
                {
                    _rb2d.AddForce(new Vector2(1, 1) * _reflectForcePower, ForceMode2D.Impulse);
                }
                else
                {
                    _rb2d.AddForce(Vector2.right * _reflectForcePower, ForceMode2D.Impulse);
                }
                
            }

            DamageHealth(1);
            StartCoroutine(ImmortalTime());
        }
    }

    IEnumerator ImmortalTime()
    {
        _animator.SetBool("isDamaged", true);
        _canDamaged = false;
        yield return new WaitForSeconds(1f);
        _animator.SetBool("isDamaged", false);
        _canDamaged = true;
    }




}
