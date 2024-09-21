using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomADTowerCreator : MonoBehaviour
{
    public GameObject[] m_MediumBaseBuilding;
    public GameObject[] m_Tower;
    public GameObject[] m_MediumRoof;

    public GameObject m_Awning;

    public MaterialPalette[] m_Palette;

    private int m_HeightIndex = 0;

    public void CreateBuilding()
    {
        m_HeightIndex = 0;
        GameObject Parent = new GameObject("Generated AD Tower");
        Parent.transform.position = transform.position;

        // Base
        int rdm = Random.Range(0, m_MediumBaseBuilding.Length);
        GameObject Base = Instantiate(m_MediumBaseBuilding[rdm], Parent.transform);

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
        rdm = Random.Range(0, m_Tower.Length);
        GameObject Storey = Instantiate(m_Tower[rdm], Parent.transform);
        Storey.transform.position = new Vector3(Storey.transform.position.x, Storey.transform.position.y + (5.0f + (m_HeightIndex * 4.5f)), Storey.transform.position.z);

        maxBaseMats = Storey.GetComponent<MeshRenderer>().sharedMaterials.Length;
        for (int i = 0; i < maxBaseMats; i++)
        {
            if (Storey.GetComponent<MeshRenderer>().sharedMaterials[i].name.Contains("Base"))
            {
                float rdmVal = Random.value;
                if (!m_MediumBaseBuilding[rdm].name.Contains("SmallBaseC"))
                {
                    if (Random.value <= 0.85f)
                    {
                        Material[] tempMatArray = Storey.GetComponent<MeshRenderer>().sharedMaterials;
                        tempMatArray[i] = lastStoreyBaseMaterial;
                        Storey.GetComponent<MeshRenderer>().sharedMaterials = tempMatArray;
                        lastStoreyBaseMaterial = tempMatArray[i];
                    }
                    else
                    {
                        if (rdmVal <= 0.60f)
                        {
                            Material[] tempMatArray = Storey.GetComponent<MeshRenderer>().sharedMaterials;
                            tempMatArray[i] = StoreyMaterial[0];
                            Storey.GetComponent<MeshRenderer>().sharedMaterials = tempMatArray;
                            lastStoreyBaseMaterial = tempMatArray[i];
                        }
                        else if (rdmVal > 0.60f && rdmVal <= 0.70f)
                        {
                            rdmMaterial = Random.Range(0, BaseMaterial.Length);
                            Material[] tempMatArray = Storey.GetComponent<MeshRenderer>().sharedMaterials;
                            tempMatArray[i] = StoreyMaterial[1];
                            Storey.GetComponent<MeshRenderer>().sharedMaterials = tempMatArray;
                            lastStoreyBaseMaterial = tempMatArray[i];
                        }
                        else if (rdmVal > 0.70f && rdmVal <= 0.80f)
                        {
                            rdmMaterial = Random.Range(0, BaseMaterial.Length);
                            Material[] tempMatArray = Storey.GetComponent<MeshRenderer>().sharedMaterials;
                            tempMatArray[i] = StoreyMaterial[2];
                            Storey.GetComponent<MeshRenderer>().sharedMaterials = tempMatArray;
                            lastStoreyBaseMaterial = tempMatArray[i];
                        }
                        else if (rdmVal > 0.80f && rdmVal <= 0.90f)
                        {
                            rdmMaterial = Random.Range(0, BaseMaterial.Length);
                            Material[] tempMatArray = Storey.GetComponent<MeshRenderer>().sharedMaterials;
                            tempMatArray[i] = StoreyMaterial[3];
                            Storey.GetComponent<MeshRenderer>().sharedMaterials = tempMatArray;
                            lastStoreyBaseMaterial = tempMatArray[i];
                        }
                        else if (rdmVal > 0.90f && rdmVal <= 1.00f)
                        {
                            rdmMaterial = Random.Range(0, BaseMaterial.Length);
                            Material[] tempMatArray = Storey.GetComponent<MeshRenderer>().sharedMaterials;
                            tempMatArray[i] = StoreyMaterial[4];
                            Storey.GetComponent<MeshRenderer>().sharedMaterials = tempMatArray;
                            lastStoreyBaseMaterial = tempMatArray[i];
                        }
                    }
                }
                else
                {
                    Material[] tempMatArray = Storey.GetComponent<MeshRenderer>().sharedMaterials;
                    tempMatArray[i] = lastStoreyBaseMaterial;
                    Storey.GetComponent<MeshRenderer>().sharedMaterials = tempMatArray;
                }
            }
        }
        // Awning
        GameObject Awning = Instantiate(m_Awning, Parent.transform);
        Awning.transform.position = new Vector3(Awning.transform.position.x, Awning.transform.position.y, Awning.transform.position.z);
        GameObject Awning2 = Instantiate(m_Awning, Parent.transform);
        Awning2.transform.localPosition = new Vector3(-Awning2.transform.localPosition.z, Awning2.transform.localPosition.y, Awning2.transform.localPosition.x);
        Awning2.transform.Rotate(0.0f, 0.0f, -90.0f);

        // Material Set
        Material[] AwningMaterial = selectedPalette.m_AwningMaterial;
        rdm = Random.Range(0, AwningMaterial.Length);
        Awning.GetComponent<MeshRenderer>().sharedMaterial = AwningMaterial[rdm];
        Awning2.GetComponent<MeshRenderer>().sharedMaterial = AwningMaterial[rdm];
    }
}
