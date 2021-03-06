﻿using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerShip : SpaceShip, ICollidable, IRemovable
{
    public UnityEvent PlayerDied;
    private PlayerActions _playerInput => PlayerInputSingleton.Instance?.PlayerInputController;

    private void OnEnable()
    {
        _playerInput.PlayerShip.Shoot.performed += ctx => OnShoot();
    }

    private void OnDisable()
    {
        if (_playerInput != null)
        {
            _playerInput.PlayerShip.Shoot.performed -= ctx => OnShoot();
        }
    }

    private void OnShoot()
    {
        _gun.Shoot();
    }

    public void OnCollided(Bullet bullet)
    {
        PlayerDied?.Invoke();
    }

    public void OnPlayerDied()
    {
        MessageBroker.Default.Publish(new OnExplodeEvent(transform.position));
        MessageBroker.Default.Publish(new OnPlayerDiedEvent());
    }

    public void OnCollided(Asteroid asteroid)
    {
        OnPlayerDied();
    }

    public void OnCollided(SpaceShip spaceShip)
    {
        OnPlayerDied();
    }

    public void Remove()
    {
        transform.position = Vector3.zero;
    }
}
