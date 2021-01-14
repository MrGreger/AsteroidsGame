using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class LinearUfoMover : UfoMover
{
    [SerializeField]
    private BasicTimer _directionDecisionTimer;
    [SerializeField]
    private Vector2 _direction;

    private void Start()
    {
        if (_directionDecisionTimer == null)
        {
            Debug.LogError("_directionDecisionTimer was not set");
        }
        _directionDecisionTimer = new BasicTimer(5f);

        _directionDecisionTimer.Elapsed += OnDirectionDecide;
    }

    private void OnDirectionDecide()
    {
        var randomNumber = UnityEngine.Random.Range(0, 101);

        if (randomNumber >= 50)
        {
            _direction *= -1;
        }
    }

    private void Update()
    {
        _directionDecisionTimer.Tick();

        Move();
    }

    protected override void Move()
    {
        _rigidbody.velocity = _speed * _direction;
    }
}

