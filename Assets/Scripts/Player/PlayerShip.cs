using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : MonoBehaviour
public class PlayerShip : SpaceShip
{
    private PlayerActions _playerInput;

    [SerializeField]
    private Gun _gun;

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
}
