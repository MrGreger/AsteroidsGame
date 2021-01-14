using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class GameArea : MonoBehaviour, IBoundedObject
{
    [SerializeField]
    private BoxCollider2D _screenBoundsCollider;

    private float _lastCameraAspect;

    public Bounds Bounds => _screenBoundsCollider.bounds;

    public Vector2 ClampPositionToGameArea(Vector2 objectPosition)
    {
        var relativeAsteroidPosition = transform.InverseTransformPoint(objectPosition);
        relativeAsteroidPosition.x = Mathf.Clamp(relativeAsteroidPosition.x, -0.49f, 0.49f);
        relativeAsteroidPosition.y = Mathf.Clamp(relativeAsteroidPosition.y, -0.49f, 0.49f);
        objectPosition = transform.TransformPoint(relativeAsteroidPosition);
        return objectPosition;
    }

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
