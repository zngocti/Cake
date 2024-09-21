using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ColorVariationController : MonoBehaviour
{
    public MaterialPalette m_Palette;

    private void OnEnable()
    {
        //CreateBuilding();
    }

    // Start is called before the first frame update
    public void CreateBuilding()
    {        
        Material[] BaseMaterial         = m_Palette.m_BaseMaterial;
        Material[] DoorMaterial         = m_Palette.m_DoorMaterial;
        Material[] StoreDoorMaterial    = m_Palette.m_StoreDoorMaterial;
        Material[] GarageMaterial       = m_Palette.m_GarageMaterial;
        Material[] RoofMaterial         = m_Palette.m_RoofMaterial;

        int maxMats = GetComponent<MeshRenderer>().sharedMaterials.Length;
        int rdmMaterial = 0;
        Material lastStoreyBaseMaterial = null;

        rdmMaterial = Random.Range(0, BaseMaterial.Length);
        for (int i = 0; i < maxMats; i++)
        {
            if (GetComponent<MeshRenderer>().sharedMaterials[i].name.Contains("Base"))
            {
                Material[] tempMatArray = GetComponent<MeshRenderer>().sharedMaterials;
                tempMatArray[i] = BaseMaterial[rdmMaterial];
                GetComponent<MeshRenderer>().sharedMaterials = tempMatArray;
                lastStoreyBaseMaterial = tempMatArray[i];
            }
        }

        rdmMaterial = Random.Range(0, DoorMaterial.Length);
        for (int i = 0; i < maxMats; i++)
        {
            if (GetComponent<MeshRenderer>().sharedMaterials[i].name.Contains("Door"))
            {                
                Material[] tempMatArray = GetComponent<MeshRenderer>().sharedMaterials;
                tempMatArray[i] = DoorMaterial[rdmMaterial];
                GetComponent<MeshRenderer>().sharedMaterials = tempMatArray;
            }
        }

        rdmMaterial = Random.Range(0, StoreDoorMaterial.Length);
        for (int i = 0; i < maxMats; i++)
        {
            if (GetComponent<MeshRenderer>().sharedMaterials[i].name.Contains("Shop"))
            {
                Material[] tempMatArray = GetComponent<MeshRenderer>().sharedMaterials;
                tempMatArray[i] = StoreDoorMaterial[rdmMaterial];
                GetComponent<MeshRenderer>().sharedMaterials = tempMatArray;
            }
        }        

        rdmMaterial = Random.Range(0, GarageMaterial.Length);
        for (int i = 0; i < maxMats; i++)
        {
            if (GetComponent<MeshRenderer>().sharedMaterials[i].name.Contains("Garage"))
            {
                Material[] tempMatArray = GetComponent<MeshRenderer>().sharedMaterials;
                tempMatArray[i] = GarageMaterial[rdmMaterial];
                GetComponent<MeshRenderer>().sharedMaterials = tempMatArray;
            }
        }        

        rdmMaterial = Random.Range(0, RoofMaterial.Length);
        for (int i = 0; i < maxMats; i++)
        {
            if (GetComponent<MeshRenderer>().sharedMaterials[i].name.Contains("Roof"))
            {
                Material[] tempMatArray = GetComponent<MeshRenderer>().sharedMaterials;
                tempMatArray[i] = RoofMaterial[rdmMaterial];
                GetComponent<MeshRenderer>().sharedMaterials = tempMatArray;
            }
        }
    }
}
