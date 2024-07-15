using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorSlider : MonoBehaviour
{
    Quaternion _startingRotation;
    private void Start()
    {
        StartCoroutine(SetStartingRotation());
    }

    IEnumerator SetStartingRotation()
    {
        yield return null;
        _startingRotation = GameManager.Instance.m_selectedCake.transform.rotation;
    }
    public void NewRotation(float num)
    {
        if (num < 0)
        {
            num = 1;
        }
        else if (num > 1)
        {
            num = 0;
        }

        if (num == 0.5f)
        {
            GameManager.Instance.m_selectedCake.transform.rotation = _startingRotation;
            return;
        }

        if (num < 0.5f)
        {
            num = - ((0.5f - num) * 180 / 0.5f);
        }
        else
        {
            num = (num - 0.5f) * 180 / 0.5f;
        }

        Vector3 newAngle = new Vector3(_startingRotation.eulerAngles.x, _startingRotation.eulerAngles.y + num, _startingRotation.eulerAngles.z);
        GameManager.Instance.m_selectedCake.transform.rotation = Quaternion.Euler(newAngle);
    }
}
