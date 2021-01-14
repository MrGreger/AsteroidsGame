using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ExplosionAnimationListener : MonoBehaviour
{
    public UnityEvent AnimationDone;

    public void OnAnimationDone()
    {
        AnimationDone?.Invoke();
    }
}
