using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    [Range(1f, 10)]
    private float _speed = 1;
    [Range(1f, 10)]
    [SerializeField]
    private float _lifeTime = 4;
    [SerializeField]
    private Rigidbody2D _rigidbody;

    private void Start()
    {
        Destroy(gameObject, _lifeTime);
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        var movementDelta = _speed * transform.up * Time.deltaTime;
        var newPosition = transform.position + movementDelta;
        _rigidbody.MovePosition(newPosition);
    }
}
