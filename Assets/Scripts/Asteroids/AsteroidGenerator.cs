using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AsteroidGenerator : MonoBehaviour
{
    [SerializeField]
    private PlayerShip _playerShip;
    [SerializeField]
    private AsteroidSettings[] _asteroidVariants;
    [SerializeField]
    private GameArea _gameArea;

    [SerializeField]
    private int _spawnAttempts = 5;

    public void Start()
    {
        InvokeRepeating(nameof(GenerateAsteroid), 0.2f, 8);
    }

    public void GenerateAsteroid()
    {
        if (_asteroidVariants == null || !_asteroidVariants.Any())
        {
            Debug.LogError("Asteroid variants are empty.");
            return;
        }

        StartCoroutine(SpawnAsteroid());
    }

    private IEnumerator SpawnAsteroid()
    {
        var asteroid = AsteroidFactory.Instance.CreateAsteroid(_asteroidVariants[Random.Range(0, _asteroidVariants.Length)]);

        var minSpawnX = _gameArea.Bounds.center.x - _gameArea.Bounds.extents.x + asteroid.Bounds.extents.x;
        var maxSpawnX = _gameArea.Bounds.center.x + _gameArea.Bounds.extents.x - asteroid.Bounds.extents.x;
        var minSpawnY = _gameArea.Bounds.center.y - _gameArea.Bounds.extents.y + asteroid.Bounds.extents.y;
        var maxSpawnY = _gameArea.Bounds.center.y + _gameArea.Bounds.extents.y - asteroid.Bounds.extents.y;

        var attempts = 0;

        var testBounds = asteroid.Bounds;

        while (attempts < _spawnAttempts)
        {
            var spawnX = Random.Range(minSpawnX, maxSpawnX);
            var spawnY = Random.Range(minSpawnY, maxSpawnY);

            testBounds.center = new Vector3(spawnX, spawnY, 0);

            if (testBounds.Intersects(_playerShip.Bounds))
            {
                attempts++;
            }
            else
            {
                asteroid.InitializeAsteroid(testBounds.center);
                break;
            }

            yield return null;
        }

        if (attempts >= _spawnAttempts)
        {
            Debug.LogError("Failed to spawn asteroid.");
            Destroy(asteroid);
        }
    }
}
