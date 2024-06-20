using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SideItemPlacer
{
    static List<GameObject> m_items;
    public static List<Material> m_materials;

    public static void PlaceSideItem(GameObject prefab, Vector3 position, Quaternion rotation, Material material, int matIndex, ParticleSystem particles)
    {
        Material[] mats;
        Material match;

        GameObject newItem = GameObject.Instantiate(prefab, position, rotation);
        newItem.transform.parent = GameManager.Instance.m_selectedCake.transform;

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
            m_items = new List<GameObject>();
        }

        if (m_materials == null)
        {
            m_materials = new List<Material>();
        }

        m_items.Add(newItem);
        m_materials.Add(match);
    }

    public static void RemoveLastItem()
    {
        if (m_items.Count - 1 > -1)
        {
            GameObject.Destroy(m_items[m_items.Count - 1].gameObject);
            m_items.RemoveAt(m_items.Count - 1);
            m_materials.RemoveAt(m_materials.Count - 1);
        }
    }
}
