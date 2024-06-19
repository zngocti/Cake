using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideCircularItemPlaceCommand : ICommand
{
    private GameObject m_prefab;
    private Transform m_placeAt;
    private Material m_material;
    private int m_matIndex;

    public SideCircularItemPlaceCommand(GameObject prefab, Transform placeAt, Material material, int matIndex)
    {
        this.m_prefab = prefab;
        this.m_placeAt = placeAt;
        this.m_material = material;
        this.m_matIndex = matIndex;
    }

    public void Execute()
    {
        SideCircularItemPlacer.PlaceSideCircularItem(m_prefab, m_placeAt, m_material, m_matIndex);
    }

    public void Undo()
    {
        SideCircularItemPlacer.RemoveCircularItem(m_placeAt, SideCircularItemPlacer.m_materials[SideCircularItemPlacer.m_materials.Count - 1], m_matIndex);
    }
}
