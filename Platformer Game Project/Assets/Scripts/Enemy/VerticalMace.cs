using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMace : MonoBehaviour
{
    [SerializeField]
    private float _minY;

    [SerializeField]
    private bool isMinYDefaultPos;

    [SerializeField]
    private float _maxY;

    [SerializeField]
    private float _patrolSpeed;

    private GameObject _minTriggerObject;
    private GameObject _maxTriggerObject;

    private bool _movementUp;
    void Start()
    {
        if (!isMinYDefaultPos)
        {
            transform.position = new Vector2(transform.position.x, _minY);
        }
        else
        {
            _minY = transform.position.y - 1.8f;
        }
       
        _movementUp = true;

        _minTriggerObject = new GameObject();
        //_minTriggerObject.transform.SetParent(transform);
        _minTriggerObject.transform.position = new Vector2(transform.position.x, _minY);
        _minTriggerObject.tag = "maceTrigger";
        _minTriggerObject.AddComponent<BoxCollider2D>();
        _minTriggerObject.GetComponent<BoxCollider2D>().isTrigger = true;
        _minTriggerObject.GetComponent<BoxCollider2D>().size = new Vector2(1, 1);

        _maxTriggerObject = new GameObject();
        //_maxTriggerObject.transform.SetParent(transform);
        _maxTriggerObject.transform.position = new Vector2(transform.position.x, transform.position.y + (_maxY - _minY));
        _maxTriggerObject.tag = "maceTrigger";
        _maxTriggerObject.AddComponent<BoxCollider2D>();
        _maxTriggerObject.GetComponent<BoxCollider2D>().isTrigger = true;
        _maxTriggerObject.GetComponent<BoxCollider2D>().size = new Vector2(1, 1);

    }

    // Update is called once per frame
    void Update()
    {
        if (_movementUp)
        {
            transform.position -= Vector3.up * _patrolSpeed * Time.deltaTime;
        }
        else
        {
            transform.position += Vector3.up * _patrolSpeed * Time.deltaTime;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "maceTrigger")
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
