using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class LinearUfoMover : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D _rigidbody;
    [SerializeField]
    private BasicTimer _directionDecisionTimer;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private Vector2 _direction;

    private void Start()
    {
        _direction = new Vector2(-1, 0);

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

    private void Move()
    {
        _rigidbody.velocity = _speed * _direction;
    }
}

