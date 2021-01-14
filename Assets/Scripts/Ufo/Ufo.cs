using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class Ufo : SpaceShip, ICollidable, IEnemy, IRemovable
{
    [SerializeField]
    private BasicTimer _shootTimer;
    [SerializeField]
    private UfoMover _ufoMover;
    private PlayerShip _playerShip;

    public int ScoreReward { get; set; }

    public void ApplySettings(UfoSettings ufo)
    {
        _ufoMover.SetSpeed(ufo.Speed);
        ScoreReward = ufo.ScoreReward;

        _shootTimer = new BasicTimer(ufo.ShootCoolDown);
    }

    private void Start()
    {
        _playerShip = FindObjectOfType<PlayerShip>();

        if (_shootTimer == null)
        {
            Debug.LogError("_shootTimer was not set.");
        }

        _shootTimer = new BasicTimer(1.5f);

        _shootTimer.Elapsed += OnShoot;
    }

    private void OnShoot()
    {
        if(_playerShip == null)
        {
            return;
        }

        var shootDirection = (_playerShip.transform.position - _gun.transform.position).normalized;

        var rotation = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
        _gun.transform.rotation =  Quaternion.Euler(0, 0, rotation - 90);
        _gun.Shoot();
    }

    private void Update()
    {
        _shootTimer.Tick();
    }

    public void OnCollided(Asteroid asteroid)
    {
    }

    public void OnCollided(SpaceShip spaceShip)
    {
    }

    public void OnCollided(Bullet bullet)
    {
        if(bullet.GunOwner is PlayerShip)
        {
            OnDead();
        }
    }

    private void OnDead()
    {
        MessageBroker.Default.Publish(new OnExplodeEvent(transform.position));
        MessageBroker.Default.Publish(new OnEnemyDieEvent(this));

        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.TryGetComponent<ICollidable>(out var ship))
        {
            ship.OnCollided(this);
        }
    }

    public void Remove()
    {
        Destroy(gameObject);
    }
}
