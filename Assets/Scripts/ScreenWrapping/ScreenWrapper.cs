using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrapper : MonoBehaviour
{
    [SerializeField]
    private Transform _wrapperRoot;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<GameArea>(out var gameArea))
        {
            var location = gameArea.transform.InverseTransformPoint(_wrapperRoot.position);

            if (Mathf.Abs(location.y) >= 0.5)
            {
                location.y *= -1;

                location.y = Mathf.Clamp(location.y, -0.49f, 0.49f);
            }
            if (Mathf.Abs(location.x) >= 0.5)
            {
                location.x *= -1;

                location.x = Mathf.Clamp(location.x, -0.49f, 0.49f);
            }

            _wrapperRoot.position = gameArea.transform.TransformPoint(location);
        }
    }
}
