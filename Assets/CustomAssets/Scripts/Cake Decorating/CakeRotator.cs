using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CakeRotator : MonoBehaviour
{
    public bool m_rotating;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_rotating)
        {
            GameManager.Instance.m_selectedCake.transform.Rotate(new Vector3(0, 20 * Time.deltaTime, 0));
        }
    }

    public void ToggleRotation()
    {
        m_rotating = !m_rotating;
    }
}
