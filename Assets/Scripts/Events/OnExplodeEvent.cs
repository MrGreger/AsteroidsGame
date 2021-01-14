using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class OnExplodeEvent
{
    public Vector3 ExplosionPosition { get; }

    public OnExplodeEvent(Vector3 explosionPosition)
    {
        ExplosionPosition = explosionPosition;
    }
}

