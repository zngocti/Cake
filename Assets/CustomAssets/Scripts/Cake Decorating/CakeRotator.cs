using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CakeRotator : MonoBehaviour
{
    public bool m_rotating;

    [SerializeField]
    float _speed = 20;

    // Update is called once per frame
    void Update()
    {
        if (m_rotating)
        {
            if (GameManager.Instance.m_selectedCake == null)
            {
                return;
            }

            GameManager.Instance.m_selectedCake.transform.Rotate(Vector3.up,  _speed * Time.deltaTime, Space.World);
        }
    }

    public void ToggleRotation()
    {
        m_rotating = !m_rotating;
    }
}
