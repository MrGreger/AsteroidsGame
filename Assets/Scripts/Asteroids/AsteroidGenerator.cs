using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

public class AsteroidGenerator : MonoBehaviour, IGenerator
{
    [SerializeField]
    private PlayerShip _playerShip;
    [SerializeField]
    private AsteroidSettings[] _asteroidVariants;
    [SerializeField]
    private GameArea _gameArea;

    [SerializeField]
    private int _spawnAttempts = 5;

    [SerializeField]
    private float _spawnDelay;

    private Coroutine _generatorCoroutine;

    public void Start()
    {
        StartGenerating();

        MessageBroker.Default.Receive<OnAsteroidBreakEvent>()
                             .Subscribe(x => GenerateAsteroids(x.BreakPosition, x.Fragments))
                             .AddTo(this);
    }

    private IEnumerator Generate()
    {
        var waitDelay = new WaitForSeconds(_spawnDelay);


        while (true)
        {
            yield return waitDelay;
            SpawnAsteroid();
        }
    }

    public void GenerateRandomAsteroid()
    {
        if (_asteroidVariants == null || !_asteroidVariants.Any())
        {
            Debug.LogError("Asteroid variants are empty.");
            return;
        }

        StartCoroutine(SpawnAsteroid());
    }

    public void GenerateAsteroids(Vector3 spawnpoint, IEnumerable<AsteroidSettings> asteroids)
    {
        foreach (var asteroidSetting in asteroids)
        {
            var asteroid = AsteroidFactory.Instance.CreateAsteroid(asteroidSetting);
            var asteroidPosition = (Random.insideUnitCircle * 1.4f) + new Vector2(spawnpoint.x, spawnpoint.y);

            asteroidPosition = _gameArea.ClampPositionToGameArea(asteroidPosition);

            asteroid.InitializeAsteroid(asteroidPosition);
        }
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
        testBounds.size *= 1.75f;

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

    public void StartGenerating()
    {
        if (_generatorCoroutine == null)
        {
            _generatorCoroutine = StartCoroutine(Generate());
        }
    }

    public void StopGenerating()
    {
        if (_generatorCoroutine != null)
        {
            StopCoroutine(_generatorCoroutine);
        }
    }
}
