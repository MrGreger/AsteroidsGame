using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidFactory : Singleton<AsteroidFactory>
{
    public Asteroid CreateAsteroid(AsteroidSettings settings)
    {
        var asteroid = Instantiate(settings.Prefab);
        asteroid.ApplySettings(settings);

        return asteroid;
    }
}
