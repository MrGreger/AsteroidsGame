using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class UISwitcher : MonoBehaviour
{
    public GameObject MenuUI;
    public GameObject PauseUI;
    public GameObject GameUI;

    public void Start()
    {
        MessageBroker.Default.Receive<OnGameStartedEvent>()
                             .Subscribe(OnGameStarted)
                             .AddTo(this);

        MessageBroker.Default.Receive<OnGameOverEvent>()
                             .Subscribe(OnGameOver)
                             .AddTo(this);

        MessageBroker.Default.Receive<OnGamePausedEvent>()
                             .Subscribe(OnGamePaused)
                             .AddTo(this);

        MessageBroker.Default.Receive<OnGameResumedEvent>()
                             .Subscribe(OnGameResumed)
                             .AddTo(this);
    }

    private void OnGameResumed(OnGameResumedEvent obj)
    {
        PauseUI.SetActive(false);
        GameUI.SetActive(true);
    }

    private void OnGamePaused(OnGamePausedEvent obj)
    {
        PauseUI.SetActive(true);
        GameUI.SetActive(false);
    }

    private void OnGameOver(OnGameOverEvent obj)
    {
        MenuUI.SetActive(true);
        GameUI.SetActive(false);
    }

    private void OnGameStarted(OnGameStartedEvent obj)
    {
        MenuUI.SetActive(false);
        GameUI.SetActive(true);
    }
}
