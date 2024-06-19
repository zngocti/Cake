using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class RandomMediumBuildingCreator : MonoBehaviour
{
    public int m_StoreyNum;
    public bool m_IsCorner;
    public bool m_IsAHome;
    public bool m_HasBalconies;

    public GameObject[] m_MediumBaseBuilding;
    public GameObject[] m_MediumBaseBuildingC;
    public GameObject[] m_MediumFloor;
    public GameObject[] m_MediumFloorAD;
    public GameObject[] m_MediumFloorC;
    public GameObject[] m_MediumRoof;

    public GameObject m_SmallLCD;
    public GameObject m_LongLCD;
    public GameObject m_Awning;
    public GameObject m_AwningC;
    public GameObject m_MediumFloorBalcony;

    public MaterialPalette[] m_Palette;

    private int m_HeightIndex = 0;
    private bool m_BannerSpawned;

    private void OnEnable()
    {
        CreateBuilding();
    }

    public void CreateBuilding()
    {
        m_BannerSpawned = false;
        m_HeightIndex = 0;
        GameObject Parent = new GameObject("Generated Medium Building");
        Parent.transform.position = transform.position;

        if (!m_IsCorner)
        {
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

            // Blank Ad space
            if (m_MediumFloorAD.Length > 0)
            {
                rdm = Random.Range(0, m_MediumFloorAD.Length);
                GameObject StoreyAD = Instantiate(m_MediumFloorAD[rdm], Parent.transform);
                StoreyAD.transform.position = new Vector3(StoreyAD.transform.position.x, StoreyAD.transform.position.y + (5.175f + (m_HeightIndex * 4.425f)), StoreyAD.transform.position.z);
                StoreyAD.GetComponent<MeshRenderer>().sharedMaterial = lastStoreyBaseMaterial;
                m_HeightIndex++;
            }

            int m_ADHeightOffset = m_HeightIndex;
            Material[] StoreyMaterial = selectedPalette.m_StoreyMaterial;
            rdm = Random.Range(0, m_MediumFloor.Length);
            float rdmVal = Random.value;
            // Storey
            for (; m_HeightIndex < m_StoreyNum; m_HeightIndex++)
            {
                GameObject Storey = Instantiate(m_MediumFloor[rdm], Parent.transform);
                Storey.transform.position = new Vector3(Storey.transform.position.x, Storey.transform.position.y + (5.0f + (m_HeightIndex * 4.5f)), Storey.transform.position.z);
               
                maxBaseMats = Storey.GetComponent<MeshRenderer>().sharedMaterials.Length;
                for (int i = 0; i < maxBaseMats; i++)
                {
                    if (Storey.GetComponent<MeshRenderer>().sharedMaterials[i].name.Contains("Base"))
                    {
                        if (m_MediumBaseBuilding[0].name.Contains("BaseE") || m_MediumBaseBuilding[0].name.Contains("BaseD"))
                        {
                            Material[] tempMatArray = Storey.GetComponent<MeshRenderer>().sharedMaterials;
                            tempMatArray[i] = lastStoreyBaseMaterial;
                            Storey.GetComponent<MeshRenderer>().sharedMaterials = tempMatArray;
                            lastStoreyBaseMaterial = tempMatArray[i];
                        }
                        else
                        {
                            if (Random.value <= 0.85f && m_HeightIndex > m_ADHeightOffset)
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
                    }
                }
                // Chance for Balcony
                if (m_HasBalconies)
                {
                    if (m_HeightIndex > 0)
                    {
                        GameObject Balcony = Instantiate(m_MediumFloorBalcony, Parent.transform);
                        Balcony.transform.position = new Vector3(Balcony.transform.position.x, Balcony.transform.position.y + (5.0f + (m_HeightIndex * 4.5f)), Balcony.transform.position.z);
                        Balcony.GetComponent<MeshRenderer>().sharedMaterial = lastStoreyBaseMaterial;
                    }
                }
                // LCD Banner
                if (Random.value <= 0.40f && m_StoreyNum > 4.0f && m_HeightIndex < m_StoreyNum - 5.0f && !m_BannerSpawned)
                {
                    float ZOffset = 0;
                    if (m_HasBalconies)
                    {
                        ZOffset = 1.5f;
                    }
                    if (Random.value <= 0.50f)
                    {
                        GameObject Banner = Instantiate(m_LongLCD, Parent.transform);
                        Banner.transform.position = new Vector3(Banner.transform.position.x, Banner.transform.position.y + (5.0f + (m_HeightIndex * 4.5f)) + 1.0f, Banner.transform.position.z + ZOffset);
                        m_BannerSpawned = true;
                    }
                    else
                    {
                        GameObject Banner = Instantiate(m_LongLCD, Parent.transform);
                        Banner.transform.position = new Vector3(-Banner.transform.position.x, Banner.transform.position.y + (5.0f + (m_HeightIndex * 4.5f)) + 1.0f, Banner.transform.position.z + ZOffset);
                        m_BannerSpawned = true;
                    }
                }
            }

            // Awning
            if (m_Awning)
            {
                if (m_StoreyNum > 0 && Random.value <= 1.35f)
                {
                    GameObject Awning = Instantiate(m_Awning, Parent.transform);
                    Awning.transform.position = new Vector3(Awning.transform.position.x, Awning.transform.position.y, Awning.transform.position.z);

                    // Material Set
                    Material[] AwningMaterial = selectedPalette.m_AwningMaterial;
                    Awning.GetComponent<MeshRenderer>().sharedMaterial = AwningMaterial[Random.Range(0, AwningMaterial.Length)];
                }
            }
            GameObject Roof;
            // Roof
            if (m_HasBalconies)
            {
                Roof = Instantiate(m_MediumRoof[2], Parent.transform);
                Roof.transform.position = new Vector3(Roof.transform.position.x, Roof.transform.position.y + (5.0f + (m_HeightIndex * 4.5f)), Roof.transform.position.z);
            }
            else
            {
                rdm = Random.Range(0, m_MediumRoof.Length - 1);
                Roof = Instantiate(m_MediumRoof[rdm], Parent.transform);
                Roof.transform.position = new Vector3(Roof.transform.position.x, Roof.transform.position.y + (5.0f + (m_HeightIndex * 4.5f)), Roof.transform.position.z);
            }

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
            int rdm = Random.Range(0, m_MediumBaseBuildingC.Length);
            GameObject Base = Instantiate(m_MediumBaseBuildingC[rdm], Parent.transform);

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
                rdm = Random.Range(0, m_MediumFloorC.Length);
                GameObject Storey = Instantiate(m_MediumFloorC[rdm], Parent.transform);
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
            }

            // Awning
            if (m_StoreyNum > 0 && Base != m_MediumBaseBuilding[2] && Random.value <= 0.35f)
            {
                GameObject Awning = Instantiate(m_AwningC, Parent.transform);
                Awning.transform.position = new Vector3(Awning.transform.position.x, Awning.transform.position.y + 3.0f, Awning.transform.position.z);

                // Material Set
                Material[] AwningMaterial = selectedPalette.m_AwningMaterial;
                Awning.GetComponent<MeshRenderer>().sharedMaterial = AwningMaterial[Random.Range(0, AwningMaterial.Length)];
            }

            // Roof
            rdm = Random.Range(0, m_MediumRoof.Length);
            GameObject Roof = Instantiate(m_MediumRoof[rdm], Parent.transform);
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
