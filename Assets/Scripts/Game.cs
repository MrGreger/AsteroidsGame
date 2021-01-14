using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public UnityEvent OnDied;

    [SerializeField]
    private PlayerShip _player;

    private void Start()
    {
        MessageBroker.Default.Receive<OnPlayerDiedEvent>()
                             .Subscribe(x =>
                             {
                                 OnPlayerDied();
                             });
    }

    public void Restart()
    {
        _player.transform.position = Vector3.zero;

        RemoveAllRemovables();
        RestartGenerators();
    }

    private void RestartGenerators()
    {
        var generators = FindObjectsOfType<MonoBehaviour>().OfType<IGenerator>();

        foreach (var generator in generators)
        {
            generator.StopGenerating();
            generator.StartGenerating();
        }
    }

    private void RemoveAllRemovables()
    {
        var removables = FindObjectsOfType<MonoBehaviour>().OfType<IRemovable>();

        foreach (var removable in removables)
        {
            removable.Remove();
        }
    }

    private void OnPlayerDied()
    {
        Time.timeScale = 0;
        OnDied?.Invoke();
    }


}

