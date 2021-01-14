using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.Events;

public class SoundEventsListener : MonoBehaviour
{
    public UnityEvent OnExplode;
    public UnityEvent EnemyHit;

    private void Start()
    {
        MessageBroker.Default.Receive<OnExplodeEvent>()
                             .Subscribe(x => OnExplode?.Invoke())
                             .AddTo(this);

        MessageBroker.Default.Receive<EnemyHitEvent>()
                     .Subscribe(x => EnemyHit?.Invoke())
                     .AddTo(this);
    }
}
