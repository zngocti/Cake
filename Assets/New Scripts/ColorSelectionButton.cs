using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorSelectionButton : MonoBehaviour
{
    [SerializeField]
    Material _material;

    [SerializeField]
    Image _buttonImage;

    [SerializeField]
    bool _changeIconColor = false;

    private void Awake()
    {
        if (_changeIconColor)
        {
            _buttonImage.color = _material.color;
        }
    }

    public void SelectColor()
    {
        SyrupSelector.Instance.SetColor(_material.color);
    }
}
