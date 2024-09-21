using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SideItemPlacer
{
    static List<(GameObject, int)> m_items;
    public static List<Material> m_materials;

    public static Transform PlaceSideItem(GameObject prefab, Vector3 position, Quaternion rotation, Material material, int matIndex, ParticleSystem particles, int itemID)
    {
        Material[] mats;
        Material match;

        GameObject newItem = GameObject.Instantiate(prefab, position, rotation);
        newItem.transform.parent = GameManager.Instance.m_selectedCake.transform;
        
        newItem.tag = CakeManager.Instance.DecorationTag;
        newItem.AddComponent<MeshCollider>();

        QuickOutline outline = newItem.AddComponent<QuickOutline>();
        outline.OutlineMode = CakeManager.Instance.OutlineMode;
        outline.OutlineColor = CakeManager.Instance.OutlineColor;
        outline.OutlineWidth = CakeManager.Instance.OutlineWidth;
        outline.enabled = false;
        DeleteTool.AddOutineItem(outline);

        ParticleSystem newParticles = GameObject.Instantiate(particles, position, rotation);
        newParticles.Stop();
        var particlShape = newParticles.shape;
        particlShape.meshRenderer = newItem.GetComponent<MeshRenderer>();
        newParticles.Play();

        if (newItem.GetComponent<MeshRenderer>())
            mats = newItem.GetComponent<MeshRenderer>().sharedMaterials;
        else
            mats = newItem.transform.GetChild(0).GetComponent<MeshRenderer>().sharedMaterials;

        match = new Material(material);

        mats[matIndex] = match;

        newItem.GetComponent<MeshRenderer>().sharedMaterials = mats;

        if (m_items == null)
        {
            m_items = new List<(GameObject, int)>();
        }

        if (m_materials == null)
        {
            m_materials = new List<Material>();
        }

        (GameObject, int) newItemWithID = (newItem, itemID);

        m_items.Add(newItemWithID);
        m_materials.Add(match);

        return newItem.transform;
    }

    /*
    public static void RemoveLastItem()
    {
        if (m_items.Count - 1 > -1)
        {
            GameObject.Destroy(m_items[m_items.Count - 1].gameObject);
            m_items.RemoveAt(m_items.Count - 1);
            m_materials.RemoveAt(m_materials.Count - 1);
        }
    }*/

    /*
    public static void RemoveSideItem(Transform placedAt, Material material, int matIndex)
    {
        for (int i = 0; i < m_items.Count; i++)
        {
            if (m_items[i].transform == placedAt && m_items[i].GetComponent<MeshRenderer>().sharedMaterials[matIndex].GetInstanceID() == material.GetInstanceID())
            {
                GameObject.Destroy(m_items[i].gameObject);
                m_items.RemoveAt(i);
                m_materials.RemoveAt(i);
                return;
            }
        }
    }*/

    public static void RemoveSideItem(int itemID)
    {
        for (int i = 0; i < m_items.Count; i++)
        {
            if (m_items[i].Item2 == itemID)
            {
                GameObject.Destroy(m_items[i].Item1);
                m_items.RemoveAt(i);
                m_materials.RemoveAt(i);
                return;
            }
        }
    }

    /*
    public static bool IsPlaced(Transform placedAt, Material material, int matIndex)
    {
        for (int i = 0; i < m_items.Count; i++)
        {
            if (m_items[i].transform == placedAt && m_items[i].GetComponent<MeshRenderer>().sharedMaterials[matIndex].GetInstanceID() == material.GetInstanceID())
            {
                return true;
            }
        }

        return false;
    }*/

    public static bool IsPlaced(int itemID)
    {
        for (int i = 0; i < m_items.Count; i++)
        {
            if (m_items[i].Item2 == itemID)
            {
                return true;
            }
        }

        return false;
    }
}
