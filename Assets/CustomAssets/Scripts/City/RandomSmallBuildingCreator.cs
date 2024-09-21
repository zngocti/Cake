using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MaterialPalette
{
    public Material[] m_BaseMaterial;
    public Material[] m_StoreyMaterial;
    public Material[] m_DoorMaterial;
    public Material[] m_StoreDoorMaterial;
    public Material[] m_AwningMaterial;
    public Material[] m_RoofMaterial;
    public Material[] m_GarageMaterial;
}

[ExecuteInEditMode]
public class RandomSmallBuildingCreator : MonoBehaviour
{
    public int m_StoreyNum;
    public bool m_IsCorner;

    public GameObject[] m_SmallBaseBuilding;
    public GameObject[] m_SmallBaseBuildingC;
    public GameObject[] m_SmallFloor;
    public GameObject[] m_SmallFloorC;
    public GameObject[] m_SmallRoof;

    public GameObject m_Awning;
    public GameObject m_AwningC;
    public GameObject m_SmallFloorBalcony;

    public MaterialPalette[] m_Palette;

    private int m_HeightIndex = 0;

    private void OnEnable()
    {
        CreateBuilding();
    }

    public void CreateBuilding()
    {
        m_HeightIndex = 0;
        GameObject Parent = new GameObject("Generated Building");
        Parent.transform.position = transform.position;

        if (!m_IsCorner)
        {
            // Base
            int rdm = Random.Range(0, m_SmallBaseBuilding.Length);
            GameObject Base = Instantiate(m_SmallBaseBuilding[rdm], Parent.transform);

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
            for (m_HeightIndex = 0; m_HeightIndex < m_StoreyNum; m_HeightIndex++)
            {
                rdm = Random.Range(0, m_SmallFloor.Length);
                GameObject Storey = Instantiate(m_SmallFloor[rdm], Parent.transform);
                Storey.transform.position = new Vector3(Storey.transform.position.x, Storey.transform.position.y + (5.0f + (m_HeightIndex * 4.5f)), Storey.transform.position.z);

                maxBaseMats = Storey.GetComponent<MeshRenderer>().sharedMaterials.Length;
                for (int i = 0; i < maxBaseMats; i++)
                {
                    if (Storey.GetComponent<MeshRenderer>().sharedMaterials[i].name.Contains("Base"))
                    {
                        float rdmVal = Random.value;
                        if (!m_SmallBaseBuilding[rdm].name.Contains("SmallBaseC"))
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

                // Chance for Balcony
                if (Random.value <= 0.30f)
                {
                    GameObject Balcony = Instantiate(m_SmallFloorBalcony, Parent.transform);
                    Balcony.transform.position = new Vector3(Balcony.transform.position.x, Balcony.transform.position.y + (5.0f + (m_HeightIndex * 4.5f)), Balcony.transform.position.z);
                    Balcony.GetComponent<MeshRenderer>().sharedMaterial = lastStoreyBaseMaterial;
                }
            }

            // Awning
            if (m_Awning)
            {
                if (m_StoreyNum > 0 && Base != m_SmallBaseBuilding[1] && Random.value <= 0.35f)
                {
                    GameObject Awning = Instantiate(m_Awning, Parent.transform);
                    Awning.transform.position = new Vector3(Awning.transform.position.x, Awning.transform.position.y, Awning.transform.position.z);

                    // Material Set
                    Material[] AwningMaterial = selectedPalette.m_AwningMaterial;
                    Awning.GetComponent<MeshRenderer>().sharedMaterial = AwningMaterial[Random.Range(0, AwningMaterial.Length)];
                }
            }

            // Roof
            rdm = Random.Range(0, m_SmallRoof.Length);
            GameObject Roof = Instantiate(m_SmallRoof[rdm], Parent.transform);
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
        else
        {
            // Base
            int rdm = Random.Range(0, m_SmallBaseBuildingC.Length);
            GameObject Base = Instantiate(m_SmallBaseBuildingC[rdm], Parent.transform);

            MaterialPalette selectedPalette = m_Palette[Random.Range(0, m_Palette.Length)];
            Material[] BaseMaterial = selectedPalette.m_BaseMaterial;
            Material[] DoorMaterial = selectedPalette.m_DoorMaterial;
            Material[] StoreDoorMaterial = selectedPalette.m_StoreDoorMaterial;
            Material[] GarageMaterial = selectedPalette.m_GarageMaterial;

            // Material Set
            int maxBaseMats = Base.GetComponent<MeshRenderer>().sharedMaterials.Length;
            int rdmMaterial = 0;


            for (int i = 0; i < maxBaseMats; i++)
            {
                if (Base.GetComponent<MeshRenderer>().sharedMaterials[i].name.Contains("Base"))
                {
                    rdmMaterial = Random.Range(0, BaseMaterial.Length);
                    Material[] tempMatArray = Base.GetComponent<MeshRenderer>().sharedMaterials;
                    tempMatArray[i] = BaseMaterial[rdmMaterial];
                    Base.GetComponent<MeshRenderer>().sharedMaterials = tempMatArray;
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

            Material lastStoreyBaseMaterial = BaseMaterial[rdmMaterial];

            Material[] StoreyMaterial = selectedPalette.m_StoreyMaterial;
            // Storey
            for (m_HeightIndex = 0; m_HeightIndex < m_StoreyNum; m_HeightIndex++)
            {
                rdm = Random.Range(0, m_SmallFloorC.Length);
                GameObject Storey = Instantiate(m_SmallFloorC[rdm], Parent.transform);
                Storey.transform.position = new Vector3(Storey.transform.position.x, Storey.transform.position.y + (5.0f + (m_HeightIndex * 4.5f)), Storey.transform.position.z);

                maxBaseMats = Storey.GetComponent<MeshRenderer>().sharedMaterials.Length;
                for (int i = 0; i < maxBaseMats; i++)
                {
                    if (Storey.GetComponent<MeshRenderer>().sharedMaterials[i].name.Contains("Base"))
                    {
                        float rdmVal = Random.value;
                        if (rdmVal <= 0.60f)
                        {
                            Material[] tempMatArray = Storey.GetComponent<MeshRenderer>().sharedMaterials;
                            tempMatArray[i] = StoreyMaterial[0];
                            Storey.GetComponent<MeshRenderer>().sharedMaterials = tempMatArray;
                            lastStoreyBaseMaterial = tempMatArray[i];
                        }
                        else if(rdmVal > 0.60f && rdmVal <= 0.70f)
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
            }

            // Awning
            if (m_StoreyNum > 0 && Base != m_SmallBaseBuilding[2] && Random.value <= 0.35f)
            {
                GameObject Awning = Instantiate(m_AwningC, Parent.transform);
                Awning.transform.position = new Vector3(Awning.transform.position.x, Awning.transform.position.y + 3.0f, Awning.transform.position.z);

                // Material Set
                Material[] AwningMaterial = selectedPalette.m_AwningMaterial;
                Awning.GetComponent<MeshRenderer>().sharedMaterial = AwningMaterial[Random.Range(0, AwningMaterial.Length)];
            }

            // Roof
            rdm = Random.Range(0, m_SmallRoof.Length);
            GameObject Roof = Instantiate(m_SmallRoof[rdm], Parent.transform);
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
}
