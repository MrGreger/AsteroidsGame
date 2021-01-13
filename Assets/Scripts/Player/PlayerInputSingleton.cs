using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputSingleton : Singleton<PlayerInputSingleton>
{
    private PlayerActions _playerInputController;

    public PlayerActions PlayerInputController
    {
        get
        {
            if(_playerInputController == null)
            {
                _playerInputController = new PlayerActions();
            }

            return _playerInputController;
        }
    }

    protected PlayerInputSingleton() { }

    private void OnEnable()
    {
        PlayerInputController.Enable();
    }

    private void OnDisable()
    {
        PlayerInputController.Disable();
    }
}
