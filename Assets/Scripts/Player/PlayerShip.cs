using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : MonoBehaviour
{
    private PlayerActions _playerInput;

    [SerializeField]
    private Gun _gun;

    private void OnEnable()
    {
        if (_playerInput == null)
        {
            _playerInput = new PlayerActions();
        }

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
}
