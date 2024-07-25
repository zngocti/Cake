using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class MaskObject : MonoBehaviour
{
    public GameObject[] m_maskObj;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < m_maskObj.Length; i++)
        {
            m_maskObj[i].GetComponent<MeshRenderer>().material.renderQueue = 3002;
        }
    }
}
