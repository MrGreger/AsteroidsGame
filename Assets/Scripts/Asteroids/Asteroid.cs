﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class Asteroid : MonoBehaviour, ICollidable, IBoundedObject, IEnemy
{
    [SerializeField]
    private Collider2D _collider;
    [SerializeField]
    private List<AsteroidSettings> _afterDeathAsteroids;
    [SerializeField]
    private AsteroidMover _asteroidMover;

    public AsteroidMover AsteroidMover => _asteroidMover;
    public Bounds Bounds => _collider.bounds;

    public int ScoreReward { get; private set; }

    private int _healthPoints;

    public void ApplySettings(AsteroidSettings asteroid)
    {
        _healthPoints = asteroid.HealthPoints;

        _asteroidMover.SetSpeed(asteroid.Speed);

        _afterDeathAsteroids = asteroid.AfterDeathAsteroids;
        ScoreReward = asteroid.ScoreReward;
    }

    public void OnCollided(Bullet bullet)
    {
        if (!(bullet.GunOwner is PlayerShip))
        {
            return;
        }

        _healthPoints--;

        MessageBroker.Default.Publish(new EnemyHitEvent());

        if (_healthPoints <= 0)
        {
            OnDead();
        }
    }

    public void OnHit(Asteroid asteroid)
    {
        throw new System.NotImplementedException();
    }

    public void OnCollided(Asteroid asteroid)
    {
        _asteroidMover.SetOppositeDirection();
    }

    private void OnDead()
    {
        MessageBroker.Default.Publish(new OnEnemyDieEvent(this));

        if (_afterDeathAsteroids == null || !_afterDeathAsteroids.Any())
        {
            Destroy(gameObject);
            return;
        }

        MessageBroker.Default.Publish(new OnAsteroidBreakEvent(_afterDeathAsteroids, transform.position));

        Destroy(gameObject);
    }

    public void InitializeAsteroid(Vector3 initialPosition)
    {
        transform.position = initialPosition;
        AsteroidMover.SelectRandomDirection();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<ICollidable>(out var collidable))
        {
            collidable.OnCollided(this);
        }
    }

    public void OnCollided(SpaceShip spaceShip)
    {
        throw new System.NotImplementedException();
    }
}
