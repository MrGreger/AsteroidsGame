using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class AvoidingUserDirectionSelector : Singleton<AvoidingUserDirectionSelector>
{
    [SerializeField]
    private PlayerShip _playerShip;
    [SerializeField]
    private float _avoidDistance = 1;

    public Vector2 SelectRandomDirection(Vector3 objectPosition)
    {
        var direction = UnityEngine.Random.insideUnitCircle.normalized;

        if (Vector3.Distance(_playerShip.transform.position, objectPosition) <= _avoidDistance)
        {
            if (Vector3.Dot(direction, _playerShip.transform.position - objectPosition) >= 0)
            {
                direction = -direction;
            }
        }

        return direction;
    }
}

