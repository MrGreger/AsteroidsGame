using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class SpaceShip : MonoBehaviour, IBoundedObject
{
    [SerializeField]
    protected Gun _gun;
    [SerializeField]
    protected Collider2D _collider;

    public Bounds Bounds => _collider.bounds;
}
