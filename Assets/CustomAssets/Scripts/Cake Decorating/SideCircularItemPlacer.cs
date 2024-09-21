using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SideCircularItemPlacer 
{
    static List<(GameObject, int)> m_items;
    public static List<Material> m_materials;

    public static void PlaceSideCircularItem(GameObject prefab, Transform placeAt, Material material, int matIndex, int itemID, string cakeLayerName, int selectableID)
    {
        GameObject newItem = GameObject.Instantiate(prefab, placeAt);

        Material[] mats = newItem.GetComponent<MeshRenderer>().sharedMaterials;
        Material match = new Material(material);
        mats[0] = match;
        newItem.GetComponent<MeshRenderer>().sharedMaterials = mats;

        newItem.tag = CakeManager.Instance.DecorationTag;
        newItem.AddComponent<MeshCollider>();

        CircularItemsCounter.Instance.AddToList(cakeLayerName, (newItem, selectableID));

        QuickOutline outline = newItem.AddComponent<QuickOutline>();
        outline.OutlineMode = CakeManager.Instance.OutlineMode;
        outline.OutlineColor = CakeManager.Instance.OutlineColor;
        outline.OutlineWidth = CakeManager.Instance.OutlineWidth;
        outline.enabled = false;
        DeleteTool.AddOutineItem(outline);

        newItem.transform.localPosition = new Vector3(0, 0, -1.5f);
        newItem.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 180));
        newItem.transform.localScale = new Vector3(1 / 2.54f, 1 / 2.54f, 1 / 2.54f);

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
    }

    /*
    public static void RemoveCircularItem(Transform placedAt, Material material, int matIndex)
    {
        for (int i = 0; i < m_items.Count; i++)
        {
            if (m_items[i].transform.parent == placedAt && m_items[i].GetComponent<MeshRenderer>().sharedMaterials[matIndex].GetInstanceID() == material.GetInstanceID())
            {
                GameObject.Destroy(m_items[i].gameObject);
                m_items.RemoveAt(i);
                m_materials.RemoveAt(i);
                return;
            }
        }
    }*/

    public static void RemoveCircularItem(int itemID)
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
            if (m_items[i].transform.parent == placedAt && m_items[i].GetComponent<MeshRenderer>().sharedMaterials[matIndex].GetInstanceID() == material.GetInstanceID())
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
