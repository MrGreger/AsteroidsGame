using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class GameArea : MonoBehaviour
{
    [SerializeField]
    private BoxCollider2D _screenBoundsCollider;

    private float _lastCameraAspect;

    private void Update()
    {
        if(Camera.main.aspect == _lastCameraAspect)
        {
            return;
        }

        var height = Camera.main.orthographicSize * 2;
        var width = height * Camera.main.aspect;

        var screenBounds = new Bounds(Camera.main.transform.position, new Vector3(width, height));

        _screenBoundsCollider.transform.localScale = new Vector3(screenBounds.size.x / _screenBoundsCollider.size.x, screenBounds.size.y / _screenBoundsCollider.size.y);
    }
}
