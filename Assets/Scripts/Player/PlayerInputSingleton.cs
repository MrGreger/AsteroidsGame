using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public PlayerActions PlayerInputController { get; private set; }

    public static PlayerInput Instance { get; private set; } = null;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            PlayerInputController = new PlayerActions();
        }
        else if (Instance == this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
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
