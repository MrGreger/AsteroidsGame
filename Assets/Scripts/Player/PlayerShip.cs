using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerShip : SpaceShip, IDamagable
{
    public UnityEvent PlayerDied;
    private PlayerActions _playerInput;

    private void OnEnable()
    {
        _playerInput = PlayerInputSingleton.Instance.PlayerInputController;

        _playerInput.PlayerShip.Shoot.performed += ctx => OnShoot();
    }

    private void OnDisable()
    {
        _playerInput.PlayerShip.Shoot.performed -= ctx => OnShoot();
    }

    private void OnShoot()
    {
        _gun.Shoot();
    }

    public void OnHit(Bullet bullet)
    {
        PlayerDied?.Invoke();
    }

    public void OnPlayerDied()
    {
        Debug.Log("I'm died!");
    }
}
