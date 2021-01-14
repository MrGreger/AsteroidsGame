using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class BasicTimer
{
    public event Action Elapsed;

    [SerializeField]
    private float _time;
    private float _currentTime;

    public BasicTimer(float time)
    {
        _time = time;
        _currentTime = 0;
    }

    public void Tick()
    {
        _currentTime += Time.deltaTime;

        if(_currentTime >= _time)
        {
            Elapsed?.Invoke();
            _currentTime = 0;
        }
    }
}

