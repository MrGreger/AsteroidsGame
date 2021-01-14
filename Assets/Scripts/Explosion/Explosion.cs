using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour, IRemovable
{
    public void Place(Vector3 positon)
    {
        transform.position = positon;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    public void Remove()
    {
        Destroy(gameObject);
    }
}
