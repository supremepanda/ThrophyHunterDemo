using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSaw : MonoBehaviour
{

    [SerializeField]
    private float _minX;

    [SerializeField]
    private bool isMinXDefaultPos;

    [SerializeField]
    private float _maxX;

    [SerializeField]
    private float _patrolSpeed;

    private GameObject _minTriggerObject;
    private GameObject _maxTriggerObject;

    private bool _movementUp;
    void Start()
    {
        if (!isMinXDefaultPos)
        {
            transform.position = new Vector2(_minX, transform.position.y);
        }
        else
        {
            _minX = transform.position.x - 1.8f;
        }

        _movementUp = true;

        _minTriggerObject = new GameObject();
        //_minTriggerObject.transform.SetParent(transform);
        _minTriggerObject.transform.position = new Vector2(_minX, transform.position.y + 0.5f);
        _minTriggerObject.tag = "sawTrigger";
        _minTriggerObject.AddComponent<BoxCollider2D>();
        _minTriggerObject.GetComponent<BoxCollider2D>().isTrigger = true;
        _minTriggerObject.GetComponent<BoxCollider2D>().size = new Vector2(1, 1);

        _maxTriggerObject = new GameObject();
        //_maxTriggerObject.transform.SetParent(transform);
        _maxTriggerObject.transform.position = new Vector2(transform.position.x + (_maxX - _minX), transform.position.y + 0.5f);
        _maxTriggerObject.tag = "sawTrigger";
        _maxTriggerObject.AddComponent<BoxCollider2D>();
        _maxTriggerObject.GetComponent<BoxCollider2D>().isTrigger = true;
        _maxTriggerObject.GetComponent<BoxCollider2D>().size = new Vector2(1, 1);

    }

    // Update is called once per frame
    void Update()
    {
        if (_movementUp)
        {
            transform.position += Vector3.right * _patrolSpeed * Time.deltaTime;
        }
        else
        {
            transform.position -= Vector3.right * _patrolSpeed * Time.deltaTime;
        }

        transform.Rotate(0f, 0f, -80f * Time.deltaTime);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("trigger!");
        if (collision.gameObject.tag == "sawTrigger")
        {
            ChangeMovementDirection();
        }
    }

    private void ChangeMovementDirection()
    {
        if (_movementUp)
        {
            _movementUp = false;
        }
        else
        {
            _movementUp = true;
        }
    }
}
