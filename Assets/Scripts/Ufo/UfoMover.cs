using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class UfoMover : MonoBehaviour
{
    [SerializeField]
    protected Rigidbody2D _rigidbody;
    [SerializeField]
    protected float _speed;

    public void SetSpeed(float speed)
    {
        if (speed < 0)
        {
            Debug.LogError("Negative speed was set");
            speed = -speed;
        }

        _speed = speed;
    }
}

