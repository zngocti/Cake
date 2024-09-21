using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoomButton : MonoBehaviour
{
    [SerializeField]
    Transform _3DButton;

    [SerializeField]
    Button _button;

    public void PushButton()
    {
        _3DButton.Translate(Vector3.forward * -10.0f);
        _button.interactable = false;
    }

    public void PullButton()
    {
        _3DButton.Translate(Vector3.forward * 10.0f);
        _button.interactable = true;
    }
}
