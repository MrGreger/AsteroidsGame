using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.Events;

public class GameScore : MonoBehaviour
{
    private const string SCORE_SETTING_NAME = "Score";

    public UnityEvent<int> ScoreChanged;

    private int _gameScore;
    private void Start()
    {
        MessageBroker.Default.Receive<OnEnemyDieEvent>()
                             .Subscribe(OnEnemyDied)
                             .AddTo(this);

        MessageBroker.Default.Receive<OnGameOverEvent>()
                             .Subscribe(CheckScores)
                             .AddTo(this);

        MessageBroker.Default.Receive<OnGameStartedEvent>()
                             .Subscribe(ResetScore)
                             .AddTo(this);
    }

    private void ResetScore(OnGameStartedEvent obj)
    {
        _gameScore = 0;
        ScoreChanged?.Invoke(_gameScore);
    }

    private void CheckScores(OnGameOverEvent obj)
      {
        if (PlayerPrefs.HasKey(SCORE_SETTING_NAME))
        {
            var oldScore = PlayerPrefs.GetInt(SCORE_SETTING_NAME);

            if (oldScore >= _gameScore)
            {
                return;
            }
        }

        MessageBroker.Default.Publish(new OnNewHighScoreEvent(_gameScore));
        PlayerPrefs.SetInt(SCORE_SETTING_NAME, _gameScore);
    }

    private void OnEnemyDied(OnEnemyDieEvent @event)
    {
        _gameScore += @event.Enemy.ScoreReward;
        ScoreChanged?.Invoke(_gameScore);
    }
}
