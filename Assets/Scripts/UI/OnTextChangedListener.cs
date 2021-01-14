using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OnTextChangedListener : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _textComponent;

    public void SetText(int textValue)
    {
        _textComponent.SetText(textValue.ToString());
    }

    public void SetText(float textValue)
    {
        _textComponent.SetText(textValue.ToString());
    }
}
