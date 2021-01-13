using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputSingleton : Singleton<PlayerInputSingleton>
{
    public PlayerActions PlayerInputController { get; private set; }

    protected PlayerInputSingleton() { }

    private void Awake()
    {
        PlayerInputController = new PlayerActions();
    }

    private void OnEnable()
    {
        PlayerInputController.Enable();
    }

    private void OnDisable()
    {
        PlayerInputController.Enable();
    }
}
