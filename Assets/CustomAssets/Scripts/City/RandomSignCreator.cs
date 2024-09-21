using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class RandomSignCreator : MonoBehaviour
{
    public Material[] m_signMaterials;

    private void OnEnable()
    {
        //RandomizeSign();
    }

    void RandomizeSign()
    {
        // Material Set
        int maxBaseMats = GetComponent<MeshRenderer>().sharedMaterials.Length;
        int rdmMaterial = 0;

        for (int i = 0; i < maxBaseMats; i++)
        {
            if (GetComponent<MeshRenderer>().sharedMaterials[i].name.Contains("Sign"))
            {
                rdmMaterial = Random.Range(0, m_signMaterials.Length);
                Material[] tempMatArray = GetComponent<MeshRenderer>().sharedMaterials;
                tempMatArray[i] = m_signMaterials[rdmMaterial];
                GetComponent<MeshRenderer>().sharedMaterials = tempMatArray;
            }
        }
    }
}
