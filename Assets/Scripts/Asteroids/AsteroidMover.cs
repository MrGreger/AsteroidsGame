using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AsteroidMover : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D _rigidbody;
    public Rigidbody2D Rigidbody => _rigidbody;

    private Vector2? _direction;

    private float _speed;

    public void SelectRandomDirection()
    {
        if (_direction == null)
        {
            _direction = AvoidingUserDirectionSelector.Instance.SelectRandomDirection(transform.position);
        }
    }

    public void SetOppositeDirection()
    {
        _rigidbody.velocity = -_rigidbody.velocity;
        _direction = _rigidbody.velocity;
    }

    public void SetSpeed(float speed)
    {
        if(speed < 0)
        {
            Debug.LogError("Negative speed was set");
            speed = -speed;
        }

        _speed = speed;
    }

    private void FixedUpdate()
    {
        if (_direction != null)
        {
            _rigidbody.velocity = _direction.Value * _speed;
            _rigidbody.velocity = Vector2.ClampMagnitude(_rigidbody.velocity, _speed);
        }
    }
}
