using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class OnAsteroidBreakEvent
{
    public OnAsteroidBreakEvent(IEnumerable<AsteroidSettings> fragments, Vector3 breakPosition)
    {
        Fragments = fragments;
        BreakPosition = breakPosition;
    }

    public IEnumerable<AsteroidSettings> Fragments { get; }
    public Vector3 BreakPosition { get; }
}

