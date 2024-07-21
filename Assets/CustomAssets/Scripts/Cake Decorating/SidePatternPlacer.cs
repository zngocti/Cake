using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SidePatternPlacer
{
    static List<Transform> m_items;
    public static List<Material> m_materials;

    public static void PlaceSidePattern(Transform placeAt, Material material)
    {
        Material[] mats = placeAt.GetComponent<MeshRenderer>().sharedMaterials;
        Material match = new Material(material);

        mats[1] = match;

        placeAt.GetComponent<MeshRenderer>().sharedMaterials = mats;

        if (m_items == null)
        {
            m_items = new List<Transform>();
        }

        if (m_materials == null)
        {
            m_materials = new List<Material>();
        }

        m_items.Add(placeAt);
        m_materials.Add(match);
    }

    public static void RemoveSidePattern(Transform placedAt, Material material)
    {
        for (int i = 0; i < m_materials.Count; i++)
        {
            if (m_items[i] == placedAt && m_materials[i].GetInstanceID() == material.GetInstanceID())
            {
                for (int j = m_items.Count - 1; j >= 0; j--)
                {
                    if (j > 0)
                    {
                        if (m_items[j - 1] == placedAt)
                        {
                            Material[] mats = placedAt.GetComponent<MeshRenderer>().sharedMaterials;
                            mats[1] = m_materials[j - 1];
                            placedAt.GetComponent<MeshRenderer>().sharedMaterials = mats;
                            break;
                        }
                    }
                    else
                    {
                        Material[] mats = placedAt.GetComponent<MeshRenderer>().sharedMaterials;
                        Material blank = new Material(Shader.Find("Specular"));
                        mats[1] = blank;
                        placedAt.GetComponent<MeshRenderer>().sharedMaterials = mats;
                        break;
                    }
                }
                m_items.RemoveAt(i);
                m_materials.RemoveAt(i);
                return;
            }
        }
    }

    public static bool IsPlaced(Transform placedAt, Material material)
    {
        for (int i = 0; i < m_items.Count; i++)
        {
            if (m_items[i] == placedAt && m_materials[i].GetInstanceID() == material.GetInstanceID())
            {
                return true;
            }
        }

        return false;
    }
}
