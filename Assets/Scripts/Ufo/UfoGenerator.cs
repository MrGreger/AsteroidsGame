using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

enum ScreenEdge
{
    Top,
    Right,
    Left,
    Bottom
}

public class UfoGenerator : MonoBehaviour, IGenerator
{
    [SerializeField]
    private GameArea _gameArea;
    [SerializeField]
    private List<UfoSettings> _ufoVariants;
    [SerializeField]
    private int _spawnAttempts = 10;
    [SerializeField]
    private PlayerShip _playerShip;
    [SerializeField]
    private float _spawnDelay = 5f;

    private Coroutine _generatorCoroutine;

    public void Start()
    {
        StartGenerating();
    }

    private IEnumerator Generate()
    {
        yield return new WaitForSeconds(Random.Range(5, 10f));

        var waitDelay = new WaitForSeconds(_spawnDelay);

        SpawnUfo();

        while (true)
        {
            yield return waitDelay;
            SpawnUfo();
        }
    }

    private IEnumerator PlaceUfo(Ufo ufo)
    {
        var randomSide = (ScreenEdge)Random.Range(0, 4);

        var attempts = 0;

        var testBounds = ufo.Bounds;
        testBounds.size *= 1.75f;

        while (attempts < _spawnAttempts)
        {

            var spawnX = float.MaxValue;
            var spawnY = float.MaxValue;

            switch (randomSide)
            {
                case ScreenEdge.Top:
                    spawnY = 0.49f;
                    spawnX = Random.Range(-0.49f, 0.49f);
                    break;
                case ScreenEdge.Bottom:
                    spawnY = -0.49f;
                    spawnX = Random.Range(-0.49f, 0.49f);
                    break;
                case ScreenEdge.Right:
                    spawnX = 0.49f;
                    spawnY = Random.Range(-0.49f, 0.49f);
                    break;
                case ScreenEdge.Left:
                    spawnX = -0.49f;
                    spawnY = Random.Range(-0.49f, 0.49f);
                    break;
            }

            testBounds.center = _gameArea.transform.TransformPoint(new Vector3(spawnX, spawnY, 0));

            if (testBounds.Intersects(_playerShip.Bounds))
            {
                attempts++;
            }
            else
            {
                var spawnPoint = testBounds.center;
                ufo.transform.position = spawnPoint;
                break;
            }

            yield return null;
        }

        if (attempts >= _spawnAttempts)
        {
            Debug.LogError("Failed to spawn ufo.");
            Destroy(ufo);
        }

    }

    public void SpawnUfo()
    {
        if (_ufoVariants == null || !_ufoVariants.Any())
        {
            Debug.LogError("Ufo variants are empty.");
            return;
        }

        var ufo = UfoFactory.Instance.CreateUfo(_ufoVariants[Random.Range(0, _ufoVariants.Count)]);

        StartCoroutine(PlaceUfo(ufo));
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
