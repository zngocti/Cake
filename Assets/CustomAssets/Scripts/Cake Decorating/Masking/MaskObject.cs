using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class MaskObject : MonoBehaviour
{
    public GameObject[] m_maskObj;

    int[] _oldQueue;

    // Start is called before the first frame update
    void Start()
    {
        _oldQueue = new int[m_maskObj.Length];

        for (int i = 0; i < m_maskObj.Length; i++)
        {
            MeshRenderer renderer = m_maskObj[i].GetComponent<MeshRenderer>();
            _oldQueue[i] = renderer.material.renderQueue;
            renderer.material.renderQueue = 3002;
        }
    }

    public void FinishScale()
    {
        for (int i = 0; i < m_maskObj.Length; i++)
        {
            m_maskObj[i].GetComponent<MeshRenderer>().material.renderQueue = _oldQueue[i];
        }

        IcingItem.StopIcing();
        gameObject.SetActive(false);
    }
}
