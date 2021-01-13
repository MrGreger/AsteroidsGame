using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBoundedObject
{
    Bounds Bounds { get; }
}
