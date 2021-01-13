using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Asteroid : MonoBehaviour, ICollidable, IBoundedObject
{
    [SerializeField]
    private Rigidbody2D _rigidbody;
    [SerializeField]
    private Collider2D _collider;
    [SerializeField]
    private List<AsteroidSettings> _afterDeathAsteroids;

    public Rigidbody2D Rigidbody => _rigidbody;
    public Bounds Bounds => _collider.bounds;

    private int _healthPoints;
    private float _speed;

    private Vector2? _direction;

    public void ApplySettings(AsteroidSettings asteroid)
    {
        _healthPoints = asteroid.HealthPoints;
        _speed = asteroid.Speed;
        _afterDeathAsteroids = asteroid.AfterDeathAsteroids;
    }

    public void OnCollided(Bullet bullet)
    {
        if (!(bullet.GunOwner is PlayerShip))
        {
            return;
        }

        _healthPoints--;

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
        _rigidbody.velocity = -_rigidbody.velocity;
        _direction = _rigidbody.velocity;
    }

    public void SelectRandomDirection()
    {
        if (_direction == null)
        {
            _direction = Random.insideUnitCircle.normalized;
        }
    }

    private void FixedUpdate()
    {
        if (_direction != null)
        {
            _rigidbody.velocity = _direction.Value * _speed;
            _rigidbody.velocity = Vector2.ClampMagnitude(_rigidbody.velocity, _speed);
        }
    }

    private void OnDead()
    {
        if (_afterDeathAsteroids == null || !_afterDeathAsteroids.Any())
        {
            Destroy(gameObject);
            return;
        }

        foreach (var asteroidSetting in _afterDeathAsteroids)
        {
            var asteroid = AsteroidFactory.Instance.CreateAsteroid(asteroidSetting);
            asteroid.transform.position = (Random.insideUnitCircle * 1.4f) + new Vector2(transform.position.x, transform.position.y);
            asteroid.SelectRandomDirection();
        }

         Destroy(gameObject);
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
