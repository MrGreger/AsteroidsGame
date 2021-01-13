using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.Events;

public class GameScore : MonoBehaviour
{
    public UnityEvent<int> ScoreChanged;

    private int _gameScore;
    private void Start()
    {
        MessageBroker.Default.Receive<OnEnemyDieEvent>()
                             .Subscribe(OnEnemyDied)
                             .AddTo(this);
    }

    private void OnEnemyDied(OnEnemyDieEvent @event)
    {
        _gameScore += @event.Enemy.ScoreReward;
        ScoreChanged?.Invoke(_gameScore);
    }
}
