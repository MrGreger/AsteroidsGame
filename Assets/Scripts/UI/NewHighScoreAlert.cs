using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;

public class NewHighScoreAlert : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _alertText;
    [SerializeField]
    private GameObject _alertGameObject;

    public void Start()
    {
        MessageBroker.Default.Receive<OnNewHighScoreEvent>()
                             .Subscribe(OnNewHighScore)
                             .AddTo(this);

        MessageBroker.Default.Receive<OnGameStartedEvent>()
                     .Subscribe(x =>
                     {
                         _alertGameObject.SetActive(false);
                     })
                     .AddTo(this);
    }

    private void OnNewHighScore(OnNewHighScoreEvent obj)
    {
        _alertGameObject.SetActive(true);
        _alertText.SetText($"New high score: {obj.Score}!");
    }
}
