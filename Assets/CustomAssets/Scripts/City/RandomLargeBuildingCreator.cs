using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class RandomLargeBuildingCreator : MonoBehaviour
{
    public int m_StoreyNum;
    public bool m_MatchBase;
    public bool m_RotateBase;

    public GameObject[] m_LargeBaseBuilding;
    public GameObject[] m_LargeFloor;
    public GameObject[] m_LargeRoof;

    public MaterialPalette[] m_Palette;

    private int m_HeightIndex = 0;

    private void OnEnable()
    {
        CreateBuilding();
    }

    public void CreateBuilding()
    {
        m_HeightIndex = 0;
        GameObject Parent = new GameObject("Generated Large Building");
        Parent.transform.position = transform.position;

        // Base
        int rdm = Random.Range(0, m_LargeBaseBuilding.Length);
        GameObject Base = Instantiate(m_LargeBaseBuilding[rdm], Parent.transform);
        if (m_RotateBase)
        {
            Base.transform.Rotate(0.0f, 0.0f, 90.0f);
        }

        MaterialPalette selectedPalette = m_Palette[Random.Range(0, m_Palette.Length)];
        Material[] BaseMaterial = selectedPalette.m_BaseMaterial;
        Material[] DoorMaterial = selectedPalette.m_DoorMaterial;
        Material[] StoreDoorMaterial = selectedPalette.m_StoreDoorMaterial;
        Material[] GarageMaterial = selectedPalette.m_GarageMaterial;

        // Material Set
        int maxBaseMats = Base.GetComponent<MeshRenderer>().sharedMaterials.Length;
        int rdmMaterial = 0;
        Material lastStoreyBaseMaterial = null;

        for (int i = 0; i < maxBaseMats; i++)
        {
            if (Base.GetComponent<MeshRenderer>().sharedMaterials[i].name.Contains("Base"))
            {
                rdmMaterial = Random.Range(0, BaseMaterial.Length);
                Material[] tempMatArray = Base.GetComponent<MeshRenderer>().sharedMaterials;
                tempMatArray[i] = BaseMaterial[rdmMaterial];
                Base.GetComponent<MeshRenderer>().sharedMaterials = tempMatArray;
                lastStoreyBaseMaterial = tempMatArray[i];
            }
            if (Base.GetComponent<MeshRenderer>().sharedMaterials[i].name.Contains("Door"))
            {
                rdmMaterial = Random.Range(0, DoorMaterial.Length);
                Material[] tempMatArray = Base.GetComponent<MeshRenderer>().sharedMaterials;
                tempMatArray[i] = DoorMaterial[rdmMaterial];
                Base.GetComponent<MeshRenderer>().sharedMaterials = tempMatArray;
            }
            if (Base.GetComponent<MeshRenderer>().sharedMaterials[i].name.Contains("Shop"))
            {
                rdmMaterial = Random.Range(0, StoreDoorMaterial.Length);
                Material[] tempMatArray = Base.GetComponent<MeshRenderer>().sharedMaterials;
                tempMatArray[i] = StoreDoorMaterial[rdmMaterial];
                Base.GetComponent<MeshRenderer>().sharedMaterials = tempMatArray;
            }
            if (Base.GetComponent<MeshRenderer>().sharedMaterials[i].name.Contains("Garage"))
            {
                rdmMaterial = Random.Range(0, GarageMaterial.Length);
                Material[] tempMatArray = Base.GetComponent<MeshRenderer>().sharedMaterials;
                tempMatArray[i] = GarageMaterial[rdmMaterial];
                Base.GetComponent<MeshRenderer>().sharedMaterials = tempMatArray;
            }
        }

        Material[] StoreyMaterial = selectedPalette.m_StoreyMaterial;

        // Storey
        rdm = Random.Range(0, m_LargeFloor.Length);
        int rdmstoreyMat = Random.Range(0, StoreyMaterial.Length);
        for (m_HeightIndex = 0; m_HeightIndex < m_StoreyNum; m_HeightIndex++)
        {
            GameObject Storey = Instantiate(m_LargeFloor[rdm], Parent.transform);
            Storey.transform.position = new Vector3(Storey.transform.position.x, Storey.transform.position.y + (5.0f + (m_HeightIndex * 4.5f)), Storey.transform.position.z);

            maxBaseMats = Storey.GetComponent<MeshRenderer>().sharedMaterials.Length;
            for (int i = 0; i < maxBaseMats; i++)
            {
                if (Storey.GetComponent<MeshRenderer>().sharedMaterials[i].name.Contains("Base"))
                {                    
                    if (m_MatchBase)
                    {
                        Material[] tempMatArray = Storey.GetComponent<MeshRenderer>().sharedMaterials;
                        tempMatArray[i] = lastStoreyBaseMaterial;
                        Storey.GetComponent<MeshRenderer>().sharedMaterials = tempMatArray;
                        lastStoreyBaseMaterial = tempMatArray[i];
                    }
                    else
                    {
                        Material[] tempMatArray = Storey.GetComponent<MeshRenderer>().sharedMaterials;
                        tempMatArray[i] = StoreyMaterial[rdmstoreyMat];
                        Storey.GetComponent<MeshRenderer>().sharedMaterials = tempMatArray;
                        lastStoreyBaseMaterial = tempMatArray[i];
                    }
                }
            }
        }

        // Roof
        rdm = Random.Range(0, m_LargeRoof.Length);
        GameObject Roof = Instantiate(m_LargeRoof[rdm], Parent.transform);
        Roof.transform.position = new Vector3(Roof.transform.position.x, Roof.transform.position.y + (5.0f + (m_HeightIndex * 4.5f)), Roof.transform.position.z);

        Material[] RoofMaterial = selectedPalette.m_RoofMaterial;

        // Material Set
        maxBaseMats = Roof.GetComponent<MeshRenderer>().sharedMaterials.Length;
        for (int i = 0; i < maxBaseMats; i++)
        {
            if (Roof.GetComponent<MeshRenderer>().sharedMaterials[i].name.Contains("Base"))
            {
                Material[] tempMatArray = Roof.GetComponent<MeshRenderer>().sharedMaterials;
                tempMatArray[i] = lastStoreyBaseMaterial;
                Roof.GetComponent<MeshRenderer>().sharedMaterials = tempMatArray;
            }
            if (Roof.GetComponent<MeshRenderer>().sharedMaterials[i].name.Contains("Roof"))
            {
                rdmMaterial = Random.Range(0, RoofMaterial.Length);
                if (Random.value <= 0.85f)
                {
                    Material[] tempMatArray = Roof.GetComponent<MeshRenderer>().sharedMaterials;
                    tempMatArray[i] = RoofMaterial[rdmMaterial];
                    Roof.GetComponent<MeshRenderer>().sharedMaterials = tempMatArray;
                }
                else
                {
                    rdmMaterial = Random.Range(0, BaseMaterial.Length);
                    Material[] tempMatArray = Roof.GetComponent<MeshRenderer>().sharedMaterials;
                    tempMatArray[i] = RoofMaterial[rdmMaterial];
                    Roof.GetComponent<MeshRenderer>().sharedMaterials = tempMatArray;
                }
            }
        }
    }
}
