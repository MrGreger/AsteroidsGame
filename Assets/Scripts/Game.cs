using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

enum GameState
{
    playing,
    paused,
    stopped
}

public class Game : MonoBehaviour
{
    public UnityEvent OnDied;

    [SerializeField]
    private PlayerShip _player;

    private GameState _gameState = GameState.stopped;

    private void Start()
    {
        MessageBroker.Default.Receive<OnPlayerDiedEvent>()
                             .Subscribe(x =>
                             {
                                 OnPlayerDied();
                             });

        PlayerInputSingleton.Instance.PlayerInputController.Game.Pause.performed += OnPausePress;
    }

    private void OnPausePress(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (_gameState == GameState.paused)
        {
            StartGame();
        }
        if (_gameState == GameState.playing)
        {
            PauseGame();
        }
    }

    public void StartGame()
    {
        if (_gameState == GameState.paused)
        {
            ResumeGame();
            return;
        }

        _gameState = GameState.playing;

        Time.timeScale = 1;

        _player.gameObject.SetActive(true);
        _player.transform.position = Vector3.zero;

        RemoveAllRemovables();
        RestartGenerators();

        MessageBroker.Default.Publish(new OnGameStartedEvent());
    }

    private void ResumeGame()
    {
        Time.timeScale = 1;
        _gameState = GameState.playing;
        MessageBroker.Default.Publish(new OnGameResumedEvent());
    }

    public void PauseGame()
    {
        _gameState = GameState.paused;
        Time.timeScale = 0;
        MessageBroker.Default.Publish(new OnGamePausedEvent());
    }

    private void RestartGenerators()
    {
        var generators = FindObjectsOfType<MonoBehaviour>().OfType<IGenerator>();

        foreach (var generator in generators)
        {
            generator.StopGenerating();
            generator.StartGenerating();
        }
    }

    private void RemoveAllRemovables()
    {
        var removables = FindObjectsOfType<MonoBehaviour>().OfType<IRemovable>();

        foreach (var removable in removables)
        {
            removable.Remove();
        }
    }

    private void OnPlayerDied()
    {
        _gameState = GameState.stopped;

        Time.timeScale = 0;
        OnDied?.Invoke();

        MessageBroker.Default.Publish(new OnGameOverEvent());
    }

    public void Quit()
    {
        Application.Quit();
    }
}

