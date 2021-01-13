using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollidable
{
    void OnCollided(Asteroid asteroid);
    void OnCollided(SpaceShip spaceShip);
    void OnCollided(Bullet spaceShip);
}
