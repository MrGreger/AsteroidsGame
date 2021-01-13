using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    private void Start()
    {
        MessageBroker.Default.Receive<OnPlayerDiedEvent>()
                             .Subscribe(x =>
                             {
                                 SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                             });
    }
}

