﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private SpaceShip _owner;
    public SpaceShip GunOwner => _owner;
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

    public void SetOwner(SpaceShip owner)
    {
        _owner = owner;
    }

    private void Move()
    {
        var movementDelta = _speed * transform.up * Time.deltaTime;
        var newPosition = transform.position + movementDelta;
        _rigidbody.MovePosition(newPosition);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent<IDamagable>(out var damagable))
        {
            damagable.OnHit(this);
            Destroy(gameObject);
        }
    }
}
