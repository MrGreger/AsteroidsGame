using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerShip : SpaceShip, ICollidable
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

    public void OnCollided(Bullet bullet)
    {
        PlayerDied?.Invoke();
    }

    public void OnPlayerDied()
    {
        Debug.Log("I'm died!");
    }

    public void OnCollided(Asteroid asteroid)
    {
        OnPlayerDied();
    }

    public void OnCollided(SpaceShip spaceShip)
    {
        throw new System.NotImplementedException();
    }
}
