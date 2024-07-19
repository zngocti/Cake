using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonUpDown : MonoBehaviour
{
    [SerializeField]
    Button _upButton;

    [SerializeField]
    CameraMoveCakeLayer _cameraMove;

    [SerializeField]
    bool _goingUp;

    public void NextLayer()
    {
        _cameraMove.m_goingUp = _goingUp;
        _upButton.onClick.Invoke();
    }
}
