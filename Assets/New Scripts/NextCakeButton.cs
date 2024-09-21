using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextCakeButton : MonoBehaviour
{
    [SerializeField]
    Transform _3DButton;

    bool _down = false;

    public void PushButton()
    {
        if (!GameManager.Instance.NextCake() || _down)
        {
            return;
        }        

        _3DButton.Translate(Vector3.forward * -10.0f);
        _down = true;
    }

    public void PullButton()
    {
        if (!_down)
        {
            return;
        }

        _3DButton.Translate(Vector3.forward * 10.0f);
        _down = false;
    }
}
