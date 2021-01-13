using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private PlayerActions _playerInput => PlayerInputSingleton.Instance.PlayerInputController;

    [SerializeField]
    private Rigidbody2D _rigidbody;

    [Range(1, 10)]
    [SerializeField]
    private float _minAcceleration;
    [Range(1, 10)]
    [SerializeField]
    private float _maxAcceleration;
    [Range(1, 5)]
    [SerializeField]
    private float _timeToMaxSpeed;
    [Range(0.1f, 10)]
    [SerializeField]
    private float _slowDownSpeed;
    [Range(0.1f, 10)]
    [SerializeField]
    private float _rotationSpeed;

    private float _currentTime;
    private float _currentAcceleration;
    private bool _acclerating;
    private Vector2 _accelerationDirection;

    private void Update()
    {
        CalculateAcceleration();

        UpdateRotation();
    }

    private void UpdateRotation()
    {
        var rotationInput = -_playerInput.PlayerShip.Rotate.ReadValue<Vector2>().x;

        var currentRotation = transform.rotation.eulerAngles;

        var desiredRotation = Quaternion.Euler(currentRotation + new Vector3(0, 0, rotationInput * _rotationSpeed));

        _rigidbody.MoveRotation(desiredRotation);
    }

    private void CalculateAcceleration()
    {
        if (_acclerating)
        {
            _accelerationDirection = transform.rotation * Vector2.up;
            _currentTime += Time.deltaTime;
            _currentTime = Mathf.Clamp(_currentTime, 0, _timeToMaxSpeed);
            _currentAcceleration = Mathf.Lerp(_minAcceleration, _maxAcceleration, _currentTime / _timeToMaxSpeed);
        }
        else
        {
            _currentAcceleration -= _slowDownSpeed * Time.deltaTime;

            if (_currentAcceleration < 0)
            {
                _currentAcceleration = 0;
            }
        }
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = Vector3.Lerp(_rigidbody.velocity, _accelerationDirection * _currentAcceleration, Time.deltaTime * _slowDownSpeed);
    }

    private void Start()
    {
        _playerInput.PlayerShip.Move.performed += ctx => StartAccelerating();
        _playerInput.PlayerShip.Move.canceled += ctx => StopAccelerating();
    }

    private void OnDisable()
    {
        _playerInput.PlayerShip.Move.performed -= ctx => StartAccelerating();
        _playerInput.PlayerShip.Move.canceled -= ctx => StopAccelerating();
    }

    private void StartAccelerating()
    {
        _acclerating = true;
    }

    private void StopAccelerating()
    {
        _currentTime = 0;
        _acclerating = false;
    }
}
