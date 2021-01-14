using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class ExplosionDispatcher : MonoBehaviour
{
    public Explosion ExplosionPrefab;

    private void Start()
    {
        MessageBroker.Default.Receive<OnEnemyDieEvent>()
                             .Where(x => x.Enemy is MonoBehaviour)
                             .Select(x => x.Enemy as MonoBehaviour)
                             .Subscribe(x =>
                             {
                                 CreateExplosion(x.transform.position);
                             }).AddTo(this);
    }

    private void CreateExplosion(Vector3 position)
    {
        var explosion = Instantiate(ExplosionPrefab, position, Quaternion.identity);
        explosion.Place(position);
    }
}
